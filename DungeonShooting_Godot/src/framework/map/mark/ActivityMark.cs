﻿
using Godot;

/// <summary>
/// 物体生成标记
/// </summary>
[Tool]
public partial class ActivityMark : Node2D
{
    /// <summary>
    /// 物体类型
    /// </summary>
    [Export]
    public ActivityIdPrefix.ActivityPrefixType Type = ActivityIdPrefix.ActivityPrefixType.NonePrefix;

    /// <summary>
    /// 物体id
    /// </summary>
    [Export]
    public string ItemId;

    /// <summary>
    /// 所在层级
    /// </summary>
    [Export]
    public RoomLayerEnum Layer = RoomLayerEnum.NormalLayer;

    /// <summary>
    /// 该标记在第几波调用 BeReady，
    /// 一个房间内所以敌人清完即可进入下一波
    /// </summary>
    [Export]
    public int WaveNumber = 1;

    /// <summary>
    /// 延时执行时间，单位：秒
    /// </summary>
    [Export]
    public float DelayTime = 0;
    
    /// <summary>
    /// 绘制的颜色
    /// </summary>
    protected Color DrawColor = new Color(0.4F, 0.56078434F, 0.8784314F);

    /// <summary>
    /// 获取物体Id
    /// </summary>
    public string GetItemId()
    {
        return ActivityIdPrefix.GetNameByPrefixType(Type) + ItemId;
    }

    /// <summary>
    /// 标记准备好了
    /// </summary>
    public void BeReady(RoomInfo roomInfo)
    {
        OnBeReady(roomInfo);
    }

    /// <summary>
    /// 调用该函数表示该标记可以生成物体了, 使用标记创建实例必须调用 CreateInstance(id)
    /// </summary>
    public virtual void OnBeReady(RoomInfo roomInfo)
    {
        CreateInstance<ActivityObject>(GetItemId());
        Visible = false;
    }
    
    /// <summary>
    /// 创建实例，并放入场景中，使用标记创建实例必须调用 CreateInstance(id)
    /// </summary>
    protected T CreateInstance<T>(string id) where T : ActivityObject
    {
        var instance = ActivityObject.Create<T>(id);
        instance.PutDown(GlobalPosition, Layer);
        if (instance is Enemy)
        {
            
        }
        return instance;
    }
    
    public override void _Draw()
    {
        if (Engine.IsEditorHint() || GameApplication.Instance.Debug)
        {
            DrawLine(new Vector2(-5, -5), new Vector2(5, 5), DrawColor, 2f);
            DrawLine(new Vector2(-5, 5), new Vector2(5, -5), DrawColor, 2f);
        }
    }

}