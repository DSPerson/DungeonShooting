﻿
using System;
using System.Collections.Generic;
using Godot;
using Plugin;

/// <summary>
/// 房间内活动物体基类
/// </summary>
public abstract class ActivityObject : KinematicBody2D
{
    /// <summary>
    /// 当前物体显示的精灵图像, 节点名称必须叫 "AnimatedSprite", 类型为 AnimatedSprite
    /// </summary>
    public AnimatedSprite AnimatedSprite { get; }

    /// <summary>
    /// 当前物体显示的阴影图像, 节点名称必须叫 "ShadowSprite", 类型为 Sprite
    /// </summary>
    public Sprite ShadowSprite { get; }

    /// <summary>
    /// 当前物体碰撞器节点, 节点名称必须叫 "Collision", 类型为 CollisionShape2D
    /// </summary>
    public CollisionShape2D Collision { get; }

    /// <summary>
    /// 是否调用过 Destroy() 函数
    /// </summary>
    public bool IsDestroyed { get; private set; }

    /// <summary>
    /// 是否正在投抛过程中
    /// </summary>
    public bool IsThrowing => _throwData != null && !_throwData.IsOver;

    /// <summary>
    /// 物体厚度, 影响阴影偏移
    /// </summary>
    public int Thickness { get; protected set; } = 2;
    
    private List<KeyValuePair<Type, Component>> _components = new List<KeyValuePair<Type, Component>>();
    private bool initShadow;
    private string _prevAnimation;

    //存储投抛该物体时所产生的数据
    private ObjectThrowData _throwData;

    public ActivityObject(string scenePath)
    {
        //加载预制体
        var tempPrefab = ResourceManager.Load<PackedScene>(scenePath);
        if (tempPrefab == null)
        {
            throw new Exception("创建 ActivityObject 的参数 scenePath 为 null !");
        }

        var tempNode = tempPrefab.Instance<ActivityObjectTemplate>();
        ZIndex = tempNode.ZIndex;
        CollisionLayer = tempNode.CollisionLayer;
        CollisionMask = tempNode.CollisionMask;

        //移动子节点
        var count = tempNode.GetChildCount();
        for (int i = 0; i < count; i++)
        {
            var body = tempNode.GetChild(0);
            tempNode.RemoveChild(body);
            AddChild(body);
            switch (body.Name)
            {
                case "AnimatedSprite":
                    AnimatedSprite = (AnimatedSprite)body;
                    break;
                case "ShadowSprite":
                    ShadowSprite = (Sprite)body;
                    ShadowSprite.Visible = false;
                    ShadowSprite.ZIndex = -5;
                    break;
                case "Collision":
                    Collision = (CollisionShape2D)body;
                    break;
            }
        }
    }

    /// <summary>
    /// 显示阴影
    /// </summary>
    public void ShowShadowSprite()
    {
        if (!initShadow)
        {
            initShadow = true;
            ShadowSprite.Material = ResourceManager.ShadowMaterial;
        }

        var anim = AnimatedSprite.Animation;
        if (_prevAnimation != anim)
        {
            //切换阴影动画
            ShadowSprite.Texture = AnimatedSprite.Frames.GetFrame(anim, AnimatedSprite.Frame);
        }

        _prevAnimation = anim;
        ShadowSprite.Scale = AnimatedSprite.Scale;
        ShadowSprite.Rotation = AnimatedSprite.Rotation;
        ShadowSprite.Position = AnimatedSprite.Position + new Vector2(0, Thickness);
        ShadowSprite.Visible = true;
    }

    /// <summary>
    /// 隐藏阴影
    /// </summary>
    public void HideShadowSprite()
    {
        ShadowSprite.Visible = false;
    }

    public void SetDefaultTexture(Texture texture)
    {
        if (AnimatedSprite.Frames == null)
        {
            SpriteFrames spriteFrames = new SpriteFrames();
            AnimatedSprite.Frames = spriteFrames;
            spriteFrames.AddFrame("default", texture);
        }
        else
        {
            SpriteFrames spriteFrames = AnimatedSprite.Frames;
            spriteFrames.SetFrame("default", 0, texture);
        }

        AnimatedSprite.Animation = "default";
        AnimatedSprite.Playing = true;
    }

    public void GetCurrentTexture()
    {

    }

    public Texture GetDefaultTexture()
    {
        return AnimatedSprite.Frames.GetFrame("default", 0);
    }

    /// <summary>
    /// 返回是否能与其他ActivityObject互动
    /// </summary>
    /// <param name="master">触发者</param>
    public virtual CheckInteractiveResult CheckInteractive(ActivityObject master)
    {
        return new CheckInteractiveResult(this);
    }

    /// <summary>
    /// 与其它ActivityObject互动时调用
    /// </summary>
    /// <param name="master">触发者</param>
    public virtual void Interactive(ActivityObject master)
    {
        
    }

    /// <summary>
    /// 投抛该物体达到最高点时调用
    /// </summary>
    public virtual void OnThrowMaxHeight(float height)
    {

    }

    /// <summary>
    /// 投抛状态下第一次接触地面时调用, 之后的回弹落地将不会调用该函数
    /// </summary>
    public virtual void OnFirstFallToGround()
    {

    }

    /// <summary>
    /// 投抛状态下每次接触地面时调用
    /// </summary>
    public virtual void OnFallToGround()
    {

    }

    /// <summary>
    /// 投抛结束时调用
    /// </summary>
    public virtual void OnThrowOver()
    {

    }
    
    /// <summary>
    /// 拾起一个 node 节点
    /// </summary>
    public void Pickup()
    {
        var parent = GetParent();
        if (parent != null)
        {
            if (IsThrowing)
            {
                StopThrow();
            }
            parent.RemoveChild(this);
        }
    }

    /// <summary>
    /// 将一个节点扔到地上, 并设置显示的阴影
    /// </summary>
    public void PutDown()
    {
        var parent = GetParent();
        if (parent != RoomManager.Current.ObjectRoot)
        {
            if (parent != null)
            {
                parent.RemoveChild(this);
            }

            RoomManager.Current.ObjectRoot.AddChild(this);
        }
        
        ShowShadowSprite();
    }
    
    /// <summary>
    /// 将一个节点扔到地上, 并设置显示的阴影
    /// </summary>
    /// <param name="position">放置的位置</param>
    public void PutDown(Vector2 position)
    {
        PutDown();
        Position = position;
    }

    /// <summary>
    /// 将该节点投抛出去
    /// </summary>
    /// <param name="size">碰撞器大小</param>
    /// <param name="start">起始坐标 (全局)</param>
    /// <param name="startHeight">起始高度</param>
    /// <param name="direction">投抛角度 (0-360)</param>
    /// <param name="xSpeed">移动速度</param>
    /// <param name="ySpeed">下坠速度</param>
    /// <param name="rotate">旋转速度</param>
    /// <param name="bounce">落地时是否回弹</param>
    /// <param name="bounceStrength">落地回弹力度, 1为不消耗能量, 值越小回弹力度越小</param>
    /// <param name="bounceSpeed">落地回弹后的速度, 1为不消速度, 值越小回弹速度消耗越大</param>
    public void Throw(Vector2 size, Vector2 start, float startHeight, float direction, float xSpeed,
        float ySpeed, float rotate, bool bounce = false, float bounceStrength = 0.5f, float bounceSpeed = 0.8f)
    {
        if (_throwData == null)
        {
            _throwData = new ObjectThrowData();
        }

        SetThrowCollision();

        _throwData.IsOver = false;
        _throwData.Size = size;
        _throwData.StartPosition = _throwData.CurrPosition = start;
        _throwData.Direction = direction;
        _throwData.XSpeed = xSpeed;
        _throwData.YSpeed = ySpeed;
        _throwData.StartXSpeed = xSpeed;
        _throwData.StartYSpeed = ySpeed;
        _throwData.RotateSpeed = rotate;
        _throwData.LinearVelocity = new Vector2(_throwData.XSpeed, 0).Rotated(_throwData.Direction * Mathf.Pi / 180);
        _throwData.Y = startHeight;
        _throwData.Bounce = bounce;
        _throwData.BounceStrength = bounceStrength;
        _throwData.BounceSpeed = bounceSpeed;

        _throwData.RectangleShape.Extents = _throwData.Size * 0.5f;

        Throw();
    }

    /// <summary>
    /// 强制停止投抛运动
    /// </summary>
    public void StopThrow()
    {
        _throwData.IsOver = true;
        RestoreCollision();
    }

    public void AddComponent(Component component)
    {
        if (!ContainsComponent(component))
        {
            _components.Add(new KeyValuePair<Type, Component>(component.GetType(), component));
            component._SetActivityObject(this);
            component.OnMount();
        }
    }

    public void RemoveComponent(Component component)
    {
        if (ContainsComponent(component))
        {
            component.OnUnMount();
            component._SetActivityObject(null);
        }
    }

    public Component GetComponent(Type type)
    {
        for (int i = 0; i < _components.Count; i++)
        {
            var temp = _components[i];
            if (temp.Key == type)
            {
                return temp.Value;
            }
        }

        return null;
    }

    public TC GetComponent<TC>() where TC : Component
    {
        var component = GetComponent(typeof(TC));
        if (component == null) return null;
        return (TC)component;
    }

    public override void _Process(float delta)
    {
        //更新组件
        if (_components.Count > 0)
        {
            var arr = _components.ToArray();
            for (int i = 0; i < arr.Length; i++)
            {
                if (IsDestroyed) return;
                var temp = arr[i].Value;
                if (temp != null && temp.ActivityObject == this && temp.Enable)
                {
                    if (!temp.IsStart)
                    {
                        temp.Start();
                    }

                    temp.Update(delta);
                }
            }
        }

        //投抛计算
        if (_throwData != null && !_throwData.IsOver)
        {
            _throwData.LinearVelocity = MoveAndSlide(_throwData.LinearVelocity);
            Position = new Vector2(Position.x, Position.y - _throwData.YSpeed * delta);
            var rotate = GlobalRotationDegrees + _throwData.RotateSpeed * delta;
            GlobalRotationDegrees = rotate;

            var pos = AnimatedSprite.GlobalPosition;
            ShadowSprite.GlobalRotationDegrees = rotate;

            var ysp = _throwData.YSpeed;

            _throwData.Y += _throwData.YSpeed * delta;
            _throwData.YSpeed -= GameConfig.G * delta;

            //达到最高点
            if (ysp * _throwData.YSpeed < 0)
            {
                ZIndex = 0;
                OnThrowMaxHeight(_throwData.Y);
            }

            //落地判断
            if (_throwData.Y <= 0)
            {
                ShadowSprite.GlobalPosition = pos + new Vector2(0, Thickness);
                Collision.GlobalPosition = pos;
                
                _throwData.IsOver = true;

                //第一次接触地面
                if (_throwData.FirstOver)
                {
                    _throwData.FirstOver = false;
                    OnFirstFallToGround();
                }

                //如果落地高度不够低, 再抛一次
                if (_throwData.StartYSpeed > 1 && _throwData.Bounce)
                {
                    _throwData.StartPosition = Position;
                    _throwData.Y = 0;
                    _throwData.XSpeed = _throwData.StartXSpeed = _throwData.StartXSpeed * _throwData.BounceSpeed;
                    _throwData.YSpeed = _throwData.StartYSpeed = _throwData.StartYSpeed * _throwData.BounceStrength;
                    _throwData.RotateSpeed = _throwData.RotateSpeed * _throwData.BounceStrength;
                    _throwData.LinearVelocity *= _throwData.BounceSpeed;
                    // _throwData.LinearVelocity =
                    //     new Vector2(_throwData.XSpeed, 0).Rotated(_throwData.Direction * Mathf.Pi / 180);
                    _throwData.FirstOver = false;
                    _throwData.IsOver = false;

                    OnFallToGround();
                }
                else //结束
                {
                    OnFallToGround();
                    ThrowOver();
                }
            }
            else
            {
                //计算阴影位置
                ShadowSprite.GlobalPosition = pos + new Vector2(0, Thickness + _throwData.Y);
                //碰撞器位置
                Collision.GlobalPosition = pos + new Vector2(0, _throwData.Y);
            }
        }

        //更新阴影贴图, 使其和动画一致
        if (ShadowSprite.Visible)
        {
            var anim = AnimatedSprite.Animation;
            if (_prevAnimation != anim)
            {
                //切换阴影动画
                ShadowSprite.Texture = AnimatedSprite.Frames.GetFrame(anim, AnimatedSprite.Frame);
            }

            _prevAnimation = anim;
        }
    }

    public override void _PhysicsProcess(float delta)
    {
        //更新组件
        if (_components.Count > 0)
        {
            var arr = _components.ToArray();
            for (int i = 0; i < arr.Length; i++)
            {
                if (IsDestroyed) return;
                var temp = arr[i].Value;
                if (temp != null && temp.ActivityObject == this && temp.Enable)
                {
                    if (!temp.IsStart)
                    {
                        temp.Start();
                    }

                    temp.PhysicsUpdate(delta);
                }
            }
        }
    }

    public void Destroy()
    {
        if (IsDestroyed)
        {
            return;
        }

        IsDestroyed = true;
        QueueFree();
        var arr = _components.ToArray();
        for (int i = 0; i < arr.Length; i++)
        {
            arr[i].Value?.Destroy();
        }
    }

    private bool ContainsComponent(Component component)
    {
        for (int i = 0; i < _components.Count; i++)
        {
            if (_components[i].Value == component)
            {
                return true;
            }
        }

        return false;
    }

    /// <summary>
    /// 触发投抛动作
    /// </summary>
    private void Throw()
    {
        var parent = GetParent();
        if (parent == null)
        {
            RoomManager.Current.SortRoot.AddChild(this);
        }
        else if (parent == RoomManager.Current.ObjectRoot)
        {
            parent.RemoveChild(this);
            RoomManager.Current.SortRoot.AddChild(this);
        }

        GlobalPosition = _throwData.StartPosition + new Vector2(0, -_throwData.Y);

        //显示阴影
        ShowShadowSprite();
    }
    
    /// <summary>
    /// 设置投抛状态下的碰撞器
    /// </summary>
    private void SetThrowCollision()
    {
        if (_throwData != null && _throwData.UseOrigin)
        {
            _throwData.OriginShape = Collision.Shape;
            _throwData.OriginPosition = Collision.Position;
            _throwData.OriginRotation = Collision.Rotation;
            _throwData.OriginScale = Collision.Scale;
            _throwData.OriginZIndex = ZIndex;
            _throwData.OriginCollisionEnable = Collision.Disabled;
            _throwData.OriginCollisionPosition = Collision.Position;
            _throwData.OriginCollisionRotation = Collision.Rotation;
            _throwData.OriginCollisionScale = Collision.Scale;
            _throwData.OriginCollisionMask = CollisionMask;
            _throwData.OriginCollisionLayer = CollisionLayer;

            if (_throwData.RectangleShape == null)
            {
                _throwData.RectangleShape = new RectangleShape2D();
            }

            Collision.Shape = _throwData.RectangleShape;
            //Collision.Position = Vector2.Zero;
            Collision.Rotation = 0;
            Collision.Scale = Vector2.One;
            ZIndex = 2;
            Collision.Disabled = false;
            Collision.Position = Vector2.Zero;
            Collision.Rotation = 0;
            Collision.Scale = Vector2.One;
            CollisionMask = 1;
            CollisionLayer = 0;
            _throwData.UseOrigin = false;
        }
    }

    /// <summary>
    /// 重置碰撞器
    /// </summary>
    private void RestoreCollision()
    {
        if (_throwData != null && !_throwData.UseOrigin)
        {
            Collision.Shape = _throwData.OriginShape;
            Collision.Position = _throwData.OriginPosition;
            Collision.Rotation = _throwData.OriginRotation;
            Collision.Scale = _throwData.OriginScale;
            ZIndex = _throwData.OriginZIndex;
            Collision.Disabled = _throwData.OriginCollisionEnable;
            Collision.Position = _throwData.OriginCollisionPosition;
            Collision.Rotation = _throwData.OriginCollisionRotation;
            Collision.Scale = _throwData.OriginCollisionScale;
            CollisionMask = _throwData.OriginCollisionMask;
            CollisionLayer = _throwData.OriginCollisionLayer;

            _throwData.UseOrigin = true;
        }
    }

    /// <summary>
    /// 投抛结束
    /// </summary>
    private void ThrowOver()
    {
        GetParent().RemoveChild(this);
        RoomManager.Current.ObjectRoot.AddChild(this);
        RestoreCollision();
        
        OnThrowOver();
    }
}