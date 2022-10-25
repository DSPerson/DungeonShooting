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
    public abstract CheckInteractiveResult CheckInteractive(ActivityObject master);

    /// <summary>
    /// 与其它ActivityObject互动时调用
    /// </summary>
    /// <param name="master">触发者</param>
    public abstract void Interactive(ActivityObject master);

    /// <summary>
    /// 投抛该物体达到最高点时调用
    /// </summary>
    public virtual void OnMaxHeight(float height)
    {

    }
    
    public virtual void PickUp()
    {
        
    }

    public virtual void PickDown()
    {
        
    }

    public virtual void Throw(Vector2 size, Vector2 start, float startHeight, float direction, float xSpeed, float ySpeed, float rotate)
    {
        
    }

    public virtual void StopThrow()
    {
        
    }
    
    /// <summary>
    /// 结束的调用
    /// </summary>
    public virtual void OnOver()
    {
        GetParent().RemoveChild(this);
        RoomManager.Current.ObjectRoot.AddChild(this);
        Collision.Disabled = true;
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
            MoveAndSlide(new Vector2(_throwData.XSpeed, 0).Rotated(_throwData.Direction * Mathf.Pi / 180));
            Position = new Vector2(0, Position.y - _throwData.YSpeed * delta);
            var rotate = GlobalRotationDegrees + _throwData.RotateSpeed * delta;
            GlobalRotationDegrees = rotate;

            //计算阴影位置
            ShadowSprite.GlobalRotationDegrees = rotate;
            // ShadowSprite.GlobalRotationDegrees = rotate + (inversionX ? 180 : 0);
            ShadowSprite.GlobalPosition = AnimatedSprite.GlobalPosition + new Vector2(0, 2 - Position.y);
            var ysp = _throwData.YSpeed;
            _throwData.YSpeed -= GameConfig.G * delta;
            //达到最高点
            if (ysp * _throwData.YSpeed < 0)
            {
                OnMaxHeight(-Position.y);
            }

            //落地判断
            if (Position.y >= 0)
            {
                Position = new Vector2(0, 0);
                _throwData.IsOver = true;
                OnOver();
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
}