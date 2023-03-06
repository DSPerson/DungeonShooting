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
        _Init();
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
        _Init();
    }

    private void _Init()
    {
        Monitoring = true;
        Monitorable = false;
        CollisionLayer = PhysicsLayer.None;
        CollisionMask = PhysicsLayer.Props | PhysicsLayer.Player | PhysicsLayer.Bullet;

        BodyEntered += OnBodyEntered;
    }

    private void OnBodyEntered(Node2D body)
    {
        GD.Print("有物体进入: " + body.Name);
        if (body is CharacterBody2D characterBody)
        {
            GD.Print("layer: " + characterBody.CollisionLayer);
        }
    }
}