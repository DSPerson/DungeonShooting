using System;
using System.Collections;
using System.Collections.Generic;
using Godot;
using Godot.Collections;

/// <summary>
/// 激光子弹
/// </summary>
public partial class Laser : Area2D, IBullet
{
    /// <summary>
    /// 激光默认宽度
    /// </summary>
    public const float LaserDefaultWidth = 3f;
    
    /// <summary>
    /// 子节点包含的例子特效, 在创建完成后自动播放
    /// </summary>
    [Export]
    public Array<GpuParticles2D> Particles2D { get; set; }
    
    public CollisionShape2D Collision { get; private set; }
    public Sprite2D LineSprite { get; private set; }
    public RectangleShape2D Shape { get; private set; }

    public event Action OnReclaimEvent;
    public event Action OnLeavePoolEvent;
    
    public bool IsRecycled { get; set; }
    public string Logotype { get; set; }

    public uint AttackLayer
    {
        get => CollisionMask;
        set => CollisionMask = value;
    }

    public BulletData BulletData { get; private set; }

    public bool IsDestroyed { get; private set; }
    
    public float Width { get; set; }
    
    //开启的协程
    private List<CoroutineData> _coroutineList;
    private float _pixelScale;
    private float _speed = 2000;
    private Tween _tween;
    private bool _init = false;

    public void InitData(BulletData data, uint attackLayer)
    {
        InitData(data, attackLayer, LaserDefaultWidth);
    }
    
    public void InitData(BulletData data, uint attackLayer, float width)
    {
        if (!_init)
        {
            Collision = GetNodeOrNull<CollisionShape2D>("CollisionShape2D");
            Collision.Disabled = true;
            Shape = (RectangleShape2D)Collision.Shape;
            LineSprite = GetNodeOrNull<Sprite2D>("LineSprite");
            _pixelScale = 1f / LineSprite.Texture.GetHeight();

            AreaEntered += OnArea2dEntered;
            
            _init = true;
        }
        
        BulletData = data;
        AttackLayer = attackLayer;
        
        Position = data.Position;
        Rotation = data.Rotation;

        //计算射线最大距离, 也就是撞到墙壁的距离
        var targetPosition = data.Position + Vector2.FromAngle(data.Rotation) * data.MaxDistance;
        var parameters = PhysicsRayQueryParameters2D.Create(data.Position, targetPosition, PhysicsLayer.Wall);
        var result = GetWorld2D().DirectSpaceState.IntersectRay(parameters);
        float distance;
        var doRebound = false; //是否需要执行反弹
        Vector2? reboundPosition = null;
        Vector2? reboundNormal = null;
        if (result != null && result.TryGetValue("position", out var point)) //撞到墙壁
        {
            doRebound = true;
            reboundPosition = (Vector2)point;
            reboundNormal = (Vector2)result["normal"];
            distance = Position.DistanceTo(reboundPosition.Value);
        }
        else //没撞到墙壁
        {
            distance = data.MaxDistance;
        }
        
        Collision.SetDeferred(CollisionShape2D.PropertyName.Disabled, false);
        Collision.Position = Vector2.Zero;
        Shape.Size = Vector2.Zero;
        LineSprite.Scale = new Vector2(0, width * _pixelScale);

        //如果子弹会对玩家造成伤害, 则显示成红色
        if (Player.Current.CollisionWithMask(attackLayer))
        {
            LineSprite.Modulate = new Color(2.5f, 0.5f, 0.5f);
        }
        else
        {
            LineSprite.Modulate = new Color(1.5f, 1.5f, 1.5f);
        }
        
        //激光飞行时间
        var time = distance / _speed;

        _tween = CreateTween();
        _tween.SetParallel();
        _tween.TweenProperty(LineSprite, "scale", new Vector2(distance, width * _pixelScale), time);
        _tween.TweenProperty(Collision, "position", new Vector2(distance * 0.5f, 0), time);
        _tween.TweenProperty(Shape, "size", new Vector2(distance, width), time);
        _tween.Chain();
        //持续时间
        // tween.TweenInterval(0.2f);
        // tween.Chain();
        _tween.TweenCallback(Callable.From(() =>
        {
            //执行反弹
            if (doRebound)
            {
                CallDeferred(nameof(OnRebound), reboundPosition.Value, reboundNormal.Value);
            }
            Collision.SetDeferred(CollisionShape2D.PropertyName.Disabled, false);
        }));
        _tween.Chain();
        _tween.TweenProperty(LineSprite, "scale", new Vector2(distance, 0), 0.3f);
        _tween.Chain();
        _tween.TweenCallback(Callable.From(() =>
        {
            _tween = null;
            DoReclaim();
        }));
        _tween.Play();
        
        if (Particles2D != null)
        {
            foreach (var particles2D in Particles2D)
            {
                particles2D.Restart();
            }
        }
    }

    public override void _Process(double delta)
    {
        ProxyCoroutineHandler.ProxyUpdateCoroutine(ref _coroutineList, (float)delta);
    }

    public void Destroy()
    {
        if (IsDestroyed)
        {
            return;
        }
        
        QueueFree();
    }

    //激光撞墙反弹逻辑
    private void OnRebound(Vector2 position, Vector2 normal)
    {
        if (BulletData.BounceCount > 0)
        {
            var newDistance = BulletData.MaxDistance - BulletData.Position.DistanceTo(position);
            if (newDistance > 0)
            {
                float rotation;
                if (normal.X == 0 && normal.Y == 0)
                {
                    rotation = (BulletData.Rotation + Mathf.Pi) % (Mathf.Pi * 2);
                }
                else
                {
                    rotation = Utils.ReflectByNormal(BulletData.Rotation, normal);
                }
                
                var bulletData = BulletData.Clone();
                bulletData.Position = position;
                bulletData.BounceCount -= 1;
                bulletData.MaxDistance = newDistance;
                bulletData.Rotation = rotation;
                FireManager.ShootBullet(bulletData, AttackLayer);
            }
        }
    }
    
    private void OnArea2dEntered(Area2D other)
    {
        var role = other.AsActivityObject<Role>();
        if (role != null)
        {
            //击退
            if (BulletData.Repel != 0)
            {
                role.AddRepelForce(Vector2.FromAngle(Rotation) * BulletData.Repel);
            }
            //造成伤害
            role.CallDeferred(nameof(Role.Hurt), BulletData.TriggerRole.IsDestroyed ? null : BulletData.TriggerRole, BulletData.Harm, Rotation);
        }
    }

    public long StartCoroutine(IEnumerator able)
    {
        return ProxyCoroutineHandler.ProxyStartCoroutine(ref _coroutineList, able);
    }

    public void StopCoroutine(long coroutineId)
    {
        ProxyCoroutineHandler.ProxyStopCoroutine(ref _coroutineList, coroutineId);
    }

    public bool IsCoroutineOver(long coroutineId)
    {
        return ProxyCoroutineHandler.ProxyIsCoroutineOver(ref _coroutineList, coroutineId);
    }

    public void StopAllCoroutine()
    {
        ProxyCoroutineHandler.ProxyStopAllCoroutine(ref _coroutineList);
    }
    
    public void DoReclaim()
    {
        ObjectPool.Reclaim(this);
    }
    
    public virtual void OnReclaim()
    {
        if (Particles2D != null)
        {
            foreach (var particles2D in Particles2D)
            {
                particles2D.Emitting = false;
            }
        }
        if (OnReclaimEvent != null)
        {
            OnReclaimEvent();
        }

        if (_tween != null)
        {
            _tween.Dispose();
            _tween = null;
        }
        GetParent().CallDeferred(Node.MethodName.RemoveChild, this);
    }

    public virtual void OnLeavePool()
    {
        StopAllCoroutine();
        if (OnLeavePoolEvent != null)
        {
            OnLeavePoolEvent();
        }
    }
}
