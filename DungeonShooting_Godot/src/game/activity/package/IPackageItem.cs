
/// <summary>
/// 可放入背包中的物体接口
/// </summary>
public interface IPackageItem
{
    /// <summary>
    /// 物体所属角色
    /// </summary>
    Role Master { get; set; }

    /// <summary>
    /// 物体在背包中的索引, 如果不在背包中则为 -1
    /// </summary>
    int PackageIndex { get; set; }

    /// <summary>
    /// 当物体拾起并放入背包时调用
    /// </summary>
    void OnPickUpItem();
    
    /// <summary>
    /// 当物体从背包中移除时调用
    /// </summary>
    void OnRemoveItem();
    
    /// <summary>
    /// 当切换到当前物体时调用
    /// </summary>
    void OnActiveItem();
    
    /// <summary>
    /// 当收起当前物体时调用
    /// </summary>
    void OnConcealItem();

    /// <summary>
    /// 当道具溢出时调用, 也就是修改了背包大小后背包容不下这个道具时调用, 用于处理扔下道具
    /// </summary>
    void OnOverflowItem();
}