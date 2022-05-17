using Godot;

public class Player : Role
{
    /// <summary>
    /// 移动加速度
    /// </summary>
    public float Acceleration = 1500f;

    /// <summary>
    /// 移动速度
    /// </summary>
    public Vector2 Velocity = Vector2.Zero;

    /// <summary>
    /// 当前的枪
    /// </summary>
    public Gun CurrGun { get; private set; }

    [Export] public PackedScene GunPrefab;

    public override void _Ready()
    {
        base._Ready();

        //加载枪
        var gun = GunPrefab.Instance<Gun>();
        MountPoint.AddChild(gun);
        CurrGun = gun;

        var attr = new GunAttribute();
        attr.FiringSpeed = 420;
        attr.StartScatteringRange = 3;
        attr.FinalScatteringRange = 30;
        attr.ScatteringRangeAddValue = 15;
        attr.ScatteringRangeBackSpeed = 20;
        //连发
        attr.ContinuousShoot = true;
        //攻击距离
        attr.MinDistance = 500;
        attr.MaxDistance = 600;
        //发射子弹数量
        attr.MinFireBulletCount = 1;
        attr.MaxFireBulletCount = 1;
        //
        attr.UpliftAngle = 10;

        attr.BarrelLength = 10;
        attr.Sprite = "res://resource/sprite/gun/gun3.png";
        gun.Init(attr);
        gun.FireEvent += FireEvent_Func;

        RoomManager.Current.Cursor.TargetGun = gun;
    }

    public override void _Process(float delta)
    {
        base._Process(delta);

        Vector2 mousePos = GetGlobalMousePosition();
        //枪口跟随鼠标
        MountPoint.LookAt(mousePos);
        //脸的朝向
        if (mousePos.x > GlobalPosition.x && Face == FaceDirection.Left)
        {
            Face = FaceDirection.Right;
        }
        else if (mousePos.x < GlobalPosition.x && Face == FaceDirection.Right)
        {
            Face = FaceDirection.Left;
        }
        //攻击
        Attack();
    }

    public override void _PhysicsProcess(float delta)
    {
        base._PhysicsProcess(delta);
        Move(delta);
        //播放动画
        PlayAnim();
    }

    private void Move(float delta)
    {
        //角色移动
        var dir = new Vector2(
            Input.GetActionRawStrength("move_right") - Input.GetActionRawStrength("move_left"),
            Input.GetActionRawStrength("move_down") - Input.GetActionRawStrength("move_up")
        ).Normalized();
        if (dir.x * Velocity.x < 0)
        {
            Velocity.x = 0;
        }
        if (dir.y * Velocity.y < 0)
        {
            Velocity.y = 0;
        }
        Velocity = Velocity.MoveToward(dir * MoveSpeed, Acceleration * delta);
        Velocity = MoveAndSlide(Velocity);
    }

    private void Attack()
    {
        if (Input.IsActionPressed("fire"))
        {
            CurrGun.Trigger();
        }
    }

    // 播放动画
    private void PlayAnim()
    {
        if (Velocity != Vector2.Zero)
        {
            if ((Face == FaceDirection.Right && Velocity.x >= 0) || Face == FaceDirection.Left && Velocity.x <= 0) //向前走
            {
                AnimatedSprite.Animation = AnimatorNames.Run;
            }
            else if ((Face == FaceDirection.Right && Velocity.x < 0) || Face == FaceDirection.Left && Velocity.x > 0) //向后走
            {
                AnimatedSprite.Animation = AnimatorNames.ReverseRun;
            }
        }
        else
        {
            AnimatedSprite.Animation = AnimatorNames.Idle;
        }
    }

    //开火后
    private void FireEvent_Func(Gun gun)
    {

    }
}