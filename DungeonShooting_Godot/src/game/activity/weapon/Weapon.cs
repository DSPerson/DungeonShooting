using Godot;
using System;
using System.Collections.Generic;
using Config;

/// <summary>
/// 武器的基类
/// </summary>
public abstract partial class Weapon : ActivityObject
{
    /// <summary>
    /// 开火回调事件
    /// </summary>
    public event Action<Weapon> FireEvent;

    /// <summary>
    /// 武器属性数据
    /// </summary>
    public ExcelConfig.Weapon Attribute => _weaponAttribute;
    private ExcelConfig.Weapon _weaponAttribute;
    private ExcelConfig.Weapon _playerWeaponAttribute;
    private ExcelConfig.Weapon _aiWeaponAttribute;

    /// <summary>
    /// 武器攻击的目标阵营
    /// </summary>
    public CampEnum TargetCamp { get; set; }

    /// <summary>
    /// 该武器的拥有者
    /// </summary>
    public Role Master { get; private set; }

    /// <summary>
    /// 当前弹夹弹药剩余量
    /// </summary>
    public int CurrAmmo { get; private set; }

    /// <summary>
    /// 剩余弹药量(备用弹药)
    /// </summary>
    public int ResidueAmmo { get; private set; }

    /// <summary>
    /// 武器管的开火点
    /// </summary>
    [Export, ExportFillNode]
    public Marker2D FirePoint { get; set; }

    /// <summary>
    /// 弹壳抛出的点
    /// </summary>
    [Export, ExportFillNode]
    public Marker2D ShellPoint { get; set; }

    /// <summary>
    /// 武器握把位置
    /// </summary>
    [Export, ExportFillNode]
    public Marker2D GripPoint { get; set; }
    
    /// <summary>
    /// 武器的当前散射半径
    /// </summary>
    public float CurrScatteringRange { get; private set; } = 0;

    /// <summary>
    /// 是否在换弹中
    /// </summary>
    /// <value></value>
    public bool Reloading { get; private set; } = false;

    /// <summary>
    /// 换弹计时器
    /// </summary>
    public float ReloadTimer { get; private set; } = 0;

    /// <summary>
    /// 换弹进度 (0 - 1)
    /// </summary>
    public float ReloadProgress
    {
        get
        {
            if (Attribute.AloneReload)
            {
                var num = 1f / Attribute.AmmoCapacity;
                return num * (Attribute.AmmoCapacity - CurrAmmo - 1) + num * (ReloadTimer / Attribute.ReloadTime);
            }

            return ReloadTimer / Attribute.ReloadTime;
        }
    }

    /// <summary>
    /// 返回是否在蓄力中,
    /// 注意, 属性仅在 Attribute.LooseShoot == false 时有正确的返回值, 否则返回 false
    /// </summary>
    public bool IsCharging => _looseShootFlag;

    /// <summary>
    /// 返回武器是否在武器袋中
    /// </summary>
    public bool IsInHolster => Master != null;

    /// <summary>
    /// 返回是否真正使用该武器
    /// </summary>
    public bool IsActive => Master != null && Master.Holster.ActiveWeapon == this;
    
    /// <summary>
    /// 动画播放器
    /// </summary>
    [Export, ExportFillNode]
    public AnimationPlayer AnimationPlayer { get; set; }

    /// <summary>
    /// 是否自动播放 SpriteFrames 的动画
    /// </summary>
    public bool IsAutoPlaySpriteFrames { get; set; } = true;

    //--------------------------------------------------------------------------------------------
    
    //是否按下
    private bool _triggerFlag = false;

    //扳机计时器
    private float _triggerTimer = 0;

    //开火前延时时间
    private float _delayedTime = 0;

    //开火间隙时间
    private float _fireInterval = 0;

    //开火武器口角度
    private float _fireAngle = 0;

    //攻击冷却计时
    private float _attackTimer = 0;

    //攻击状态
    private bool _attackFlag = false;

    //按下的时间
    private float _downTimer = 0;

    //松开的时间
    private float _upTimer = 0;

    //连发次数
    private float _continuousCount = 0;

    //连发状态记录
    private bool _continuousShootFlag = false;

    //松开扳机是否开火
    private bool _looseShootFlag = false;

    //蓄力攻击时长
    private float _chargeTime = 0;

    //是否需要重置武器数据
    private bool _dirtyFlag = false;

    //当前后坐力导致的偏移长度
    private float _currBacklashLength = 0;

    //临时存放动画精灵位置
    private Vector2 _tempAnimatedSpritePosition;
    
    // ----------------------------------------------
    private uint _tempLayer;

    private static bool _init = false;
    private static Dictionary<string, ExcelConfig.Weapon> _weaponAttributeMap =
        new Dictionary<string, ExcelConfig.Weapon>();

    /// <summary>
    /// 初始化武器属性数据
    /// </summary>
    public static void InitWeaponAttribute()
    {
        if (_init)
        {
            return;
        }

        _init = true;
        foreach (var weaponAttr in ExcelConfig.Weapon_List)
        {
            if (!string.IsNullOrEmpty(weaponAttr.WeaponId))
            {
                if (!_weaponAttributeMap.TryAdd(weaponAttr.WeaponId, weaponAttr))
                {
                    GD.PrintErr("发现重复注册的武器属性: " + weaponAttr.Id);
                }
            }
        }
    }
    
    private static ExcelConfig.Weapon _GetWeaponAttribute(string itemId)
    {
        if (_weaponAttributeMap.TryGetValue(itemId, out var attr))
        {
            return attr;
        }

        throw new Exception($"武器'{itemId}'没有在 Weapon 表中配置属性数据!");
    }
    
    public override void OnInit()
    {
        InitWeapon(_GetWeaponAttribute(ItemId));
    }

    /// <summary>
    /// 初始化武器属性
    /// </summary>
    public void InitWeapon(ExcelConfig.Weapon attribute)
    {
        _playerWeaponAttribute = attribute;
        _weaponAttribute = attribute;
        if (!string.IsNullOrEmpty(attribute.AiUseAttributeId))
        {
            _aiWeaponAttribute = ExcelConfig.Weapon_Map[attribute.AiUseAttributeId];
        }
        else
        {
            _aiWeaponAttribute = attribute;
        }

        if (Attribute.AmmoCapacity > Attribute.MaxAmmoCapacity)
        {
            Attribute.AmmoCapacity = Attribute.MaxAmmoCapacity;
            GD.PrintErr("弹夹的容量不能超过弹药上限, 武器id: " + ItemId);
        }
        //弹药量
        CurrAmmo = Attribute.AmmoCapacity;
        //剩余弹药量
        ResidueAmmo = Mathf.Min(Attribute.StandbyAmmoCapacity + CurrAmmo, Attribute.MaxAmmoCapacity) - CurrAmmo;
        
        ThrowCollisionSize = attribute.ThrowCollisionSize.AsVector2();
    }

    /// <summary>
    /// 单次开火时调用的函数
    /// </summary>
    protected abstract void OnFire();

    /// <summary>
    /// 发射子弹时调用的函数, 每发射一枚子弹调用一次,
    /// 如果做霰弹武器效果, 一次开火发射5枚子弹, 则该函数调用5次
    /// </summary>
    /// <param name="fireRotation">开火时枪口旋转角度</param>
    protected abstract void OnShoot(float fireRotation);
    
    /// <summary>
    /// 当按下扳机时调用
    /// </summary>
    protected virtual void OnDownTrigger()
    {
    }

    /// <summary>
    /// 当松开扳机时调用
    /// </summary>
    protected virtual void OnUpTrigger()
    {
    }

    /// <summary>
    /// 开始蓄力时调用, 
    /// 注意, 该函数仅在 Attribute.LooseShoot == false 时才能被调用
    /// </summary>
    protected virtual void OnStartCharge()
    {
    }

    /// <summary>
    /// 当开始换弹时调用
    /// </summary>
    protected virtual void OnReload()
    {
    }

    /// <summary>
    /// 当换弹完成时调用
    /// </summary>
    protected virtual void OnReloadFinish()
    {
    }

    /// <summary>
    /// 当武器被拾起时调用
    /// </summary>
    /// <param name="master">拾起该武器的角色</param>
    protected virtual void OnPickUp(Role master)
    {
    }

    /// <summary>
    /// 当武器从武器袋中移除时调用
    /// </summary>
    protected virtual void OnRemove()
    {
    }

    /// <summary>
    /// 当武器被激活时调用, 也就是使用当武器时调用
    /// </summary>
    protected virtual void OnActive()
    {
    }

    /// <summary>
    /// 当武器被收起时调用
    /// </summary>
    protected virtual void OnConceal()
    {
    }

    /// <summary>
    /// 射击时调用, 返回消耗弹药数量, 默认为1, 如果返回为 0, 则不消耗弹药
    /// </summary>
    protected virtual int UseAmmoCount()
    {
        return 1;
    }

    public override void EnterTree()
    {
        base.EnterTree();
        //收集落在地上的武器
        if (IsInGround())
        {
            World.Weapon_UnclaimedWeapons.Add(this);
        }
    }

    public override void ExitTree()
    {
        base.ExitTree();
        World.Weapon_UnclaimedWeapons.Remove(this);
    }

    protected override void Process(float delta)
    {
        //这把武器被扔在地上, 或者当前武器没有被使用
        if (Master == null || Master.Holster.ActiveWeapon != this)
        {
            //_triggerTimer
            _triggerTimer = _triggerTimer > 0 ? _triggerTimer - delta : 0;
            //攻击冷却计时
            _attackTimer = _attackTimer > 0 ? _attackTimer - delta : 0;
            //武器的当前散射半径
            CurrScatteringRange = Mathf.Max(CurrScatteringRange - Attribute.ScatteringRangeBackSpeed * delta,
                Attribute.StartScatteringRange);
            //松开扳机
            if (_triggerFlag || _downTimer > 0)
            {
                UpTrigger();
                _downTimer = 0;
            }
            
            _triggerFlag = false;

            //重置数据
            if (_dirtyFlag)
            {
                _dirtyFlag = false;
                Reloading = false;
                ReloadTimer = 0;
                _attackFlag = false;
                _continuousCount = 0;
                _delayedTime = 0;
                _upTimer = 0;
                _looseShootFlag = false;
                _chargeTime = 0;
            }
        }
        else //正在使用中
        {
            _dirtyFlag = true;
            //换弹
            if (Reloading)
            {
                ReloadTimer -= delta;
                if (ReloadTimer <= 0)
                {
                    ReloadSuccess();
                }
            }

            // 攻击的计时器
            if (_attackTimer > 0)
            {
                _attackTimer -= delta;
                if (_attackTimer < 0)
                {
                    _delayedTime += _attackTimer;
                    _attackTimer = 0;
                    //枪口默认角度
                    RotationDegrees = -Attribute.DefaultAngle;
                }
            }
            else if (_delayedTime > 0) //攻击延时
            {
                _delayedTime -= delta;
                if (_attackTimer < 0)
                {
                    _delayedTime = 0;
                }
            }
            
            //扳机判定
            if (_triggerFlag)
            {
                if (_looseShootFlag) //蓄力时长
                {
                    _chargeTime += delta;
                }

                _downTimer += delta;
                if (_upTimer > 0) //第一帧按下扳机
                {
                    DownTrigger();
                    _upTimer = 0;
                }
            }
            else
            {
                _upTimer += delta;
                if (_downTimer > 0) //第一帧松开扳机
                {
                    UpTrigger();
                    _downTimer = 0;
                }
            }

            //连发判断
            if (!_looseShootFlag && _continuousCount > 0 && _delayedTime <= 0 && _attackTimer <= 0)
            {
                //连发开火
                TriggerFire();
            }

            if (!_attackFlag && _attackTimer <= 0)
            {
                if (_upTimer >= Attribute.ScatteringRangeBackTime)
                {
                    CurrScatteringRange = Mathf.Max(CurrScatteringRange - Attribute.ScatteringRangeBackSpeed * delta,
                        Attribute.StartScatteringRange);
                }
            }

            _triggerTimer = _triggerTimer > 0 ? _triggerTimer - delta : 0;
            _triggerFlag = false;
            _attackFlag = false;
            
            //武器身回归
            //Position = Position.MoveToward(Vector2.Zero, Attribute.BacklashRegressionSpeed * delta).Rotated(Rotation);
            _currBacklashLength = Mathf.MoveToward(_currBacklashLength, 0, Attribute.BacklashRegressionSpeed * delta);
            Position = new Vector2(_currBacklashLength, 0).Rotated(Rotation);
            if (_attackTimer > 0)
            {
                RotationDegrees = Mathf.Lerp(
                    _fireAngle, -Attribute.DefaultAngle,
                    Mathf.Clamp((_fireInterval - _attackTimer) * Attribute.UpliftAngleRestore / _fireInterval, 0, 1)
                );
            }
        }
    }

    /// <summary>
    /// 返回武器是否在地上
    /// </summary>
    /// <returns></returns>
    public bool IsInGround()
    {
        return Master == null && GetParent() == GameApplication.Instance.World.NormalLayer;
    }
    
    /// <summary>
    /// 扳机函数, 调用即视为按下扳机
    /// </summary>
    public void Trigger()
    {
        //这一帧已经按过了, 不需要再按下
        if (_triggerFlag) return;
        
        //是否第一帧按下
        var justDown = _downTimer == 0;
        //是否能发射
        var flag = false;
        if (_continuousCount <= 0) //不能处于连发状态下
        {
            if (Attribute.ContinuousShoot) //自动射击
            {
                if (_triggerTimer > 0)
                {
                    if (_continuousShootFlag)
                    {
                        flag = true;
                    }
                }
                else
                {
                    flag = true;
                    if (_delayedTime <= 0 && _attackTimer <= 0)
                    {
                        _continuousShootFlag = true;
                    }
                }
            }
            else //半自动
            {
                if (justDown && _triggerTimer <= 0 && _attackTimer <= 0)
                {
                    flag = true;
                }
            }
        }

        if (flag)
        {
            var fireFlag = true; //是否能开火
            if (Reloading) //换弹中
            {
                if (CurrAmmo > 0 && Attribute.AloneReload && Attribute.AloneReloadCanShoot) //立即停止换弹
                {
                    Reloading = false;
                    ReloadTimer = 0;
                }
                else
                {
                    fireFlag = false;
                }
            }
            else if (CurrAmmo <= 0) //子弹不够
            {
                fireFlag = false;
                if (justDown)
                {
                    //第一帧按下, 触发换弹
                    Reload();
                }
            }

            if (fireFlag)
            {
                if (justDown)
                {
                    //开火前延时
                    _delayedTime = Attribute.DelayedTime;
                    //扳机按下间隔
                    _triggerTimer = Attribute.TriggerInterval;
                    //连发数量
                    if (!Attribute.ContinuousShoot)
                    {
                        _continuousCount =
                            Utils.RandomRangeInt(Attribute.MinContinuousCount, Attribute.MaxContinuousCount);
                    }
                }

                if (_delayedTime <= 0 && _attackTimer <= 0)
                {
                    if (Attribute.LooseShoot) //松发开火
                    {
                        _looseShootFlag = true;
                        OnStartCharge();
                    }
                    else
                    {
                        //开火
                        TriggerFire();
                    }
                }

                _attackFlag = true;
            }

        }

        _triggerFlag = true;
    }

    /// <summary>
    /// 返回是否按下扳机
    /// </summary>
    public bool IsPressTrigger()
    {
        return _triggerFlag;
    }
    
    /// <summary>
    /// 获取本次扳机按下的时长, 单位: 秒
    /// </summary>
    public float GetTriggerDownTime()
    {
        return _downTimer;
    }

    /// <summary>
    /// 获取扳机蓄力时长, 计算按下扳机后从可以开火到当前一共经过了多长时间, 可用于计算蓄力攻击
    /// 注意, 该函数仅在 Attribute.LooseShoot == false 时有正确的返回值, 否则返回 0
    /// </summary>
    public float GetTriggerChargeTime()
    {
        return _chargeTime;
    }
    
    /// <summary>
    /// 获取延时射击倒计时, 单位: 秒
    /// </summary>
    public float GetDelayedAttackTime()
    {
        return _delayedTime;
    }
    
    /// <summary>
    /// 刚按下扳机
    /// </summary>
    private void DownTrigger()
    {
        OnDownTrigger();
    }

    /// <summary>
    /// 刚松开扳机
    /// </summary>
    private void UpTrigger()
    {
        _continuousShootFlag = false;
        if (_delayedTime > 0)
        {
            _continuousCount = 0;
        }

        //松发开火执行
        if (_looseShootFlag)
        {
            _looseShootFlag = false;
            if (_chargeTime >= Attribute.MinChargeTime) //判断蓄力是否够了
            {
                TriggerFire();
            }
            else //不能攻击
            {
                _continuousCount = 0;
            }
            _chargeTime = 0;
        }

        OnUpTrigger();
    }

    /// <summary>
    /// 触发开火
    /// </summary>
    private void TriggerFire()
    {
        _continuousCount = _continuousCount > 0 ? _continuousCount - 1 : 0;

        //减子弹数量
        if (_playerWeaponAttribute != _weaponAttribute) //Ai使用该武器, 有一定概率不消耗弹药
        {
            if (Utils.RandomRangeFloat(0, 1) < _weaponAttribute.AiAmmoConsumptionProbability) //触发消耗弹药
            {
                CurrAmmo -= UseAmmoCount();
            }
        }
        else
        {
            CurrAmmo -= UseAmmoCount();
        }

        //开火间隙
        _fireInterval = 60 / Attribute.StartFiringSpeed;
        //攻击冷却
        _attackTimer += _fireInterval;

        //播放开火动画
        if (IsAutoPlaySpriteFrames)
        {
            PlaySpriteAnimation(AnimatorNames.Fire);
        }
        //触发开火函数
        OnFire();

        //开火发射的子弹数量
        var bulletCount = Utils.RandomRangeInt(Attribute.MaxFireBulletCount, Attribute.MinFireBulletCount);
        //武器口角度
        var angle = new Vector2(GameConfig.ScatteringDistance, CurrScatteringRange).Angle();

        //先算武器口方向
        var tempRotation = Utils.RandomRangeFloat(-angle, angle);
        var tempAngle = Mathf.RadToDeg(tempRotation);

        //开火时枪口角度
        var fireRotation = Mathf.DegToRad(Master.MountPoint.RealRotationDegrees) + tempRotation;
        //创建子弹
        for (int i = 0; i < bulletCount; i++)
        {
            //发射子弹
            OnShoot(fireRotation);
        }

        //当前的散射半径
        CurrScatteringRange = Mathf.Min(CurrScatteringRange + Attribute.ScatteringRangeAddValue,
            Attribute.FinalScatteringRange);
        //武器的旋转角度
        tempAngle -= Attribute.UpliftAngle;
        RotationDegrees = tempAngle;
        _fireAngle = tempAngle;
        
        //武器身位置
        var max = Mathf.Abs(Mathf.Max(Attribute.MaxBacklash, Attribute.MinBacklash));
        _currBacklashLength = Mathf.Clamp(
            _currBacklashLength - Utils.RandomRangeFloat(Attribute.MinBacklash, Attribute.MaxBacklash),
            -max, max
        );
        Position = new Vector2(_currBacklashLength, 0).Rotated(Rotation);

        if (FireEvent != null)
        {
            FireEvent(this);
        }
    }

    /// <summary>
    /// 获取武器攻击的目标层级
    /// </summary>
    /// <returns></returns>
    public uint GetAttackLayer()
    {
        return Master != null ? Master.AttackLayer : Role.DefaultAttackLayer;
    }
    
    /// <summary>
    /// 返回弹药是否到达上限
    /// </summary>
    public bool IsAmmoFull()
    {
        return CurrAmmo + ResidueAmmo >= Attribute.MaxAmmoCapacity;
    }

    /// <summary>
    /// 返回弹夹是否打空
    /// </summary>
    public bool IsAmmoEmpty()
    {
        return CurrAmmo == 0;
    }
    
    /// <summary>
    /// 返回是否弹药耗尽
    /// </summary>
    public bool IsTotalAmmoEmpty()
    {
        return CurrAmmo + ResidueAmmo == 0;
    }

    /// <summary>
    /// 强制修改当前弹夹弹药量
    /// </summary>
    public void SetCurrAmmo(int count)
    {
        CurrAmmo = Mathf.Clamp(count, 0, Attribute.AmmoCapacity);
    }

    /// <summary>
    /// 强制修改备用弹药量
    /// </summary>
    public void SetResidueAmmo(int count)
    {
        ResidueAmmo = Mathf.Clamp(count, 0, Attribute.MaxAmmoCapacity - CurrAmmo);
    }
    
    /// <summary>
    /// 强制修改弹药量, 优先改动备用弹药
    /// </summary>
    public void SetTotalAmmo(int total)
    {
        if (total < 0)
        {
            return;
        }
        var totalAmmo = CurrAmmo + ResidueAmmo;
        if (totalAmmo == total)
        {
            return;
        }
        
        if (total > totalAmmo) //弹药增加
        {
            ResidueAmmo = Mathf.Min(total - CurrAmmo, Attribute.MaxAmmoCapacity - CurrAmmo);
        }
        else //弹药减少
        {
            if (CurrAmmo < total)
            {
                ResidueAmmo = total - CurrAmmo;
            }
            else
            {
                CurrAmmo = total;
                ResidueAmmo = 0;
            }
        }
    }

    /// <summary>
    /// 拾起的弹药数量, 如果到达容量上限, 则返回拾取完毕后剩余的弹药数量
    /// </summary>
    /// <param name="count">弹药数量</param>
    private int PickUpAmmo(int count)
    {
        var num = ResidueAmmo;
        ResidueAmmo = Mathf.Min(ResidueAmmo + count, Attribute.MaxAmmoCapacity - CurrAmmo);
        return count - ResidueAmmo + num;
    }

    /// <summary>
    /// 触发换弹
    /// </summary>
    public void Reload()
    {
        if (CurrAmmo < Attribute.AmmoCapacity && ResidueAmmo > 0 && !Reloading)
        {
            Reloading = true;
            ReloadTimer = Attribute.ReloadTime;
            //播放换弹动画
            if (IsAutoPlaySpriteFrames)
            {
                PlaySpriteAnimation(AnimatorNames.Reload);
            }
            OnReload();
        }
    }

    /// <summary>
    /// 换弹计时器时间到, 执行换弹操作
    /// </summary>
    private void ReloadSuccess()
    {
        if (Attribute.AloneReload) //单独装填
        {
            if (ResidueAmmo >= Attribute.AloneReloadCount) //剩余子弹充足
            {
                if (CurrAmmo + Attribute.AloneReloadCount <= Attribute.AmmoCapacity)
                {
                    ResidueAmmo -= Attribute.AloneReloadCount;
                    CurrAmmo += Attribute.AloneReloadCount;
                }
                else //子弹满了
                {
                    var num = Attribute.AmmoCapacity - CurrAmmo;
                    CurrAmmo = Attribute.AmmoCapacity;
                    ResidueAmmo -= num;
                }
            }
            else if (ResidueAmmo != 0) //剩余子弹不足
            {
                if (ResidueAmmo + CurrAmmo <= Attribute.AmmoCapacity)
                {
                    CurrAmmo += ResidueAmmo;
                    ResidueAmmo = 0;
                }
                else //子弹满了
                {
                    var num = Attribute.AmmoCapacity - CurrAmmo;
                    CurrAmmo = Attribute.AmmoCapacity;
                    ResidueAmmo -= num;
                }
            }

            if (ResidueAmmo == 0 || CurrAmmo == Attribute.AmmoCapacity) //换弹结束
            {
                Reloading = false;
                ReloadTimer = 0;
                OnReloadFinish();
            }
            else
            {
                ReloadTimer = Attribute.ReloadTime;
                //播放换弹动画
                if (IsAutoPlaySpriteFrames)
                {
                    PlaySpriteAnimation(AnimatorNames.Reload);
                }
                OnReload();
            }
        }
        else //换弹结束
        {
            if (CurrAmmo + ResidueAmmo >= Attribute.AmmoCapacity)
            {
                ResidueAmmo -= Attribute.AmmoCapacity - CurrAmmo;
                CurrAmmo = Attribute.AmmoCapacity;
            }
            else
            {
                CurrAmmo += ResidueAmmo;
                ResidueAmmo = 0;
            }

            Reloading = false;
            ReloadTimer = 0;
            OnReloadFinish();
        }
    }
    
    //播放动画
    private void PlaySpriteAnimation(string name)
    {
        var spriteFrames = AnimatedSprite.SpriteFrames;
        if (spriteFrames != null && spriteFrames.HasAnimation(name))
        {
            AnimatedSprite.Play(name);
        }
    }

    public override CheckInteractiveResult CheckInteractive(ActivityObject master)
    {
        var result = new CheckInteractiveResult(this);

        if (master is Role roleMaster) //碰到角色
        {
            if (Master == null)
            {
                var masterWeapon = roleMaster.Holster.ActiveWeapon;
                //查找是否有同类型武器
                var index = roleMaster.Holster.FindWeapon(ItemId);
                if (index != -1) //如果有这个武器
                {
                    if (CurrAmmo + ResidueAmmo != 0) //子弹不为空
                    {
                        var targetWeapon = roleMaster.Holster.GetWeapon(index);
                        if (!targetWeapon.IsAmmoFull()) //背包里面的武器子弹未满
                        {
                            //可以互动拾起弹药
                            result.CanInteractive = true;
                            result.Message = Attribute.Name;
                            result.ShowIcon = ResourcePath.resource_sprite_ui_icon_icon_bullet_png;
                            return result;
                        }
                    }
                }
                else //没有武器
                {
                    if (roleMaster.Holster.CanPickupWeapon(this)) //能拾起武器
                    {
                        //可以互动, 拾起武器
                        result.CanInteractive = true;
                        result.Message = Attribute.Name;
                        result.ShowIcon = ResourcePath.resource_sprite_ui_icon_icon_pickup_png;
                        return result;
                    }
                    else if (masterWeapon != null && masterWeapon.Attribute.WeightType == Attribute.WeightType) //替换武器
                    {
                        //可以互动, 切换武器
                        result.CanInteractive = true;
                        result.Message = Attribute.Name;
                        result.ShowIcon = ResourcePath.resource_sprite_ui_icon_icon_replace_png;
                        return result;
                    }
                }
            }
        }

        return result;
    }

    public override void Interactive(ActivityObject master)
    {
        if (master is Role roleMaster) //与role互动
        {
            var holster = roleMaster.Holster;
            //查找是否有同类型武器
            var index = holster.FindWeapon(ItemId);
            if (index != -1) //如果有这个武器
            {
                if (CurrAmmo + ResidueAmmo == 0) //没有子弹了
                {
                    return;
                }

                var weapon = holster.GetWeapon(index);
                //子弹上限
                var maxCount = Attribute.MaxAmmoCapacity;
                //是否捡到子弹
                var flag = false;
                if (ResidueAmmo > 0 && weapon.CurrAmmo + weapon.ResidueAmmo < maxCount)
                {
                    var count = weapon.PickUpAmmo(ResidueAmmo);
                    if (count != ResidueAmmo)
                    {
                        ResidueAmmo = count;
                        flag = true;
                    }
                }

                if (CurrAmmo > 0 && weapon.CurrAmmo + weapon.ResidueAmmo < maxCount)
                {
                    var count = weapon.PickUpAmmo(CurrAmmo);
                    if (count != CurrAmmo)
                    {
                        CurrAmmo = count;
                        flag = true;
                    }
                }

                //播放互动效果
                if (flag)
                {
                    Throw(GlobalPosition, 0, Utils.RandomRangeInt(20, 50), Vector2.Zero, Utils.RandomRangeInt(-180, 180));
                }
            }
            else //没有武器
            {
                if (holster.PickupWeapon(this) == -1)
                {
                    //替换武器
                    roleMaster.ThrowWeapon();
                    roleMaster.PickUpWeapon(this);
                }
            }
        }
    }

    /// <summary>
    /// 获取当前武器真实的旋转角度(弧度制), 由于武器旋转时加入了旋转吸附, 所以需要通过该函数来来知道当前武器的真实旋转角度
    /// </summary>
    public float GetRealGlobalRotation()
    {
        return Mathf.DegToRad(Master.MountPoint.RealRotationDegrees) + Rotation;
    }

    /// <summary>
    /// 触发扔掉武器抛出的效果, 并不会管武器是否在武器袋中
    /// </summary>
    /// <param name="master">触发扔掉该武器的的角色</param>
    public void ThrowWeapon(Role master)
    {
        ThrowWeapon(master, master.GlobalPosition);
    }

    /// <summary>
    /// 触发扔掉武器抛出的效果, 并不会管武器是否在武器袋中
    /// </summary>
    /// <param name="master">触发扔掉该武器的的角色</param>
    /// <param name="startPosition">投抛起始位置</param>
    public void ThrowWeapon(Role master, Vector2 startPosition)
    {
        //阴影偏移
        ShadowOffset = new Vector2(0, 2);

        if (master.Face == FaceDirection.Left)
        {
            Scale *= new Vector2(1, -1);
        }

        var rotation = master.MountPoint.GlobalRotation;
        GlobalRotation = rotation;
        
        //继承role的移动速度
        InheritVelocity(master);

        startPosition -= GripPoint.Position.Rotated(rotation);
        var startHeight = -master.MountPoint.Position.Y;
        var direction = Mathf.RadToDeg(rotation) + Utils.RandomRangeInt(-20, 20);
        var velocity = new Vector2(20, 0).Rotated(direction * Mathf.Pi / 180);
        var yf = Utils.RandomRangeInt(50, 70);
        var rotate = Utils.RandomRangeInt(-60, 60);
        Throw(startPosition, startHeight, yf, velocity, rotate);
    }

    protected override void OnThrowStart()
    {
        //禁用碰撞
        //Collision.Disabled = true;
        AnimationPlayer.Play(AnimatorNames.Floodlight);
    }

    protected override void OnThrowOver()
    {
        //启用碰撞
        //Collision.Disabled = false;
        AnimationPlayer.Play(AnimatorNames.Floodlight);
    }

    /// <summary>
    /// 触发拾起到 Holster, 这个函数由 Holster 对象调用
    /// </summary>
    public void PickUpWeapon(Role master)
    {
        Master = master;
        if (master.IsAi)
        {
            _weaponAttribute = _aiWeaponAttribute;
        }
        else
        {
            _weaponAttribute = _playerWeaponAttribute;
        }
        //停止动画
        AnimationPlayer.Stop();
        //清除泛白效果
        SetBlendSchedule(0);
        ZIndex = 0;
        //禁用碰撞
        //Collision.Disabled = true;
        //精灵位置
        _tempAnimatedSpritePosition = AnimatedSprite.Position;
        var position = GripPoint.Position;
        AnimatedSprite.Position = new Vector2(-position.X, -position.Y);
        //修改层级
        _tempLayer = CollisionLayer;
        CollisionLayer = PhysicsLayer.InHand;
        //清除 Ai 拾起标记
        RemoveSign(SignNames.AiFindWeaponSign);
        OnPickUp(master);
    }

    /// <summary>
    /// 触发从 Holster 中移除, 这个函数由 Holster 对象调用
    /// </summary>
    public void RemoveAt()
    {
        Master = null;
        CollisionLayer = _tempLayer;
        _weaponAttribute = _playerWeaponAttribute;
        AnimatedSprite.Position = _tempAnimatedSpritePosition;
        //清除 Ai 拾起标记
        RemoveSign(SignNames.AiFindWeaponSign);
        OnRemove();
    }

    /// <summary>
    /// 触发启用武器
    /// </summary>
    public void Active()
    {
        //调整阴影
        ShadowOffset = new Vector2(0, Master.GlobalPosition.Y - GlobalPosition.Y);
        //枪口默认抬起角度
        RotationDegrees = -Attribute.DefaultAngle;
        ShowShadowSprite();
        OnActive();
    }

    /// <summary>
    /// 触发收起武器
    /// </summary>
    public void Conceal()
    {
        HideShadowSprite();
        OnConceal();
    }

    //-------------------------- ----- 子弹相关 -----------------------------

    /// <summary>
    /// 投抛弹壳的默认实现方式, shellId为弹壳id, 不需要前缀
    /// </summary>
    protected ActivityObject ThrowShell(string shellId)
    {
        var shellPosition = Master.MountPoint.Position + ShellPoint.Position;
        var startPos = ShellPoint.GlobalPosition;
        var startHeight = -shellPosition.Y;
        startPos.Y += startHeight;
        var direction = GlobalRotationDegrees + Utils.RandomRangeInt(-30, 30) + 180;
        var verticalSpeed = Utils.RandomRangeInt(60, 120);
        var velocity = new Vector2(Utils.RandomRangeInt(20, 60), 0).Rotated(direction * Mathf.Pi / 180);
        var rotate = Utils.RandomRangeInt(-720, 720);
        var shell = Create(shellId);
        shell.Rotation = Master.MountPoint.RealRotation;
        shell.InheritVelocity(Master);
        shell.Throw(startPos, startHeight, verticalSpeed, velocity, rotate);
        return shell;
    }
    
    //-------------------------------- Ai相关 -----------------------------

    /// <summary>
    /// 获取 Ai 对于该武器的评分, 评分越高, 代表 Ai 会越优先选择该武器, 如果为 -1, 则表示 Ai 不会使用该武器
    /// </summary>
    public float GetAiScore()
    {
        return 1;
    }
}