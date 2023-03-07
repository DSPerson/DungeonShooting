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
        Visible = false;
        Collision.Disabled = true;
        _door.Navigation.NavigationNode.Enabled = true;
    }

    /// <summary>
    /// 关闭当前的门
    /// </summary>
    public void CloseDoor()
    {
        Visible = true;
        Collision.Disabled = false;
        _door.Navigation.NavigationNode.Enabled = false;
    }
}