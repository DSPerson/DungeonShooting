
using Godot;

/// <summary>
/// Ui节点代码接口
/// </summary>
/// <typeparam name="TNodeType">Godot中的节点类型</typeparam>
/// <typeparam name="TCloneType">克隆该对象返回的类型</typeparam>
public abstract class IUiNode<TNodeType, TCloneType> where TNodeType : Node
{
    /// <summary>
    /// Godot节点实例
    /// </summary>
    public TNodeType Instance { get; }
    /// <summary>
    /// 克隆当前对象, 并返回新的对象,
    /// 注意: 如果子节点改名或者移动层级, 那么有可能对导致属性中的子节点无法访问
    /// </summary>
    public abstract TCloneType Clone();

    public IUiNode(TNodeType node)
    {
        Instance = node;
    }
    
    /// <summary>
    /// 嵌套打开子ui
    /// </summary>
    public UiBase OpenNestedUi(string uiName)
    {
        var packedScene = ResourceManager.Load<PackedScene>("res://" + GameConfig.UiPrefabDir + uiName + ".tscn");
        var uiBase = packedScene.Instantiate<UiBase>();
        Instance.AddChild(uiBase);
        
        uiBase.OnCreateUi();
        uiBase.ShowUi();
        return uiBase;
    }
    
    /// <summary>
    /// 嵌套打开子ui
    /// </summary>
    public T OpenNestedUi<T>(string uiName) where T : UiBase
    {
        return (T)OpenNestedUi(uiName);
    }
}