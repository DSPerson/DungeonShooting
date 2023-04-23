﻿
using System.Collections;
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
    /// 物体会在该矩形区域内随机位置生成
    /// </summary>
    [Export]
    public Vector2I BirthRect = Vector2I.Zero;
    
    /// <summary>
    /// 绘制的颜色
    /// </summary>
    [Export]
    public Color DrawColor = new Color(1, 1, 1, 1);

    /// <summary>
    /// 物体初始海拔高度
    /// </summary>
    [ExportGroup("Vertical")]
    [Export(PropertyHint.Range, "0, 128")]
    public int Altitude = 0;

    /// <summary>
    /// 物体初始纵轴速度
    /// </summary>
    [Export(PropertyHint.Range, "-1000,1000,0.1")]
    public float VerticalSpeed = 0;

    /// <summary>
    /// 当前标记所在Tile节点
    /// </summary>
    public TileMap TileRoot;

    //是否已经结束
    private bool _isOver = true;
    private float _overTimer = 0;
    private float _timer = 0;
    private RoomInfo _tempRoom;

    //绘制的字体
    private static Font _drawFont;
    
    /// <summary>
    /// 获取物体Id
    /// </summary>
    public string GetItemId()
    {
        return ActivityIdPrefix.GetNameByPrefixType(Type) + ItemId;
    }

    public override void _Process(double delta)
    {
#if TOOLS
        if (Engine.IsEditorHint())
        {
            QueueRedraw();
            return;
        }
#endif
        if (_isOver)
        {
            _overTimer += (float)delta;
            if (_overTimer >= 1)
            {
                SetActive(false);
            }
        }
        else
        {
            if (DelayTime > 0)
            {
                _timer += (float)delta;
                if (_timer >= DelayTime)
                {
                    Doing(_tempRoom);
                    _tempRoom = null;
                    _isOver = true;
                }
            }
        }
    }

    /// <summary>
    /// 标记准备好了
    /// </summary>
    public void BeReady(RoomInfo roomInfo)
    {
        _isOver = false;
        _overTimer = 0;
        SetActive(true);
        if (DelayTime <= 0)
        {
            Doing(roomInfo);
            _isOver = true;
        }
        else
        {
            _timer = 0;
            _tempRoom = roomInfo;
        }
    }

    /// <summary>
    /// 是否已经结束
    /// </summary>
    public bool IsOver()
    {
        return _isOver && _overTimer >= 1;
    }

    /// <summary>
    /// 调用该函数表示该标记可以生成物体了
    /// </summary>
    public virtual void Doing(RoomInfo roomInfo)
    {
        CreateActivityObject().PutDown(Layer);
    }

    /// <summary>
    /// 实例化ItemId指定的物体, 并返回对象实例, 函数会自动设置位置
    /// </summary>
    protected ActivityObject CreateActivityObject()
    {
        var instance = ActivityObject.Create(GetItemId());
        var pos = Position;
        if (BirthRect != Vector2I.Zero)
        {
            instance.Position = new Vector2(
                Utils.RandomRangeInt((int)pos.X - BirthRect.X / 2, (int)pos.X + BirthRect.X / 2),
                Utils.RandomRangeInt((int)pos.Y - BirthRect.Y / 2, (int)pos.Y + BirthRect.Y / 2)
            );
        }
        else
        {
            instance.Position = pos;
        }
        
        instance.VerticalSpeed = VerticalSpeed;
        instance.Altitude = Altitude;
        instance.StartCoroutine(OnActivityObjectBirth(instance));
        return instance;
    }
    
    /// <summary>
    /// 生成 ActivityObject 时调用, 用于出生时的动画效果
    /// </summary>
    protected virtual IEnumerator OnActivityObjectBirth(ActivityObject instance)
    {
        var a = 1.0f;
        instance.SetBlendColor(Colors.White);
        //禁用自定义行为
        instance.EnableCustomBehavior = false;
        //禁用下坠
        instance.EnableVerticalMotion = false;

        for (var i = 0; i < 10; i++)
        {
            instance.SetBlendSchedule(a);
            yield return 0;
        }
        
        while (a > 0)
        {
            instance.SetBlendSchedule(a);
            a -= 0.05f;
            yield return 0;
        }
        
        //启用自定义行为
        instance.EnableCustomBehavior = true;
        //启用下坠
        instance.EnableVerticalMotion = true;
    }

#if TOOLS
    public override void _Draw()
    {
        if (Engine.IsEditorHint() || GameApplication.Instance.Debug)
        {
            var drawColor = DrawColor;

            //如果在编辑器中选中了该节点, 则绘更改绘制颜色的透明度
            var selectedNodes = Plugin.Plugin.Instance?.GetEditorInterface()?.GetSelection()?.GetSelectedNodes();
            if (selectedNodes != null && selectedNodes.Contains(this))
            {
                drawColor.A = 1f;
            }
            else
            {
                drawColor.A = 0.5f;
            }
            
            DrawLine(new Vector2(-2, -2), new Vector2(2, 2), drawColor, 1f);
            DrawLine(new Vector2(-2, 2), new Vector2(2, -2), drawColor, 1f);

            if (BirthRect != Vector2.Zero)
            {
                DrawRect(new Rect2(-BirthRect / 2, BirthRect), drawColor, false, 0.5f);
            }

            if (_drawFont == null)
            {
                _drawFont = ResourceManager.Load<Font>(ResourcePath.Silver_ttf);
            }
            DrawString(_drawFont, new Vector2(-14, 12), WaveNumber.ToString(), HorizontalAlignment.Center, 28, 14);
        }
    }
#endif

    /// <summary>
    /// 设置当前节点是否是活动状态
    /// </summary>
    public void SetActive(bool flag)
    {
        // SetProcess(flag);
        // SetPhysicsProcess(flag);
        // SetProcessInput(flag);
        // Visible = flag;
        
        var parent = GetParent();
        if (flag)
        {
            if (parent == null)
            {
                TileRoot.AddChild(this);
            }
            else if (parent != TileRoot)
            {
                parent.RemoveChild(this);
                TileRoot.AddChild(this);
            }
            Owner = TileRoot;
        }
        else
        {
            if (parent != null)
            {
                parent.RemoveChild(this);
                Owner = null;
            }
        }
    }
}