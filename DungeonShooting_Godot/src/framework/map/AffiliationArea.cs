﻿
using Godot;

/// <summary>
/// 归属区域
/// </summary>
public partial class AffiliationArea : Area2D
{
    private bool _init = false;
    
    public void Init(Rect2 rect2)
    {
        if (_init)
        {
            return;
        }

        _init = true;
        var collisionShape = new CollisionShape2D();
        collisionShape.GlobalPosition = rect2.Position + rect2.Size / 2;
        var shape = new RectangleShape2D();
        shape.Size = rect2.Size;
        collisionShape.Shape = shape;
        AddChild(collisionShape);
    }

    public void Init(Vector2[] polygon)
    {
        if (_init)
        {
            return;
        }

        _init = true;
        var collisionPolygon = new CollisionPolygon2D();
        collisionPolygon.Polygon = polygon;
        AddChild(collisionPolygon);
    }
}