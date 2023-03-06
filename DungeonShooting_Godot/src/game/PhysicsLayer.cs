/// <summary>
/// 物理碰撞层
/// </summary>
public class PhysicsLayer
{
    /// <summary>
    /// 无任何层级
    /// </summary>
    public const uint None = 0;
    /// <summary>
    /// 墙壁
    /// </summary>
    public const uint Wall = 0b1;
    /// <summary>
    /// 子弹
    /// </summary>
    public const uint Bullet = 0b10;
    /// <summary>
    /// 道具
    /// </summary>
    public const uint Props = 0b100;
    /// <summary>
    /// 玩家
    /// </summary>
    public const uint Player = 0b1000;
    /// <summary>
    /// 敌人
    /// </summary>
    public const uint Enemy = 0b10000;
    /// <summary>
    /// 归属区域判断层级
    /// </summary>
    public const uint Affiliation = 0b100000;
}