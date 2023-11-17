#region 基础敌人设计思路
/*
敌人有三种状态: 
状态1: 未发现玩家, 视野不可穿墙, 该状态下敌人移动比较规律, 移动速度较慢, 一旦玩家进入视野或者听到玩家枪声, 立刻切换至状态3, 该房间的敌人不能再回到状态1
状态2: 发现有玩家, 但不知道在哪, 视野不可穿墙, 该情况下敌人移动速度明显加快, 移动不规律, 一旦玩家进入视野或者听到玩家枪声, 立刻切换至状态3
状态3: 明确知道玩家的位置, 视野允许穿墙, 移动速度与状态2一致, 进入该状态时, 敌人之间会相互告知玩家所在位置, 并朝着玩家位置开火,
       如果有墙格挡, 则有一定概率继续开火, 一旦玩家立刻敌人视野超哥一段时间, 敌人自动切换为状态2

敌人状态1只存在于少数房间内, 比如特殊房间, 大部分情况下敌人应该是状态2, 或者玩家进入房间时就被敌人发现
*/
#endregion


using System;
using AdvancedState;
using Godot;
using NnormalState;
using AiAstonishedState = AdvancedState.AiAstonishedState;
using AiAttackState = AdvancedState.AiAttackState;
using AiFollowUpState = AdvancedState.AiFollowUpState;
using AiLeaveForState = AdvancedState.AiLeaveForState;
using AiNormalState = AdvancedState.AiNormalState;
using AiNotifyState = AdvancedState.AiNotifyState;
using AiSurroundState = AdvancedState.AiSurroundState;
using AiTailAfterState = AdvancedState.AiTailAfterState;

/// <summary>
/// 高级敌人，可以携带武器
/// </summary>
[Tool]
public partial class AdvancedEnemy : Enemy
{
    /// <summary>
    /// 角色携带的武器背包
    /// </summary>
    public Package<Weapon, AdvancedEnemy> WeaponPack { get; private set; }
    
    /// <summary>
    /// 武器挂载点
    /// </summary>
    [Export, ExportFillNode]
    public MountRotation MountPoint { get; set; }
    
    /// <summary>
    /// 近战碰撞检测区域
    /// </summary>
    [Export, ExportFillNode]
    public Area2D MeleeAttackArea { get; set; }
    
    /// <summary>
    /// 近战碰撞检测区域的碰撞器
    /// </summary>
    [Export, ExportFillNode]
    public CollisionPolygon2D MeleeAttackCollision { get; set; }

    /// <summary>
    /// 近战攻击时挥动武器的角度
    /// </summary>
    [Export]
    public float MeleeAttackAngle { get; set; } = 120;

    /// <summary>
    /// 武器挂载点是否始终指向目标
    /// </summary>
    public bool MountLookTarget { get; set; } = true;
    
    /// <summary>
    /// 背后武器的挂载点
    /// </summary>
    [Export, ExportFillNode]
    public Marker2D BackMountPoint { get; set; }
    
    /// <summary>
    /// 是否处于近战攻击中
    /// </summary>
    public bool IsMeleeAttack { get; private set; }
    
    //近战计时器
    private float _meleeAttackTimer = 0;
    
    
    /// <summary>
    /// 当拾起某个武器时调用
    /// </summary>
    protected virtual void OnPickUpWeapon(Weapon weapon)
    {
    }
    
    /// <summary>
    /// 当扔掉某个武器时调用
    /// </summary>
    protected virtual void OnThrowWeapon(Weapon weapon)
    {
    }

    /// <summary>
    /// 当切换到某个武器时调用
    /// </summary>
    protected virtual void OnExchangeWeapon(Weapon weapon)
    {
    }

    
    public override void OnInit()
    {
        base.OnInit();
        IsAi = true;
        StateController = AddComponent<StateController<AdvancedEnemy, AIAdvancedStateEnum>>();

        AttackLayer = PhysicsLayer.Wall | PhysicsLayer.Player;
        EnemyLayer = PhysicsLayer.Player;
        Camp = CampEnum.Camp2;

        RoleState.MoveSpeed = 20;

        MaxHp = 20;
        Hp = 20;

        //PathSign = new PathSign(this, PathSignLength, GameApplication.Instance.Node3D.Player);

        //注册Ai状态机
        StateController.Register(new AiNormalState());
        //StateController.Register(new AiProbeState());
        StateController.Register(new AiTailAfterState());
        StateController.Register(new AiFollowUpState());
        StateController.Register(new AiLeaveForState());
        StateController.Register(new AiSurroundState());
        StateController.Register(new AiFindAmmoState());
        StateController.Register(new AiAttackState());
        StateController.Register(new AiAstonishedState());
        StateController.Register(new AiNotifyState());
        
        //默认状态
        StateController.ChangeStateInstant(AIAdvancedStateEnum.AiNormal);
    }

    public override void EnterTree()
    {
        if (!World.Enemy_InstanceList.Contains(this))
        {
            World.Enemy_InstanceList.Add(this);
        }
    }

    public override void ExitTree()
    {
        World.Enemy_InstanceList.Remove(this);
    }

    protected override void OnDie()
    {
        //扔掉所有武器
        var weapons = WeaponPack.GetAndClearItem();
        for (var i = 0; i < weapons.Length; i++)
        {
            weapons[i].ThrowWeapon(this);
        }

        var effPos = Position + new Vector2(0, -Altitude);
        //血液特效
        var blood = ObjectManager.GetPoolItem<AutoDestroyParticles>(ResourcePath.prefab_effect_enemy_EnemyBloodEffect_tscn);
        blood.Position = effPos - new Vector2(0, 12);
        blood.AddToActivityRoot(RoomLayerEnum.NormalLayer);
        blood.PlayEffect();

        //创建敌人碎片
        var count = Utils.Random.RandomRangeInt(3, 6);
        for (var i = 0; i < count; i++)
        {
            var debris = Create(Ids.Id_effect0001);
            debris.PutDown(effPos, RoomLayerEnum.NormalLayer);
            debris.InheritVelocity(this);
        }
        
        //派发敌人死亡信号
        EventManager.EmitEvent(EventEnum.OnEnemyDie, this);
        Destroy();
    }

    protected override void Process(float delta)
    {
        base.Process(delta);
        if (IsDie)
        {
            return;
        }
        
        //看向目标
        if (LookTarget != null && MountLookTarget)
        {
            var pos = LookTarget.Position;
            LookPosition = pos;
            //脸的朝向
            var gPos = Position;
            if (pos.X > gPos.X && Face == FaceDirection.Left)
            {
                Face = FaceDirection.Right;
            }
            else if (pos.X < gPos.X && Face == FaceDirection.Right)
            {
                Face = FaceDirection.Left;
            }
            //枪口跟随目标
            MountPoint.SetLookAt(pos);
        }

        //拾起武器操作
        EnemyPickUpWeapon();
    }
    
    /// <summary>
    /// 当武器放到后背时调用, 用于设置武器位置和角度
    /// </summary>
    /// <param name="weapon">武器实例</param>
    /// <param name="index">放入武器背包的位置</param>
    public virtual void OnPutBackMount(Weapon weapon, int index)
    {
        if (index < 8)
        {
            if (index % 2 == 0)
            {
                weapon.Position = new Vector2(-4, 3);
                weapon.RotationDegrees = 90 - (index / 2f) * 20;
                weapon.Scale = new Vector2(-1, 1);
            }
            else
            {
                weapon.Position = new Vector2(4, 3);
                weapon.RotationDegrees = 270 + (index - 1) / 2f * 20;
                weapon.Scale = new Vector2(1, 1);
            }
        }
        else
        {
            weapon.Visible = false;
        }
    }

    /// <summary>
    /// 返回地上的武器是否有可以拾取的, 也包含没有被其他敌人标记的武器
    /// </summary>
    public bool CheckUsableWeaponInUnclaimed()
    {
        foreach (var unclaimedWeapon in World.Weapon_UnclaimedWeapons)
        {
            //判断是否能拾起武器, 条件: 相同的房间
            if (unclaimedWeapon.AffiliationArea == AffiliationArea)
            {
                if (!unclaimedWeapon.IsTotalAmmoEmpty())
                {
                    if (!unclaimedWeapon.HasSign(SignNames.AiFindWeaponSign))
                    {
                        return true;
                    }
                    else
                    {
                        //判断是否可以移除该标记
                        var enemy = unclaimedWeapon.GetSign<AdvancedEnemy>(SignNames.AiFindWeaponSign);
                        if (enemy == null || enemy.IsDestroyed) //标记当前武器的敌人已经被销毁
                        {
                            unclaimedWeapon.RemoveSign(SignNames.AiFindWeaponSign);
                            return true;
                        }
                        else if (!enemy.IsAllWeaponTotalAmmoEmpty()) //标记当前武器的敌人已经有新的武器了
                        {
                            unclaimedWeapon.RemoveSign(SignNames.AiFindWeaponSign);
                            return true;
                        }
                    }
                }
            }
        }

        return false;
    }
    
    /// <summary>
    /// 寻找可用的武器
    /// </summary>
    public Weapon FindTargetWeapon()
    {
        Weapon target = null;
        var position = Position;
        foreach (var weapon in World.Weapon_UnclaimedWeapons)
        {
            //判断是否能拾起武器, 条件: 相同的房间, 或者当前房间目前没有战斗, 或者不在战斗房间
            if (weapon.AffiliationArea == AffiliationArea)
            {
                //还有弹药
                if (!weapon.IsTotalAmmoEmpty())
                {
                    //查询是否有其他敌人标记要拾起该武器
                    if (weapon.HasSign(SignNames.AiFindWeaponSign))
                    {
                        var enemy = weapon.GetSign<AdvancedEnemy>(SignNames.AiFindWeaponSign);
                        if (enemy == this) //就是自己标记的
                        {

                        }
                        else if (enemy == null || enemy.IsDestroyed) //标记当前武器的敌人已经被销毁
                        {
                            weapon.RemoveSign(SignNames.AiFindWeaponSign);
                        }
                        else if (!enemy.IsAllWeaponTotalAmmoEmpty()) //标记当前武器的敌人已经有新的武器了
                        {
                            weapon.RemoveSign(SignNames.AiFindWeaponSign);
                        }
                        else //放弃这把武器
                        {
                            continue;
                        }
                    }

                    if (target == null) //第一把武器
                    {
                        target = weapon;
                    }
                    else if (target.Position.DistanceSquaredTo(position) >
                             weapon.Position.DistanceSquaredTo(position)) //距离更近
                    {
                        target = weapon;
                    }
                }
            }
        }

        return target;
    }

    /// <summary>
    /// 获取武器攻击范围 (最大距离值与最小距离的中间值)
    /// </summary>
    /// <param name="weight">从最小到最大距离的过渡量, 0 - 1, 默认 0.5</param>
    public float GetWeaponRange(float weight = 0.5f)
    {
        if (WeaponPack.ActiveItem != null)
        {
            var attribute = WeaponPack.ActiveItem.Attribute;
            return Mathf.Lerp(Utils.GetConfigRangeStart(attribute.Bullet.DistanceRange), Utils.GetConfigRangeEnd(attribute.Bullet.DistanceRange), weight);
        }

        return 0;
    }
    
    /// <summary>
    /// AI 拾起武器操作
    /// </summary>
    private void EnemyPickUpWeapon()
    {
        //这几个状态不需要主动拾起武器操作
        var state = StateController.CurrState;
        if (state == AINormalStateEnum.AiNormal)
        {
            return;
        }
        
        //拾起地上的武器
        if (InteractiveItem is Weapon weapon)
        {
            if (WeaponPack.ActiveItem == null) //手上没有武器, 无论如何也要拾起
            {
                TriggerInteractive();
                return;
            }

            //没弹药了
            if (weapon.IsTotalAmmoEmpty())
            {
                return;
            }
            
            var index = WeaponPack.FindIndex((we, i) => we.ActivityBase.Id == weapon.ActivityBase.Id);
            if (index != -1) //与武器背包中武器类型相同, 补充子弹
            {
                if (!WeaponPack.GetItem(index).IsAmmoFull())
                {
                    TriggerInteractive();
                }

                return;
            }

            // var index2 = Holster.FindWeapon((we, i) =>
            //     we.Attribute.WeightType == weapon.Attribute.WeightType && we.IsTotalAmmoEmpty());
            var index2 = WeaponPack.FindIndex((we, i) => we.IsTotalAmmoEmpty());
            if (index2 != -1) //扔掉没子弹的武器
            {
                ThrowWeapon(index2);
                TriggerInteractive();
                return;
            }
            
            // if (Holster.HasVacancy()) //有空位, 拾起武器
            // {
            //     TriggerInteractive();
            //     return;
            // }
        }
    }
    
    /// <summary>
    /// 获取锁定目标的剩余时间
    /// </summary>
    public override float GetLockRemainderTime()
    {
        var weapon = WeaponPack.ActiveItem;
        if (weapon == null)
        {
            return 0;
        }
        return weapon.Attribute.AiAttackAttr.LockingTime - LockTargetTime;
    }

    public override void LookTargetPosition(Vector2 pos)
    {
        LookTarget = null;
        LookPosition = pos;
        if (MountLookTarget)
        {
            //脸的朝向
            var gPos = Position;
            if (pos.X > gPos.X && Face == FaceDirection.Left)
            {
                Face = FaceDirection.Right;
            }
            else if (pos.X < gPos.X && Face == FaceDirection.Right)
            {
                Face = FaceDirection.Left;
            }
            //枪口跟随目标
            MountPoint.SetLookAt(pos);
        }
    }
    
    /// <summary>
    /// 返回所有武器是否弹药都打光了
    /// </summary>
    public bool IsAllWeaponTotalAmmoEmpty()
    {
        foreach (var weapon in WeaponPack.ItemSlot)
        {
            if (weapon != null && !weapon.IsTotalAmmoEmpty())
            {
                return false;
            }
        }

        return true;
    }

    public override float GetAttackRotation()
    {
        return MountPoint.RealRotation;
    }

    public override Vector2 GetMountPosition()
    {
        return MountPoint.GlobalPosition;
    }

    public override Node2D GetMountNode()
    {
        return MountPoint;
    }

    /// <summary>
    /// 拾起一个武器, 返回是否成功拾起, 如果不想立刻切换到该武器, exchange 请传 false
    /// </summary>
    /// <param name="weapon">武器对象</param>
    /// <param name="exchange">是否立即切换到该武器, 默认 true </param>
    public bool PickUpWeapon(Weapon weapon, bool exchange = true)
    {
        if (WeaponPack.PickupItem(weapon, exchange) != -1)
        {
            //从可互动队列中移除
            InteractiveItemList.Remove(weapon);
            OnPickUpWeapon(weapon);
            return true;
        }

        return false;
    }

    /// <summary>
    /// 切换到下一个武器
    /// </summary>
    public void ExchangeNextWeapon()
    {
        var weapon = WeaponPack.ActiveItem;
        WeaponPack.ExchangeNext();
        if (WeaponPack.ActiveItem != weapon)
        {
            OnExchangeWeapon(WeaponPack.ActiveItem);
        }
    }

    /// <summary>
    /// 切换到上一个武器
    /// </summary>
    public void ExchangePrevWeapon()
    {
        var weapon = WeaponPack.ActiveItem;
        WeaponPack.ExchangePrev();
        if (WeaponPack.ActiveItem != weapon)
        {
            OnExchangeWeapon(WeaponPack.ActiveItem);
        }
    }

    /// <summary>
    /// 扔掉当前使用的武器, 切换到上一个武器
    /// </summary>
    public void ThrowWeapon()
    {
        ThrowWeapon(WeaponPack.ActiveIndex);
    }

    /// <summary>
    /// 扔掉指定位置的武器
    /// </summary>
    /// <param name="index">武器在武器背包中的位置</param>
    public void ThrowWeapon(int index)
    {
        var weapon = WeaponPack.GetItem(index);
        if (weapon == null)
        {
            return;
        }

        var temp = weapon.AnimatedSprite.Position;
        if (Face == FaceDirection.Left)
        {
            temp.Y = -temp.Y;
        }
        //var pos = GlobalPosition + temp.Rotated(weapon.GlobalRotation);
        WeaponPack.RemoveItem(index);
        //播放抛出效果
        weapon.ThrowWeapon(this, GlobalPosition);
    }
    
    /// <summary>
    /// 切换到下一个武器
    /// </summary>
    public void ExchangeNextActiveProp()
    {
        var prop = ActivePropsPack.ActiveItem;
        ActivePropsPack.ExchangeNext();
        if (prop != ActivePropsPack.ActiveItem)
        {
            OnExchangeActiveProp(ActivePropsPack.ActiveItem);
        }
    }

    /// <summary>
    /// 切换到上一个武器
    /// </summary>
    public void ExchangePrevActiveProp()
    {
        var prop = ActivePropsPack.ActiveItem;
        ActivePropsPack.ExchangePrev();
        if (prop != ActivePropsPack.ActiveItem)
        {
            OnExchangeActiveProp(ActivePropsPack.ActiveItem);
        }
    }

    //-------------------------------------------------------------------------------------

    /// <summary>
    /// 触发换弹
    /// </summary>
    public virtual void Reload()
    {
        if (WeaponPack.ActiveItem != null)
        {
            WeaponPack.ActiveItem.Reload();
        }
    }
    
    public override void Attack()
    {
        if (!IsMeleeAttack && WeaponPack.ActiveItem != null)
        {
            WeaponPack.ActiveItem.Trigger(this);
        }
    }

    /// <summary>
    /// 触发近战攻击
    /// </summary>
    public virtual void MeleeAttack()
    {
        if (IsMeleeAttack || _meleeAttackTimer > 0)
        {
            return;
        }

        if (WeaponPack.ActiveItem != null && WeaponPack.ActiveItem.Attribute.CanMeleeAttack)
        {
            IsMeleeAttack = true;
            _meleeAttackTimer = RoleState.MeleeAttackTime;
            MountLookTarget = false;
            
            //WeaponPack.ActiveItem.TriggerMeleeAttack(this);
            //播放近战动画
            this.PlayAnimation_MeleeAttack(() =>
            {
                MountLookTarget = true;
                IsMeleeAttack = false;
            });
        }
    }

    /// <summary>
    /// 切换当前使用的武器的回调
    /// </summary>
    private void OnChangeActiveItem(Weapon weapon)
    {
        //这里处理近战区域
        if (weapon != null)
        {
            MeleeAttackCollision.Polygon = Utils.CreateSectorPolygon(
                Utils.ConvertAngle(-MeleeAttackAngle / 2f),
                (weapon.GetLocalFirePosition() - weapon.GripPoint.Position).Length() * 1.2f,
                MeleeAttackAngle,
                6
            );
            MeleeAttackArea.CollisionMask = AttackLayer | PhysicsLayer.Bullet;
        }
    }

    /// <summary>
    /// 近战区域碰到敌人
    /// </summary>
    private void OnMeleeAttackBodyEntered(Node2D body)
    {
        var activeWeapon = WeaponPack.ActiveItem;
        if (activeWeapon == null)
        {
            return;
        }
        var activityObject = body.AsActivityObject();
        if (activityObject != null)
        {
            if (activityObject is AdvancedRole role) //攻击角色
            {
                var damage = Utils.Random.RandomConfigRange(activeWeapon.Attribute.MeleeAttackHarmRange);
                damage = RoleState.CalcDamage(damage);
                
                //击退
                if (role is not Player) //目标不是玩家才会触发击退
                {
                    var attr = IsAi ? activeWeapon.AiUseAttribute : activeWeapon.PlayerUseAttribute;
                    var repel = Utils.Random.RandomConfigRange(attr.MeleeAttackRepelRange);
                    var position = role.GlobalPosition - MountPoint.GlobalPosition;
                    var v2 = position.Normalized() * repel;
                    role.MoveController.AddForce(v2);
                }
                
                role.CallDeferred(nameof(Hurt), this, damage, (role.GetCenterPosition() - GlobalPosition).Angle());
            }
            else if (activityObject is Bullet bullet) //攻击子弹
            {
                var attackLayer = bullet.AttackLayer;
                if (CollisionWithMask(attackLayer)) //是攻击玩家的子弹
                {
                    bullet.PlayDisappearEffect();
                    bullet.Destroy();
                }
            }
        }
    }
}
