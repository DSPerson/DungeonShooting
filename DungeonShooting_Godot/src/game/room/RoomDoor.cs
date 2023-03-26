﻿
/// <summary>
/// 房间的门, 门有两种状态, 打开和关闭
/// </summary>
[RegisterActivity(ActivityIdPrefix.Other + "0001", ResourcePath.prefab_map_RoomDoor_tscn)]
public partial class RoomDoor : ActivityObject
{
    /// <summary>
    /// 门的方向
    /// </summary>
    public DoorDirection Direction => _door.Direction;
    
    /// <summary>
    /// 门是否关闭
    /// </summary>
    public bool IsClose { get; private set; }
    
    private RoomDoorInfo _door;

    /// <summary>
    /// 初始化调用
    /// </summary>
    public void Init(RoomDoorInfo doorInfo)
    {
        _door = doorInfo;
        OpenDoor();

        switch (doorInfo.Direction)
        {
            case DoorDirection.E:
                RotationDegrees = 90;
                break;
            case DoorDirection.W:
                RotationDegrees = 270;
                break;
            case DoorDirection.S:
                RotationDegrees = 180;
                break;
            case DoorDirection.N:
                RotationDegrees = 0;
                break;
        }
    }

    /// <summary>
    /// 打开当前的门
    /// </summary>
    public void OpenDoor()
    {
        IsClose = false;
        Visible = false;
        Collision.Disabled = true;
        if (_door.Navigation != null)
        {
            _door.Navigation.OpenNavigationNode.Enabled = true;
            _door.Navigation.OpenNavigationNode.Visible = true;
            _door.Navigation.CloseNavigationNode.Enabled = false;
            _door.Navigation.CloseNavigationNode.Visible = false;
        }
    }

    /// <summary>
    /// 关闭当前的门
    /// </summary>
    public void CloseDoor()
    {
        IsClose = true;
        Visible = true;
        Collision.Disabled = false;
        if (_door.Navigation != null)
        {
            _door.Navigation.OpenNavigationNode.Enabled = false;
            _door.Navigation.OpenNavigationNode.Visible = false;
            _door.Navigation.CloseNavigationNode.Enabled = true;
            _door.Navigation.CloseNavigationNode.Visible = true;
        }
    }
}