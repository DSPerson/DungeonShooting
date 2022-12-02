using System;
using System.Collections.Generic;
using Godot;

/// <summary>
/// 角色基类
/// </summary>
public abstract class Role : ActivityObject
{
    /// <summary>
    /// 默认攻击对象层级
    /// </summary>
    public const uint DefaultAttackLayer = PhysicsLayer.Player | PhysicsLayer.Enemy | PhysicsLayer.Wall | PhysicsLayer.Props;
    
    /// <summary>
    /// 动画播放器
    /// </summary>
    public AnimationPlayer AnimationPlayer { get; private set; }
    
    /// <summary>
    /// 重写的纹理, 即将删除, 请直接更改 AnimatedSprite.Frames
    /// </summary>
    [Obsolete]
    public Texture OverrideTexture { get; protected set; }

    /// <summary>
    /// 移动速度
    /// </summary>
    public float MoveSpeed = 120f;

    /// <summary>
    /// 所属阵营
    /// </summary>
    public CampEnum Camp;

    /// <summary>
    /// 攻击目标的碰撞器所属层级, 数据源自于: PhysicsLayer
    /// </summary>
    public uint AttackLayer { get; set; } = PhysicsLayer.Wall;

    /// <summary>
    /// 携带的道具包裹
    /// </summary>
    public List<object> PropsPack { get; } = new List<object>();

    /// <summary>
    /// 角色携带的枪套
    /// </summary>
    public Holster Holster { get; }

    /// <summary>
    /// 武器挂载点
    /// </summary>
    public MountRotation MountPoint { get; private set; }
    /// <summary>
    /// 背后武器的挂载点
    /// </summary>
    public Position2D BackMountPoint { get; private set; }

    /// <summary>
    /// 互动碰撞区域
    /// </summary>
    public Area2D InteractiveArea { get; private set; }
    
    /// <summary>
    /// 脸的朝向
    /// </summary>
    public FaceDirection Face { get => _face; set => SetFace(value); }
    private FaceDirection _face;

    /// <summary>
    /// 是否启用角色移动, 如果禁用, 那么调用 CalcMove() 将不再有任何效果
    /// </summary>
    public bool EnableMove { get; set; } = true;
    
    /// <summary>
    /// 移动速度, 通过调用 CalcMove() 函数来移动
    /// </summary>
    public Vector2 Velocity { get; set; } = Vector2.Zero;
    
    /// <summary>
    /// 血量
    /// </summary>
    public int Hp
    {
        get => _hp;
        protected set
        {
            int temp = _hp;
            _hp = value;
            if (temp != _hp) OnChangeHp(_hp);
        }
    }
    private int _hp = 0;

    /// <summary>
    /// 最大血量
    /// </summary>
    public int MaxHp
    {
        get => _maxHp;
        protected set
        {
            int temp = _maxHp;
            _maxHp = value;
            if (temp != _maxHp) OnChangeMaxHp(_maxHp);
        }
    }
    private int _maxHp = 0;
    
    /// <summary>
    /// 当前护盾值
    /// </summary>
    public int Shield
    {
        get => _shield;
        protected set
        {
            int temp = _shield;
            _shield = value;
            if (temp != _shield) OnChangeShield(_shield);
        }
    }
    private int _shield = 0;

    /// <summary>
    /// 最大护盾值
    /// </summary>
    public int MaxShield
    {
        get => _maxShield;
        protected set
        {
            int temp = _maxShield;
            _maxShield = value;
            if (temp != _maxShield) OnChangeMaxShield(_maxShield);
        }
    }
    private int _maxShield = 0;

    /// <summary>
    /// 当前角色所看向的对象, 也就是枪口指向的对象
    /// </summary>
    public ActivityObject LookTarget { get; set; }

    //初始缩放
    private Vector2 _startScale;
    //所有角色碰撞的道具
    private readonly List<ActivityObject> _interactiveItemList = new List<ActivityObject>();

    private CheckInteractiveResult _tempResultData;

    /// <summary>
    /// 可以互动的道具
    /// </summary>
    protected ActivityObject InteractiveItem { get; private set; }

    /// <summary>
    /// 当血量改变时调用
    /// </summary>
    protected virtual void OnChangeHp(int hp)
    {
    }

    /// <summary>
    /// 当最大血量改变时调用
    /// </summary>
    protected virtual void OnChangeMaxHp(int maxHp)
    {
    }
    
    /// <summary>
    /// 护盾值改变时调用
    /// </summary>
    protected virtual void OnChangeShield(int shield)
    {
    }

    /// <summary>
    /// 最大护盾值改变时调用
    /// </summary>
    protected virtual void OnChangeMaxShield(int maxShield)
    {
    }

    /// <summary>
    /// 当受伤时调用
    /// </summary>
    /// <param name="damage">受到的伤害</param>
    protected virtual void OnHit(int damage)
    {
    }

    /// <summary>
    /// 当可互动的物体改变时调用, result 参数为 null 表示变为不可互动
    /// </summary>
    /// <param name="result">检测是否可互动时的返回值</param>
    protected virtual void ChangeInteractiveItem(CheckInteractiveResult result)
    {
    }

    /// <summary>
    /// 死亡时调用
    /// </summary>
    protected virtual void OnDie()
    {
    }
    
    public Role() : this(ResourcePath.prefab_role_Role_tscn)
    {
    }
    
    public Role(string scenePath) : base(scenePath)
    {
        Holster = new Holster(this);
    }
    
    public override void _Ready()
    {
        base._Ready();
        AnimationPlayer = GetNode<AnimationPlayer>("AnimationPlayer");
        _startScale = Scale;
        MountPoint = GetNode<MountRotation>("MountPoint");
        MountPoint.Master = this;
        BackMountPoint = GetNode<Position2D>("BackMountPoint");
        //即将删除
        if (OverrideTexture != null)
        {
            // 更改纹理
            ChangeFrameTexture(AnimatorNames.Idle, AnimatedSprite);
            ChangeFrameTexture(AnimatorNames.Run, AnimatedSprite);
            ChangeFrameTexture(AnimatorNames.ReverseRun, AnimatedSprite);
        }
        Face = FaceDirection.Right;

        //连接互动物体信号
        InteractiveArea = GetNode<Area2D>("InteractiveArea");
        InteractiveArea.Connect("area_entered", this, nameof(_OnPropsEnter));
        InteractiveArea.Connect("area_exited", this, nameof(_OnPropsExit));
    }

    public override void _Process(float delta)
    {
        base._Process(delta);
        
        //看向目标
        if (LookTarget != null)
        {
            Vector2 pos = LookTarget.GlobalPosition;
            //脸的朝向
            var gPos = GlobalPosition;
            if (pos.x > gPos.x && Face == FaceDirection.Left)
            {
                Face = FaceDirection.Right;
            }
            else if (pos.x < gPos.x && Face == FaceDirection.Right)
            {
                Face = FaceDirection.Left;
            }
            //枪口跟随目标
            MountPoint.SetLookAt(pos);
        }
        
        //检查可互动的道具
        bool findFlag = false;
        for (int i = 0; i < _interactiveItemList.Count; i++)
        {
            var item = _interactiveItemList[i];
            if (item == null)
            {
                _interactiveItemList.RemoveAt(i--);
            }
            else
            {
                //找到可互动的道具了
                if (!findFlag)
                {
                    var result = item.CheckInteractive(this);
                    if (result.CanInteractive) //可以互动
                    {
                        findFlag = true;
                        if (InteractiveItem != item) //更改互动物体
                        {
                            InteractiveItem = item;
                            ChangeInteractiveItem(result);
                        }
                        else if (result.ShowIcon != _tempResultData.ShowIcon) //切换状态
                        {
                            ChangeInteractiveItem(result);
                        }
                    }
                    _tempResultData = result;
                }
            }
        }
        //没有可互动的道具
        if (!findFlag && InteractiveItem != null)
        {
            InteractiveItem = null;
            ChangeInteractiveItem(null);
        }
    }

    /// <summary>
    /// 使角色看向指定的坐标,
    /// 注意, 调用该函数会清空 LookTarget, 因为拥有 LookTarget 时也会每帧更新玩家视野位置
    /// </summary>
    /// <param name="pos"></param>
    public void LookTargetPosition(Vector2 pos)
    {
        LookTarget = null;
        //脸的朝向
        var gPos = GlobalPosition;
        if (pos.x > gPos.x && Face == FaceDirection.Left)
        {
            Face = FaceDirection.Right;
        }
        else if (pos.x < gPos.x && Face == FaceDirection.Right)
        {
            Face = FaceDirection.Left;
        }
        //枪口跟随目标
        MountPoint.SetLookAt(pos);
    }
    
    /// <summary>
    /// 判断指定坐标是否在角色视野方向
    /// </summary>
    public bool IsPositionInForward(Vector2 pos)
    {
        var gps = GlobalPosition;
        return (Face == FaceDirection.Left && pos.x <= gps.x) ||
               (Face == FaceDirection.Right && pos.x >= gps.x);
    }

    /// <summary>
    /// 计算角色移动
    /// </summary>
    public virtual void CalcMove(float delta)
    {
        if (EnableMove && Velocity != Vector2.Zero)
        {
            Velocity = MoveAndSlide(Velocity);
        }
    }

    /// <summary>
    /// 拾起一个武器, 并且切换到这个武器, 返回是否成功拾取
    /// </summary>
    /// <param name="weapon">武器对象</param>
    public virtual bool PickUpWeapon(Weapon weapon)
    {
        if (Holster.PickupWeapon(weapon) != -1)
        {
            //从可互动队列中移除
            _interactiveItemList.Remove(weapon);
            return true;
        }

        return false;
    }

    /// <summary>
    /// 切换到下一个武器
    /// </summary>
    public virtual void ExchangeNext()
    {
        Holster.ExchangeNext();
    }

    /// <summary>
    /// 切换到上一个武器
    /// </summary>
    public virtual void ExchangePrev()
    {
        Holster.ExchangePrev();
    }

    /// <summary>
    /// 扔掉当前使用的武器, 切换到上一个武器
    /// </summary>
    public virtual void ThrowWeapon()
    {
        var weapon = Holster.RemoveWeapon(Holster.ActiveIndex);
        //播放抛出效果
        if (weapon != null)
        {
            weapon.ThrowWeapon(this);
        }
    }

    /// <summary>
    /// 返回是否存在可互动的物体
    /// </summary>
    public bool HasInteractive()
    {
        return InteractiveItem != null;
    }

    /// <summary>
    /// 触发与碰撞的物体互动, 并返回与其互动的物体
    /// </summary>
    public ActivityObject TriggerInteractive()
    {
        if (HasInteractive())
        {
            var item = InteractiveItem;
            item.Interactive(this);
            return item;
        }
        return null;
    }

    /// <summary>
    /// 触发换弹
    /// </summary>
    public virtual void Reload()
    {
        if (Holster.ActiveWeapon != null)
        {
            Holster.ActiveWeapon.Reload();
        }
    }

    /// <summary>
    /// 触发攻击
    /// </summary>
    public virtual void Attack()
    {
        if (Holster.ActiveWeapon != null)
        {
            Holster.ActiveWeapon.Trigger();
        }
    }

    /// <summary>
    /// 受到伤害
    /// </summary>
    /// <param name="damage">伤害的量</param>
    public virtual void Hurt(int damage)
    {
        Hp -= damage;
        AnimationPlayer.Stop();
        AnimationPlayer.Play("hit");
        OnHit(damage);
    }

    /// <summary>
    /// 设置脸的朝向
    /// </summary>
    private void SetFace(FaceDirection face)
    {
        if (_face != face)
        {
            _face = face;
            if (face == FaceDirection.Right)
            {
                RotationDegrees = 0;
                Scale = _startScale;
            }
            else
            {
                RotationDegrees = 180;
                Scale = new Vector2(_startScale.x, -_startScale.y);
            }
        }
    }

    /// <summary>
    /// 更改指定动画的纹理, 即将删除
    /// </summary>
    [Obsolete]
    private void ChangeFrameTexture(string anim, AnimatedSprite animatedSprite)
    {
        SpriteFrames spriteFrames = animatedSprite.Frames;
        if (spriteFrames != null)
        {
            int count = spriteFrames.GetFrameCount(anim);
            for (int i = 0; i < count; i++)
            {
                AtlasTexture temp = spriteFrames.GetFrame(anim, i) as AtlasTexture;
                temp.Atlas = OverrideTexture;
            }
        }
    }

    /// <summary>
    /// 连接信号: InteractiveArea.area_entered
    /// 与物体碰撞
    /// </summary>
    private void _OnPropsEnter(Area2D other)
    {
        ActivityObject propObject = other.AsActivityObject();
        if (propObject != null)
        {
            if (!_interactiveItemList.Contains(propObject))
            {
                _interactiveItemList.Add(propObject);
            }
        }
    }

    /// <summary>
    /// 连接信号: InteractiveArea.area_exited
    /// 物体离开碰撞区域
    /// </summary>
    private void _OnPropsExit(Area2D other)
    {
        ActivityObject propObject = other.AsActivityObject();
        if (propObject != null)
        {
            if (_interactiveItemList.Contains(propObject))
            {
                _interactiveItemList.Remove(propObject);
            }
            if (InteractiveItem == propObject)
            {
                InteractiveItem = null;
                ChangeInteractiveItem(null);
            }
        }
    }
}