﻿
using System.Collections.Generic;
using Godot;

public class ActivityMark
{
    /// <summary>
    /// 物体 Id
    /// </summary>
    public string Id { get; set; }

    /// <summary>
    /// 刷新位置
    /// </summary>
    public Vector2 Position { get; set; }

    /// <summary>
    /// 额外属性
    /// </summary>
    public Dictionary<string, string> Attr { get; set; }

    /// <summary>
    /// 特殊标记类型
    /// </summary>
    public SpecialMarkType MarkType { get; set; }

    /// <summary>
    /// 延时时间, 单位: 秒
    /// </summary>
    public float DelayTime { get; set; }
}