using Godot;

/// <summary>
/// 子弹类
/// </summary>
public class Bullet : ActivityObject
{
    /// <summary>
    /// 碰撞区域
    /// </summary>
    public Area2D CollisionArea { get; }

    // 最大飞行距离
    private float MaxDistance;

    // 子弹飞行速度
    private float FlySpeed = 350;

    //当前子弹已经飞行的距离
    private float CurrFlyDistance = 0;

    public Bullet(string scenePath, float maxDistance, Vector2 position, float rotation, uint targetLayer) :
        base(scenePath)
    {
        CollisionArea = GetNode<Area2D>("CollisionArea");
        CollisionArea.CollisionMask = targetLayer;
        CollisionArea.Connect("area_entered", this, nameof(OnArea2dEntered));

        MaxDistance = maxDistance;
        Position = position;
        Rotation = rotation;
        ShadowOffset = new Vector2(0, 5);
    }

    public override void _Ready()
    {
        base._Ready();
        //绘制阴影
        ShowShadowSprite();
    }

    public override void _PhysicsProcess(float delta)
    {
        base._PhysicsProcess(delta);
        //移动
        var kinematicCollision = MoveAndCollide(new Vector2(FlySpeed * delta, 0).Rotated(Rotation));
        if (kinematicCollision != null)
        {
            //创建粒子特效
            var packedScene = ResourceManager.Load<PackedScene>(ResourcePath.prefab_effect_BulletSmoke_tscn);
            var smoke = packedScene.Instance<Particles2D>();
            smoke.GlobalPosition = kinematicCollision.Position;
            smoke.GlobalRotation = kinematicCollision.Normal.Angle();
            GameApplication.Instance.Room.GetRoot(true).AddChild(smoke);

            Destroy();
            return;
        }
        //距离太大, 自动销毁
        CurrFlyDistance += FlySpeed * delta;
        if (CurrFlyDistance >= MaxDistance)
        {
            Destroy();
        }
    }

    private void OnArea2dEntered(Area2D other)
    {
        var role = other.AsActivityObject<Role>();
        if (role != null)
        {
            role.Hurt(1);
            Destroy();
        }
    }
}