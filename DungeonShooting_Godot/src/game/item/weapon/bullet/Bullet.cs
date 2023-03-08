using Godot;

/// <summary>
/// 子弹类
/// </summary>
[RegisterActivity(ActivityIdPrefix.Bullet + "0001", ResourcePath.prefab_weapon_bullet_Bullet_tscn)]
public partial class Bullet : ActivityObject
{
    /// <summary>
    /// 碰撞区域
    /// </summary>
    public Area2D CollisionArea { get; private set; }

    // 最大飞行距离
    private float MaxDistance;

    // 子弹飞行速度
    private float FlySpeed;

    //当前子弹已经飞行的距离
    private float CurrFlyDistance = 0;

    public void Init(float speed, float maxDistance, Vector2 position, float rotation, uint targetLayer)
    {
        CollisionArea = GetNode<Area2D>("CollisionArea");
        CollisionArea.CollisionMask = targetLayer;
        CollisionArea.AreaEntered += OnArea2dEntered;

        FlySpeed = speed;
        MaxDistance = maxDistance;
        Position = position;
        Rotation = rotation;
        ShadowOffset = new Vector2(0, 5);

        BasisVelocity = new Vector2(FlySpeed, 0).Rotated(Rotation);
    }

    protected override void PhysicsProcessOver(float delta)
    {
        //移动
        var lastSlideCollision = GetLastSlideCollision();
        //撞到墙
        if (lastSlideCollision != null)
        {
            //创建粒子特效
            var packedScene = ResourceManager.Load<PackedScene>(ResourcePath.prefab_effect_BulletSmoke_tscn);
            var smoke = packedScene.Instantiate<GpuParticles2D>();
            smoke.GlobalPosition = lastSlideCollision.GetPosition();
            smoke.GlobalRotation = lastSlideCollision.GetNormal().Angle();
            smoke.AddToActivityRoot(RoomLayerEnum.YSortLayer);

            Destroy();
            return;
        }
        //距离太大, 自动销毁
        CurrFlyDistance += FlySpeed * delta;
        if (CurrFlyDistance >= MaxDistance)
        {
            var packedScene = ResourceManager.Load<PackedScene>(ResourcePath.prefab_effect_BulletDisappear_tscn);
            var node = packedScene.Instantiate<Node2D>();
            node.GlobalPosition = GlobalPosition;
            node.AddToActivityRoot(RoomLayerEnum.YSortLayer);
            
            Destroy();
        }
    }

    private void OnArea2dEntered(Area2D other)
    {
        var role = other.AsActivityObject<Role>();
        if (role != null)
        {
            role.CallDeferred(nameof(Role.Hurt), 4, Rotation);
            Destroy();
        }
    }
}