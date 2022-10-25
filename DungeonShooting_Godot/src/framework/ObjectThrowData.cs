﻿using Godot;

public class ObjectThrowData
{
    /// <summary>
    /// 是否已经结束
    /// </summary>
    public bool IsOver = true;

    /// <summary>
    /// 物体大小
    /// </summary>
    public Vector2 Size = Vector2.One;

    /// <summary>
    /// 起始坐标
    /// </summary>
    public Vector2 StartPosition;

    /// <summary>
    /// 移动方向, 0 - 360
    /// </summary>
    public float Direction;

    /// <summary>
    /// x速度, 也就是水平速度
    /// </summary>
    public float XSpeed;

    /// <summary>
    /// y轴速度, 也就是竖直速度
    /// </summary>
    public float YSpeed;

    /// <summary>
    /// 初始x轴组队
    /// </summary>
    public float StartXSpeed;

    /// <summary>
    /// 初始y轴速度
    /// </summary>
    public float StartYSpeed;

    /// <summary>
    /// 旋转速度
    /// </summary>
    public float RotateSpeed;
    
    /// <summary>
    /// 碰撞器形状
    /// </summary>
    public RectangleShape2D RectangleShape;
}