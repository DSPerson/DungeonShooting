using Godot;


/// <summary>
/// 玩家角色基类, 所有角色都必须继承该类
/// </summary>
[Tool, GlobalClass]
public partial class Player : Role
{
    /// <summary>
    /// 获取当前操作的角色
    /// </summary>
    public static Player Current { get; private set; }
    
    /// <summary>
    /// 移动加速度
    /// </summary>
    public float Acceleration { get; set; } = 1500f;
    
    /// <summary>
    /// 移动摩擦力
    /// </summary>
    public float Friction { get; set; } = 800f;

    /// <summary>
    /// 设置当前操作的玩家对象
    /// </summary>
    public static void SetCurrentPlayer(Player player)
    {
        Current = player;
        //设置相机和鼠标跟随玩家
        GameCamera.Main.SetFollowTarget(player);
        GameApplication.Instance.Cursor.SetMountRole(player);
    }
    
    public override void OnInit()
    {
        base.OnInit();
        
        AttackLayer = PhysicsLayer.Wall | PhysicsLayer.Props | PhysicsLayer.Enemy;
        Camp = CampEnum.Camp1;

        //让相机跟随玩家
        // var remoteTransform = new RemoteTransform2D();
        // AddChild(remoteTransform);
        // MainCamera.Main.GlobalPosition = GlobalPosition;
        // MainCamera.Main.ResetSmoothing();
        // remoteTransform.RemotePath = remoteTransform.GetPathTo(MainCamera.Main);

        MaxHp = 6;
        Hp = 6;
        MaxShield = 2;
        Shield = 2;

        // debug用
        // Acceleration = 3000;
        // Friction = 3000;
        // MoveSpeed = 500;
        // CollisionLayer = 0;
        // CollisionMask = 0;
        // GameCamera.Main.Zoom = new Vector2(0.5f, 0.5f);
    }

    protected override void Process(float delta)
    {
        if (IsDie)
        {
            return;
        }
        base.Process(delta);
        //脸的朝向
        if (LookTarget == null)
        {
            var gPos = GlobalPosition;
            Vector2 mousePos = InputManager.CursorPosition;
            if (mousePos.X > gPos.X && Face == FaceDirection.Left)
            {
                Face = FaceDirection.Right;
            }
            else if (mousePos.X < gPos.X && Face == FaceDirection.Right)
            {
                Face = FaceDirection.Left;
            }
            //枪口跟随鼠标
            MountPoint.SetLookAt(mousePos);
        }

        if (InputManager.Exchange) //切换武器
        {
            ExchangeNext();
        }
        else if (InputManager.Throw) //扔掉武器
        {
            ThrowWeapon();

            // //测试用的, 所有敌人也扔掉武器
            // if (Affiliation != null)
            // {
            //     var enemies = Affiliation.FindIncludeItems(o =>
            //     {
            //         return o.CollisionWithMask(PhysicsLayer.Enemy);
            //     });
            //     foreach (var activityObject in enemies)
            //     {
            //         if (activityObject is Enemy enemy)
            //         {
            //             enemy.ThrowWeapon();
            //         }
            //     }
            // }
        }
        else if (InputManager.Interactive) //互动物体
        {
            TriggerInteractive();
        }
        else if (InputManager.Reload) //换弹
        {
            Reload();
        }
        if (InputManager.Fire) //开火
        {
            Attack();
        }

        if (Input.IsKeyPressed(Key.P))
        {
            Hurt(1000, 0);
        }
    }

    protected override void PhysicsProcess(float delta)
    {
        if (IsDie)
        {
            return;
        }

        base.PhysicsProcess(delta);
        HandleMoveInput(delta);
        //播放动画
        PlayAnim();
    }

    protected override int OnHandlerHurt(int damage)
    {
        //修改受到的伤害, 每次只受到1点伤害
        return 1;
    }

    protected override void OnChangeHp(int hp)
    {
        //GameApplication.Instance.Ui.SetHp(hp);
        EventManager.EmitEvent(EventEnum.OnPlayerHpChange, hp);
    }

    protected override void OnChangeMaxHp(int maxHp)
    {
        //GameApplication.Instance.Ui.SetMaxHp(maxHp);
        EventManager.EmitEvent(EventEnum.OnPlayerMaxHpChange, maxHp);
    }

    protected override void ChangeInteractiveItem(CheckInteractiveResult result)
    {
        //派发互动对象改变事件
        EventManager.EmitEvent(EventEnum.OnPlayerChangeInteractiveItem, result);
    }

    protected override void OnChangeShield(int shield)
    {
        //GameApplication.Instance.Ui.SetShield(shield);
        EventManager.EmitEvent(EventEnum.OnPlayerShieldChange, shield);
    }

    protected override void OnChangeMaxShield(int maxShield)
    {
        //GameApplication.Instance.Ui.SetMaxShield(maxShield);
        EventManager.EmitEvent(EventEnum.OnPlayerMaxShieldChange, maxShield);
    }

    protected override void OnDie()
    {
        GameCamera.Main.SetFollowTarget(null);
        BasisVelocity = Vector2.Zero;
        MoveController.ClearForce();
        UiManager.Open_Settlement();
        //GameApplication.Instance.World.ProcessMode = ProcessModeEnum.WhenPaused;
    }

    //处理角色移动的输入
    private void HandleMoveInput(float delta)
    {
        //角色移动
        Vector2 dir = InputManager.MoveAxis;
        // 移动. 如果移动的数值接近0(是用 摇杆可能出现 方向 可能会出现浮点)，就friction的值 插值 到 0
        // 如果 有输入 就以当前速度，用acceleration 插值到 对应方向 * 最大速度
        if (Mathf.IsZeroApprox(dir.X))
        {
            BasisVelocity = new Vector2(Mathf.MoveToward(BasisVelocity.X, 0, Friction * delta), BasisVelocity.Y);
        }
        else
        {
            BasisVelocity = new Vector2(Mathf.MoveToward(BasisVelocity.X, dir.X * MoveSpeed, Acceleration * delta),
                BasisVelocity.Y);
        }

        if (Mathf.IsZeroApprox(dir.Y))
        {
            BasisVelocity = new Vector2(BasisVelocity.X, Mathf.MoveToward(BasisVelocity.Y, 0, Friction * delta));
        }
        else
        {
            BasisVelocity = new Vector2(BasisVelocity.X,
                Mathf.MoveToward(BasisVelocity.Y, dir.Y * MoveSpeed, Acceleration * delta));
        }
    }

    // 播放动画
    private void PlayAnim()
    {
        if (BasisVelocity != Vector2.Zero)
        {
            if ((Face == FaceDirection.Right && BasisVelocity.X >= 0) || Face == FaceDirection.Left && BasisVelocity.X <= 0) //向前走
            {
                AnimatedSprite.Play(AnimatorNames.Run);
            }
            else if ((Face == FaceDirection.Right && BasisVelocity.X < 0) || Face == FaceDirection.Left && BasisVelocity.X > 0) //向后走
            {
                AnimatedSprite.Play(AnimatorNames.ReverseRun);
            }
        }
        else
        {
            AnimatedSprite.Play(AnimatorNames.Idle);
        }
    }
}