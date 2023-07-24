
/// <summary>
/// 事件类型枚举
/// </summary>
public enum EventEnum
{
    /// <summary>
    /// 敌人死亡, 参数为死亡的敌人的实例, 参数类型为<see cref="Enemy"/>
    /// </summary>
    OnEnemyDie,
    /// <summary>
    /// 玩家进入一个房间，参数类型为<see cref="RoomInfo"/>
    /// </summary>
    OnPlayerEnterRoom,
    /// <summary>
    /// 玩家第一次进入某个房间，参数类型为<see cref="RoomInfo"/>
    /// </summary>
    OnPlayerFirstEnterRoom,
    /// <summary>
    /// 玩家可互动对象改变, 参数类型为<see cref="CheckInteractiveResult"/>
    /// </summary>
    OnPlayerChangeInteractiveItem,
    /// <summary>
    /// 玩家血量发生改变, 参数为玩家血量
    /// </summary>
    OnPlayerHpChange,
    /// <summary>
    /// 玩家最大血量发生改变, 参数为玩家最大血量
    /// </summary>
    OnPlayerMaxHpChange,
    /// <summary>
    /// 玩家护盾值发生改变, 参数为玩家护盾值
    /// </summary>
    OnPlayerShieldChange,
    /// <summary>
    /// 玩家最大护盾值发生改变, 参数为玩家最大护盾值
    /// </summary>
    OnPlayerMaxShieldChange,
    /// <summary>
    /// 玩家拾起武器, 参数为<see cref="Weapon"/>
    /// </summary>
    OnPlayerPickUpWeapon,
    /// <summary>
    /// 玩家丢弃武器, 参数为<see cref="Weapon"/>
    /// </summary>
    OnPlayerRemoveWeapon,
    /// <summary>
    /// 玩家拾起道具, 参数为<see cref="Prop"/>
    /// </summary>
    OnPlayerPickUpProp,
    /// <summary>
    /// 玩家丢弃道具, 参数为<see cref="Prop"/>
    /// </summary>
    OnPlayerRemoveProp,
    
    /// <summary>
    /// 当玩家进入地牢时调用, 没有参数
    /// </summary>
    OnEnterDungeon,
    /// <summary>
    /// 当玩家退出地牢时调用, 没有参数
    /// </summary>
    OnExitDungeon,
}
