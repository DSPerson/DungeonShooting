using Godot;

/// <summary>
/// 子弹类
/// </summary>
[Tool]
public partial class Bullet : ActivityObject
{
    /// <summary>
    /// 碰撞区域
    /// </summary>
    [Export, ExportFillNode]
    public Area2D CollisionArea { get; set; }

    /// <summary>
    /// 发射该子弹的武器
    /// </summary>
    public Weapon Weapon { get; private set; }
    
    /// <summary>
    /// 发射该子弹的角色
    /// </summary>
    public Role Role { get; private set; }

    /// <summary>
    /// 最小伤害
    /// </summary>
    public int MinHarm { get; set; } = 4;
    
    /// <summary>
    /// 最大伤害
    /// </summary>
    public int MaxHarm { get; set; } = 4;

    // 最大飞行距离
    private float MaxDistance;

    // 子弹飞行速度
    private float FlySpeed;

    //当前子弹已经飞行的距离
    private float CurrFlyDistance = 0;

    public void Init(Weapon weapon, float speed, float maxDistance, Vector2 position, float rotation, uint targetLayer)
    {
        Weapon = weapon;
        Role = weapon.Master;
        CollisionArea.CollisionMask = targetLayer;
        CollisionArea.AreaEntered += OnArea2dEntered;
        
        //只有玩家使用该武器才能获得正常速度的子弹
        if (weapon.Master is Player)
        {
            FlySpeed = speed;
        }
        else
        {
            FlySpeed = speed * weapon.Attribute.AiBulletSpeedScale;
        }
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
            var packedScene = ResourceManager.Load<PackedScene>(ResourcePath.prefab_effect_BulletDisappear_tscn);
            var node = packedScene.Instantiate<Node2D>();
            node.GlobalPosition = GlobalPosition;
            node.AddToActivityRoot(RoomLayerEnum.YSortLayer);

            //计算子弹造成的伤害
            var damage = Utils.RandomRangeInt(MinHarm, MaxHarm);
            if (Role != null)
            {
                var d = damage;
                damage = Role.RoleState.CallCalcDamageEvent(damage);
                GD.Print($"原伤害: {d}, 计算伤害: {damage}");
            }
            role.CallDeferred(nameof(Role.Hurt), damage, Rotation);
            Destroy();
        }
    }
}