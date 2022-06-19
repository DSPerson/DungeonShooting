using Godot;
using System;

/// <summary>
/// 枪的基类
/// </summary>
public abstract class Gun : Node2D
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
    public Sprite GunSprite { get; private set; }

    /// <summary>
    /// 动画播放器
    /// </summary>
    /// <value></value>
    public AnimationPlayer AnimationPlayer { get; private set; }

    /// <summary>
    /// 枪攻击的目标阵营
    /// </summary>
    public CampEnum TargetCamp { get; set; }

    /// <summary>
    /// 该武器的拥有者
    /// </summary>
    public Role Master { get; private set; }

    /// <summary>
    /// 枪管的开火点
    /// </summary>
    public Position2D FirePoint { get; private set; }
    /// <summary>
    /// 枪管的原点
    /// </summary>
    public Position2D OriginPoint { get; private set; }
    /// <summary>
    /// 弹壳抛出的点
    /// </summary>
    public Position2D ShellPoint { get; private set; }
    /// <summary>
    /// 枪的当前散射半径
    /// </summary>
    public float CurrScatteringRange { get; private set; } = 0;

    //是否按下
    private bool triggerFlag = false;
    //扳机计时器
    private float triggerTimer = 0;
    //开火前延时时间
    private float delayedTime = 0;
    //开火间隙时间
    private float fireInterval = 0;
    //开火枪口角度
    private float fireAngle = 0;
    //攻击冷却计时
    private float attackTimer = 0;
    //攻击状态
    private bool attackFlag = false;
    //按下的时间
    private float downTimer = 0;
    //松开的时间
    private float upTimer = 0;
    //连发次数
    private float continuousCount = 0;
    private bool continuousShootFlag = false;

    //状态 0 在地上, 1 被拾起
    private int _state = 0;

    public override void _Process(float delta)
    {
        if (Master == null) //这把武器被扔在地上
        {

        }
        else if (Master.Holster.ActiveGun != this) //当前武器没有被使用
        {
            triggerTimer = triggerTimer > 0 ? triggerTimer - delta : 0;
            triggerFlag = false;
            attackFlag = false;
            attackTimer = attackTimer > 0 ? attackTimer - delta : 0;
            continuousCount = 0;
            delayedTime = 0;
        }
        else //正在使用中
        {
            if (triggerFlag)
            {
                if (upTimer > 0) //第一帧按下扳机
                {
                    upTimer = 0;
                    DownTrigger();
                }
                downTimer += delta;
            }
            else
            {
                if (downTimer > 0) //第一帧松开扳机
                {
                    downTimer = 0;
                    UpTriggern();
                }
                upTimer += delta;
            }

            // 攻击的计时器
            if (attackTimer > 0)
            {
                attackTimer -= delta;
                if (attackTimer < 0)
                {
                    delayedTime += attackTimer;
                    attackTimer = 0;
                }
            }
            else if (delayedTime > 0) //攻击延时
            {
                delayedTime -= delta;
                if (attackTimer < 0)
                {
                    delayedTime = 0;
                }
            }

            //连发判断
            if (continuousCount > 0 && delayedTime <= 0 && attackTimer <= 0)
            {
                TriggernFire();
            }

            if (!attackFlag && attackTimer <= 0)
            {
                CurrScatteringRange = Mathf.Max(CurrScatteringRange - Attribute.ScatteringRangeBackSpeed * delta, Attribute.StartScatteringRange);
            }
            triggerTimer = triggerTimer > 0 ? triggerTimer - delta : 0;
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
    }

    public void Init(GunAttribute attribute)
    {
        if (_attribute != null)
        {
            throw new Exception("当前武器已经初始化过了!");
        }

        GunSprite = GetNode<Sprite>("GunSprite");
        FirePoint = GetNode<Position2D>("FirePoint");
        OriginPoint = GetNode<Position2D>("OriginPoint");
        ShellPoint = GetNode<Position2D>("ShellPoint");
        AnimationPlayer = GetNode<AnimationPlayer>("AnimationPlayer");

        Attribute = attribute;
        //更新图片
        GunSprite.Texture = attribute.Sprite;
        GunSprite.Position = Attribute.CenterPosition;
        //开火位置
        FirePoint.Position = new Vector2(attribute.FirePosition.x, -attribute.FirePosition.y);
        OriginPoint.Position = new Vector2(0, -attribute.FirePosition.y);

        Init();
    }

    /// <summary>
    /// 扳机函数, 调用即视为扣动扳机
    /// </summary>
    public void Trigger()
    {
        //是否第一帧按下
        var justDown = downTimer == 0;
        //是否能发射
        var flag = false;
        if (continuousCount <= 0) //不能处于连发状态下
        {
            if (Attribute.ContinuousShoot) //自动射击
            {
                if (triggerTimer > 0)
                {
                    if (continuousShootFlag)
                    {
                        flag = true;
                    }
                }
                else
                {
                    flag = true;
                    if (delayedTime <= 0 && attackTimer <= 0)
                    {
                        continuousShootFlag = true;
                    }
                }
            }
            else //半自动
            {
                if (justDown && triggerTimer <= 0)
                {
                    flag = true;
                }
            }
        }

        if (flag)
        {
            if (justDown)
            {
                //开火前延时
                delayedTime = Attribute.DelayedTime;
                //扳机按下间隔
                triggerTimer = Attribute.TriggerInterval;
                //连发数量
                if (!Attribute.ContinuousShoot)
                {
                    continuousCount = MathUtils.RandRangeInt(Attribute.MinContinuousCount, Attribute.MaxContinuousCount);
                }
            }
            if (delayedTime <= 0 && attackTimer <= 0)
            {
                TriggernFire();
            }
            attackFlag = true;
        }
        triggerFlag = true;
    }

    /// <summary>
    /// 刚按下扳机
    /// </summary>
    private void DownTrigger()
    {

    }

    /// <summary>
    /// 刚松开扳机
    /// </summary>
    private void UpTriggern()
    {
        continuousShootFlag = false;
        if (delayedTime > 0)
        {
            continuousCount = 0;
        }
    }

    /// <summary>
    /// 触发开火
    /// </summary>
    private void TriggernFire()
    {
        continuousCount = continuousCount > 0 ? continuousCount - 1 : 0;
        fireInterval = 60 / Attribute.StartFiringSpeed;
        attackTimer += fireInterval;

        //触发开火函数
        Fire();

        //开火发射的子弹数量
        var bulletCount = MathUtils.RandRangeInt(Attribute.MaxFireBulletCount, Attribute.MinFireBulletCount);
        //枪口角度
        var angle = new Vector2(GameConfig.ScatteringDistance, CurrScatteringRange).Angle();

        //创建子弹
        for (int i = 0; i < bulletCount; i++)
        {
            //先算枪口方向
            Rotation = (float)GD.RandRange(-angle, angle);
            //发射子弹
            ShootBullet();
        }

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

    /// <summary>
    /// 初始化时调用
    /// </summary>
    protected abstract void Init();

    /// <summary>
    /// 单次开火时调用的函数
    /// </summary>
    protected abstract void Fire();

    /// <summary>
    /// 发射子弹时调用的函数, 每发射一枚子弹调用一次,
    /// 如果做霰弹枪效果, 一次开火发射5枚子弹, 则该函数调用5次
    /// </summary>
    protected abstract void ShootBullet();

    /// <summary>
    /// 当武器被拾起时调用
    /// </summary>
    /// <param name="master">拾起该武器的角色</param>
    protected abstract void OnPickUp(Role master);

    /// <summary>
    /// 当武器被扔掉时调用
    /// </summary>
    protected abstract void OnThrowOut();

    /// <summary>
    /// 当武器被激活时调用, 也就是使用当武器是调用
    /// </summary>
    protected abstract void OnActive();

    /// <summary>
    /// 当武器被收起时调用
    /// </summary>
    protected abstract void OnConceal();

    public void _PickUpGun(Role master)
    {
        Master = master;
        _state = 1;
        //握把位置
        GunSprite.Position = Attribute.HoldPosition;
        AnimationPlayer.Play("RESET");
        ZIndex = 0;
        OnPickUp(master);
    }

    public void _ThrowOutGun()
    {
        Master = null;
        _state = 0;
        GunSprite.Position = Attribute.CenterPosition;
        AnimationPlayer.Play("Floodlight");
        OnThrowOut();
    }

    public void _Active()
    {
        OnActive();
    }

    public void _Conceal()
    {
        OnConceal();
    }

    /// <summary>
    /// 实例化并返回子弹对象
    /// </summary>
    /// <param name="bulletPack">子弹的预制体</param>
    protected T CreateBullet<T>(PackedScene bulletPack, Vector2 globalPostion, float globalRotation, Node parent = null) where T : Node2D, IBullet
    {
        return (T)CreateBullet(bulletPack, globalPostion, globalRotation, parent);
    }


    protected IBullet CreateBullet(PackedScene bulletPack, Vector2 globalPostion, float globalRotation, Node parent = null)
    {
        // 实例化子弹
        Node2D bullet = bulletPack.Instance<Node2D>();
        // 设置坐标
        bullet.GlobalPosition = globalPostion;
        // 旋转角度
        bullet.GlobalRotation = globalRotation;
        if (parent == null)
        {
            RoomManager.Current.ItemRoot.AddChild(bullet);
        }
        else
        {
            parent.AddChild(bullet);
        }
        // 调用初始化
        IBullet result = (IBullet)bullet;
        result.Init(TargetCamp, this, null);
        return result;
    }
}