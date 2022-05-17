using Godot;
using System;

public class Gun : Area2D
{
    /// <summary>
    /// 开火回调事件
    /// </summary>
    public event Action<Gun> FireEvent;

    /// <summary>
    /// 属性数据
    /// </summary>
    public GunAttribute Attribute
    {
        get
        {
            if (_attribute == null)
            {
                throw new Exception("请先调用Init来初始化枪的属性");
            }
            return _attribute;
        }
        private set => _attribute = value;
    }
    private GunAttribute _attribute;

    /// <summary>
    /// 枪的图片
    /// </summary>
    public Sprite Sprite { get; private set; }

    /// <summary>
    /// 枪攻击的目标阵营
    /// </summary>
    public CampEnum TargetCamp { get; set; }
    /// <summary>
    /// 开火点
    /// </summary>
    public Position2D FirePoint { get; private set; }
    /// <summary>
    /// 原点
    /// </summary>
    public Position2D OriginPoint { get; private set; }

    /// <summary>
    /// 枪的当前散射半径
    /// </summary>
    public float CurrScatteringRange { get; private set; } = 0;

    //是否按下
    private bool triggerFlag = false;
    private float fireInterval = 0;
    private float fireAngle = 0;
    private float attackTimer = 0;
    private bool attackFlag = false;
    private int downFrame = 0;
    //子弹
    private PackedScene bulletPacked;

    public override void _EnterTree()
    {
        Sprite = GetNode<Sprite>("Sprite");
        FirePoint = GetNode<Position2D>("FirePoint");
        OriginPoint = GetNode<Position2D>("OriginPoint");
    }

    public override void _Process(float delta)
    {
        downFrame = triggerFlag ? downFrame : 0;
        // 攻击的计时器
        if (attackTimer > 0)
        {
            attackTimer -= delta;
        }
        else if (!attackFlag && attackTimer < 0)
        {
            attackTimer = 0;
        }

        if (!attackFlag && attackTimer <= 0)
        {
            CurrScatteringRange = Mathf.Max(CurrScatteringRange - Attribute.ScatteringRangeBackSpeed * delta, Attribute.StartScatteringRange);
        }
        triggerFlag = false;
        attackFlag = false;

        //枪身回归
        Position = Position.MoveToward(Vector2.Zero, 35 * delta);
        if (fireInterval == 0)
        {
            RotationDegrees = 0;
        }
        else
        {
            RotationDegrees = Mathf.Lerp(0, fireAngle, attackTimer / fireInterval);
        }
    }

    public void Init(GunAttribute attribute)
    {
        Attribute = attribute;
        //更新图片
        var texture = ResourceLoader.Load<Texture>(attribute.Sprite);
        Sprite.Texture = texture;
        //子弹
        bulletPacked = ResourceLoader.Load<PackedScene>(attribute.Bullet);
        //开火位置
        FirePoint.Position = new Vector2(attribute.BarrelLength, FirePoint.Position.y);
    }

    /// <summary>
    /// 扳机函数, 调用即视为扣动扳机
    /// </summary>
    public void Trigger()
    {
        //是否第一帧按下
        var justDown = downFrame++ == 0;

        if (Attribute.ContinuousShoot || justDown)
        {
            if (attackTimer <= 0)
            {
                fireInterval = 60 / Attribute.FiringSpeed;
                attackTimer += fireInterval;
                Fire();
                //当前的散射半径
                CurrScatteringRange = Mathf.Min(CurrScatteringRange + Attribute.ScatteringRangeAddValue, Attribute.FinalScatteringRange);
                //枪的旋转角度
                RotationDegrees -= Attribute.UpliftAngle;
                fireAngle = RotationDegrees;
                //枪身位置
                Position = new Vector2(Mathf.Max(-Attribute.MaxBacklash, Position.x - MathUtils.RandRange(Attribute.MinBacklash, Attribute.MaxBacklash)), Position.y);

                if (FireEvent != null)
                {
                    FireEvent(this);
                }
            }
            attackFlag = true;
        }
        triggerFlag = true;
    }

    protected virtual void Fire()
    {
        //开火发射的子弹数量
        var bulletCount = MathUtils.RandRangeInt(Attribute.MaxFireBulletCount, Attribute.MinFireBulletCount);
        //枪口角度
        var angle = new Vector2(GameConfig.ScatteringDistance, CurrScatteringRange).Angle();

        //创建子弹
        for (int i = 0; i < bulletCount; i++)
        {
            //先算枪口方向
            Rotation = (float)GD.RandRange(-angle, angle);

            //创建子弹
            var bullet = bulletPacked.Instance<Bullet>();
            bullet.GlobalPosition = FirePoint.GlobalPosition;
            bullet.Rotation = (FirePoint.GlobalPosition - OriginPoint.GlobalPosition).Angle();
            GetTree().CurrentScene.AddChild(bullet);

            //飞行距离
            var distance = MathUtils.RandRange(Attribute.MinDistance, Attribute.MaxDistance);
            //初始化子弹数据
            bullet.Init(TargetCamp, distance, Colors.White);
        }
    }
}