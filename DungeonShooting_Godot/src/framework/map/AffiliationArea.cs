﻿
using System;
using System.Collections.Generic;
using Godot;

/// <summary>
/// 归属区域
/// </summary>
public partial class AffiliationArea : Area2D
{
    /// <summary>
    /// 当前实例所属房间
    /// </summary>
    public RoomInfo RoomInfo;
    
    /// <summary>
    /// 当前归属区域包含的所有物体对象
    /// </summary>
    private readonly HashSet<ActivityObject> _includeItems = new HashSet<ActivityObject>();
    
    /// <summary>
    /// 玩家是否是第一次进入
    /// </summary>
    public bool IsFirstEnterFlag { get; private set; } = true;
    
    private bool _init = false;
    
    /// <summary>
    /// 根据矩形区域初始化归属区域
    /// </summary>
    public void Init(RoomInfo roomInfo, Rect2 rect2)
    {
        if (_init)
        {
            return;
        }

        _init = true;

        RoomInfo = roomInfo;
        var collisionShape = new CollisionShape2D();
        collisionShape.GlobalPosition = rect2.Position + rect2.Size / 2;
        var shape = new RectangleShape2D();
        shape.Size = rect2.Size;
        collisionShape.Shape = shape;
        AddChild(collisionShape);
        _Init();
    }

    private void _Init()
    {
        Monitoring = true;
        Monitorable = false;
        CollisionLayer = PhysicsLayer.None;
        CollisionMask = PhysicsLayer.Props | PhysicsLayer.Player | PhysicsLayer.Enemy;

        BodyEntered += OnBodyEntered;
    }

    /// <summary>
    /// 将物体添加到当前所属区域中
    /// </summary>
    public void InsertItem(ActivityObject activityObject)
    {
        if (activityObject.AffiliationArea == this)
        {
            return;
        }

        if (activityObject.AffiliationArea != null)
        {
            _includeItems.Remove(activityObject);
        }
        activityObject.AffiliationArea = this;
        _includeItems.Add(activityObject);

        //如果是玩家
        if (activityObject == Player.Current)
        {
            OnPlayerEnterRoom();
        }
    }

    /// <summary>
    /// 将物体从当前所属区域移除
    /// </summary>
    public void RemoveItem(ActivityObject activityObject)
    {
        if (activityObject.AffiliationArea == null)
        {
            return;
        }
        activityObject.AffiliationArea = null;
        _includeItems.Remove(activityObject);
    }

    /// <summary>
    /// 获取该区域中物体的总数
    /// </summary>
    public int GetIncludeItemsCount()
    {
        return _includeItems.Count;
    }

    /// <summary>
    /// 统计符合条件的数量
    /// </summary>
    /// <param name="handler">操作函数, 返回是否满足要求</param>
    public int FindIncludeItemsCount(Func<ActivityObject, bool> handler)
    {
        var count = 0;
        foreach (var activityObject in _includeItems)
        {
            if (handler(activityObject))
            {
                count++;
            }
        }
        return count;
    }

    /// <summary>
    /// 查询所有符合条件的对象并返回
    /// </summary>
    /// <param name="handler">操作函数, 返回是否满足要求</param>
    public ActivityObject[] FindIncludeItems(Func<ActivityObject, bool> handler)
    {
        var list = new List<ActivityObject>();
        foreach (var activityObject in _includeItems)
        {
            if (handler(activityObject))
            {
                list.Add(activityObject);
            }
        }
        return list.ToArray();
    }

    /// <summary>
    /// 检查是否有符合条件的对象
    /// </summary>
    /// <param name="handler">操作函数, 返回是否满足要求</param>
    public bool ExistIncludeItem(Func<ActivityObject, bool> handler)
    {
        foreach (var activityObject in _includeItems)
        {
            if (handler(activityObject))
            {
                return true;
            }
        }

        return false;
    }
    
    private void OnBodyEntered(Node2D body)
    {
        if (body is ActivityObject activityObject)
        {
            //注意需要延时调用
            CallDeferred(nameof(InsertItem), activityObject);
        }
    }

    //玩家进入房间
    private void OnPlayerEnterRoom()
    {
        if (IsFirstEnterFlag)
        {
            EventManager.EmitEvent(EventEnum.OnPlayerFirstEnterRoom, RoomInfo);
            IsFirstEnterFlag = false;
        }
        EventManager.EmitEvent(EventEnum.OnPlayerEnterRoom, RoomInfo);
    }
}