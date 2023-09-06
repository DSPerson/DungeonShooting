
using System.Collections.Generic;
using Godot;

/// <summary>
/// 房间的数据描述
/// </summary>
public class RoomInfo : IDestroy
{
    public RoomInfo(int id, DungeonRoomType type, DungeonRoomSplit roomSplit)
    {
        Id = id;
        RoomType = type;
        RoomSplit = roomSplit;
    }

    /// <summary>
    /// 房间 id
    /// </summary>
    public int Id;

    /// <summary>
    /// 房间类型
    /// </summary>
    public DungeonRoomType RoomType;

    /// <summary>
    /// 层级, 也就是离初始房间间隔多少个房间
    /// </summary>
    public int Layer;
    
    /// <summary>
    /// 生成该房间使用的配置数据
    /// </summary>
    public DungeonRoomSplit RoomSplit;
    
    /// <summary>
    /// 房间大小, 单位: 格
    /// </summary>
    public Vector2I Size;

    /// <summary>
    /// 房间位置, 单位: 格
    /// </summary>
    public Vector2I Position;
    
    /// <summary>
    /// 门
    /// </summary>
    public List<RoomDoorInfo> Doors = new List<RoomDoorInfo>();

    /// <summary>
    /// 连接该房间的过道占用区域信息
    /// </summary>
    public List<Rect2I> AisleArea = new List<Rect2I>();

    /// <summary>
    /// 下一个房间
    /// </summary>
    public List<RoomInfo> Next = new List<RoomInfo>();
    
    /// <summary>
    /// 上一个房间
    /// </summary>
    public RoomInfo Prev;

    /// <summary>
    /// 当前房间使用的预设
    /// </summary>
    public RoomPreinstall RoomPreinstall;

    /// <summary>
    /// 当前房间归属区域
    /// </summary>
    public AffiliationArea AffiliationArea;

    /// <summary>
    /// 静态精灵绘制画布
    /// </summary>
    public RoomStaticImageCanvas StaticImageCanvas;

    /// <summary>
    /// 是否处于闭关状态, 也就是房间门没有主动打开
    /// </summary>
    public bool IsSeclusion { get; private set; } = false;
    
    public bool IsDestroyed { get; private set; }
    
    // private bool _beReady = false;
    // private bool _waveStart = false;
    // private int _currWaveIndex = 0;
    // private int _currWaveNumber = 0;
    //private List<ActivityMark> _currActivityMarks = new List<ActivityMark>();

    /// <summary>
    /// 获取房间的全局坐标, 单位: 像素
    /// </summary>
    public Vector2I GetWorldPosition()
    {
        return new Vector2I(
            Position.X * GameConfig.TileCellSize,
            Position.Y * GameConfig.TileCellSize
        );
    }

    /// <summary>
    /// 获取房间左上角的 Tile 距离全局坐标原点的偏移, 单位: 像素
    /// </summary>
    /// <returns></returns>
    public Vector2I GetOffsetPosition()
    {
        return RoomSplit.RoomInfo.Position.AsVector2I() * GameConfig.TileCellSize;
    }
    
    /// <summary>
    /// 获取房间横轴结束位置, 单位: 格
    /// </summary>
    public int GetHorizontalEnd()
    {
        return Position.X + Size.X;
    }

    /// <summary>
    /// 获取房间纵轴结束位置, 单位: 格
    /// </summary>
    public int GetVerticalEnd()
    {
        return Position.Y + Size.Y;
    }
    
    /// <summary>
    /// 获取房间横轴开始位置, 单位: 格
    /// </summary>
    public int GetHorizontalStart()
    {
        return Position.X;
    }

    /// <summary>
    /// 获取房间纵轴开始位置, 单位: 格
    /// </summary>
    public int GetVerticalStart()
    {
        return Position.Y;
    }

    /// <summary>
    /// 获取房间宽度, 单位: 像素
    /// </summary>
    public int GetWidth()
    {
        return Size.X * GameConfig.TileCellSize;
    }
    
    
    /// <summary>
    /// 获取房间高度, 单位: 像素
    /// </summary>
    public int GetHeight()
    {
        return Size.Y * GameConfig.TileCellSize;
    }
    
    public void Destroy()
    {
        if (IsDestroyed)
        {
            return;
        }

        IsDestroyed = true;
        foreach (var nextRoom in Next)
        {
            nextRoom.Destroy();
        }
        Next.Clear();
        if (RoomPreinstall != null)
        {
            RoomPreinstall.Destroy();
            RoomPreinstall = null;
        }
        
        if (StaticImageCanvas != null)
        {
            StaticImageCanvas.Destroy();
        }

        if (AffiliationArea != null)
        {
            AffiliationArea.Destroy();
        }
    }
    
    /// <summary>
    /// 加载地牢完成
    /// </summary>
    public void OnReady()
    {
        //提前加载波
        RoomPreinstall.OnReady();
    }
    
    /// <summary>
    /// 玩家第一次进入房间, 房间准备好了, 准备刷敌人, 并且关闭所有<br/>
    /// 当清完每一波刷新的敌人后即可开门
    /// </summary>
    public void OnFirstEnter()
    {
        if (RoomPreinstall.IsRunWave)
        {
            return;
        }
        
        //房间内有敌人, 或者会刷新敌人才会关门
        var hasEnemy = false;
        if (AffiliationArea.ExistEnterItem(activityObject => activityObject.CollisionWithMask(PhysicsLayer.Enemy))) //先判断房间里面是否有敌人
        {
            hasEnemy = true;
        }
        else if (RoomPreinstall.HasEnemy()) //在判断是否会刷出敌人
        {
            hasEnemy = true;
        }

        if (!hasEnemy) //没有敌人, 不关门
        {
            IsSeclusion = false;
            //执行第一波生成
            RoomPreinstall.StartWave();
            return;
        }
        
        //关门
        foreach (var doorInfo in Doors)
        {
            doorInfo.Door.CloseDoor();
        }
        IsSeclusion = true;

        //执行第一波生成
        RoomPreinstall.StartWave();
    }

    /// <summary>
    /// 当前房间所有敌人都被清除了
    /// </summary>
    public void OnClearRoom()
    {
        if (RoomPreinstall.IsLastWave) //所有 mark 都走完了
        {
            IsSeclusion = false;
            //开门
            if (RoomPreinstall.HasEnemy())
            {
                foreach (var doorInfo in Doors)
                {
                    doorInfo.Door.OpenDoor();
                }
            }
            //所有标记执行完成
            RoomPreinstall.OverWave();
        }
        else //执行下一波
        {
            RoomPreinstall.NextWave();
        }
    }
}