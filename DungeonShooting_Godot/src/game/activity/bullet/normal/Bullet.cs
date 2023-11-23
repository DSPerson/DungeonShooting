
using System;
using System.Collections;
using Godot;

/// <summary>
/// 子弹类
/// </summary>
[Tool]
public partial class Bullet : ActivityObject, IBullet
{
    public event Action OnReclaimEvent;
    public event Action OnLeavePoolEvent;
    
    public bool IsRecycled { get; set; }
    public string Logotype { get; set; }
    
    /// <summary>
    /// 子弹伤害碰撞区域
    /// </summary>
    [Export, ExportFillNode]
    public Area2D CollisionArea { get; set; }
    
    /// <summary>
    /// 子弹伤害碰撞检测形状
    /// </summary>
    [Export, ExportFillNode]
    public CollisionShape2D CollisionShape2D { get; set; }

    /// <summary>
    /// 攻击的层级
    /// </summary>
    public uint AttackLayer
    {
        get => CollisionArea.CollisionMask;
        set => CollisionArea.CollisionMask = value;
    }

    public BulletData BulletData { get; private set; }
    
    /// <summary>
    /// 当前反弹次数
    /// </summary>
    public int CurrentBounce { get; protected set; } = 0;

    /// <summary>
    /// 当前穿透次数
    /// </summary>
    public int CurrentPenetration { get; protected set; } = 0;
    
    //当前子弹已经飞行的距离
    private float CurrFlyDistance = 0;

    private bool _init = false;
    
    public virtual void InitData(BulletData data, uint attackLayer)
    {
        if (!_init)
        {
            CollisionArea.AreaEntered += OnArea2dEntered;
            _init = true;
        }
        
        CurrentBounce = 0;
        CurrentPenetration = 0;
        CurrFlyDistance = 0;
        
        BulletData = data;
        AttackLayer = attackLayer;
        Rotation = data.Rotation;
        
        var triggerRole = data.TriggerRole;
        if (data.TriggerRole != null && data.TriggerRole.AffiliationArea != null) //设置所属区域
        {
            if (triggerRole.AffiliationArea != null) 
            {
                triggerRole.AffiliationArea.InsertItem(this);
            }
        }
        
        Position = data.Position + new Vector2(0, data.Altitude);
        Altitude = data.Altitude;
        if (data.VerticalSpeed != 0)
        {
            VerticalSpeed = data.VerticalSpeed;
        }
        else
        {
            VerticalSpeed = 0;
        }

        //BasisVelocity = new Vector2(data.FlySpeed, 0).Rotated(Rotation);
        MoveController.AddForce(new Vector2(data.FlySpeed, 0).Rotated(Rotation));
        
        //如果子弹会对玩家造成伤害, 则显示红色描边
        if (Player.Current.CollisionWithMask(attackLayer))
        {
            ShowBorderFlashes();
        }
        PutDown(RoomLayerEnum.YSortLayer);
        //播放子弹移动动画
        PlaySpriteAnimation(AnimatorNames.Move);
        //强制更新下坠逻辑处理
        UpdateFall((float)GetProcessDeltaTime());

        //过期销毁
        if (data.LifeTime > 0)
        {
            this.CallDelay(data.LifeTime, OnLimeOver);
        }
    }

    public override void OnMoveCollision(KinematicCollision2D collision)
    {
        CurrentBounce++;
        if (CurrentBounce > BulletData.BounceCount) //反弹次数超过限制
        {
            //创建粒子特效
            var effect = ObjectManager.GetPoolItem<IEffect>(ResourcePath.prefab_effect_weapon_BulletSmoke_tscn);
            var smoke = (Node2D)effect;
            var rotated = AnimatedSprite.Position.Rotated(Rotation);
            smoke.GlobalPosition = collision.GetPosition() + new Vector2(0, rotated.Y);
            smoke.GlobalRotation = collision.GetNormal().Angle();
            smoke.AddToActivityRoot(RoomLayerEnum.YSortLayer);
            effect.PlayEffect();
            DoReclaim();
        }
    }

    /// <summary>
    /// 碰到目标
    /// </summary>
    public virtual void OnCollisionTarget(ActivityObject o)
    {
        if (o is Role role)
        {
            PlayDisappearEffect();

            //击退
            if (role is not Player) //目标不是玩家才会触发击退
            {
                if (BulletData.Repel != 0)
                {
                    role.AddRepelForce(Velocity.Normalized() * BulletData.Repel);
                }
            }
            
            //造成伤害
            role.CallDeferred(nameof(Role.Hurt), BulletData.TriggerRole.IsDestroyed ? null : BulletData.TriggerRole, BulletData.Harm, Rotation);

            //穿透次数
            CurrentPenetration++;
            if (CurrentPenetration > BulletData.Penetration)
            {
                DoReclaim();
            }
        }
    }

    /// <summary>
    /// 到达最大运行距离
    /// </summary>
    public virtual void OnMaxDistance()
    {
        PlayDisappearEffect();
        DoReclaim();
    }
    
    /// <summary>
    /// 子弹生命周期结束
    /// </summary>
    public virtual void OnLimeOver()
    {
        PlayDisappearEffect();
        DoReclaim();
    }
    
    protected override void OnFallToGround()
    {
        //落地销毁
        PlayDisappearEffect();
        DoReclaim();
    }
    
    /// <summary>
    /// 显示红色描边
    /// </summary>
    public void ShowBorderFlashes()
    {
        ShowOutline = true;
        OutlineColor = new Color(1, 0, 0);
        StartCoroutine(BorderFlashes());
    }
    
    private IEnumerator BorderFlashes()
    {
        while (true)
        {
            ShowOutline = !ShowOutline;
            yield return new WaitForSeconds(0.12f);
        }
    }

    /// <summary>
    /// 播放子弹消失的特效
    /// </summary>
    public virtual void PlayDisappearEffect()
    {
        var effect = ObjectManager.GetPoolItem<IEffect>(ResourcePath.prefab_effect_weapon_BulletDisappear_tscn);
        var node = (Node2D)effect;
        node.GlobalPosition = AnimatedSprite.GlobalPosition;
        node.AddToActivityRoot(RoomLayerEnum.YSortLayer);
        effect.PlayEffect();
    }
    
    protected override void Process(float delta)
    {
        if (ActivityMaterial.DynamicCollision)
        {
            //子弹高度大于 16 关闭碰撞检测
            CollisionShape2D.Disabled = Altitude >= 16;
        }
        //距离太大, 自动销毁
        CurrFlyDistance += BulletData.FlySpeed * delta;
        if (CurrFlyDistance >= BulletData.MaxDistance)
        {
            OnMaxDistance();
        }
    }
    
    private void OnArea2dEntered(Area2D other)
    {
        if (IsDestroyed)
        {
            return;
        }
        var activityObject = other.AsActivityObject();
        OnCollisionTarget(activityObject);
    }
    
    public virtual void DoReclaim()
    {
        ObjectPool.Reclaim(this);
    }
    
    public virtual void OnReclaim()
    {
        if (OnReclaimEvent != null)
        {
            OnReclaimEvent();
        }
        if (AffiliationArea != null)
        {
            AffiliationArea.RemoveItem(this);
        }
        ShowOutline = false;
        GetParent().CallDeferred(Node.MethodName.RemoveChild, this);
    }

    public virtual void OnLeavePool()
    {
        MoveController.ClearForce();
        StopAllCoroutine();
        if (OnLeavePoolEvent != null)
        {
            OnLeavePoolEvent();
        }
    }
}