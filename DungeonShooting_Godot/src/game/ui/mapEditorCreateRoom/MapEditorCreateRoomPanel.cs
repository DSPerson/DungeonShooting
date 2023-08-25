using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using Godot;

namespace UI.MapEditorCreateRoom;

public partial class MapEditorCreateRoomPanel : MapEditorCreateRoom
{
    //key: 组名称, value: 选项索引
    private Dictionary<string, int> _groupMap = new Dictionary<string, int>();

    public override void OnCreateUi()
    {
        //初始化选项
        var groupButton = S_GroupSelect.Instance;
        var index = 0;
        foreach (var mapGroupInfo in MapProjectManager.GroupMap)
        {
            var id = index++;
            var groupGroupName = mapGroupInfo.Value.GroupName;
            _groupMap.Add(groupGroupName, id);
            groupButton.AddItem(groupGroupName, id);
        }

        var selectButton = S_TypeSelect.Instance;
        var roomTypes = Enum.GetValues<DungeonRoomType>();
        for (var i = 0; i < roomTypes.Length; i++)
        {
            var item = roomTypes[i];
            var text = DungeonManager.DungeonRoomTypeToDescribeString(item);
            selectButton.AddItem(text, (int)item);
        }
    }

    /// <summary>
    /// 设置选中的组
    /// </summary>
    public void SetSelectGroup(string groupName)
    {
        if (_groupMap.TryGetValue(groupName, out var value))
        {
            S_GroupSelect.Instance.Selected = value;
            return;
        }
        
        S_GroupSelect.Instance.Selected = -1;
    }

    /// <summary>
    /// 设置选中的房间类型
    /// </summary>
    public void SetSelectType(int index)
    {
        S_TypeSelect.Instance.Selected = index;
    }

    /// <summary>
    /// 填完数据后获取数据对象, 并进行验证, 如果验证失败, 则返回 null
    /// </summary>
    public DungeonRoomSplit GetRoomInfo()
    {
        var roomInfo = new DungeonRoomInfo();
        roomInfo.RoomName = S_RoomNameInput.Instance.Text;
        roomInfo.Remark = S_RemarkInput.Instance.Text;
        //检查名称是否合规
        if (string.IsNullOrEmpty(roomInfo.RoomName))
        {
            EditorWindowManager.ShowTips("错误", "房间名称不能为空!");
            return null;
        }
        
        var groupIndex = S_GroupSelect.Instance.Selected;
        foreach (var pair in _groupMap)
        {
            if (pair.Value == groupIndex)
            {
                roomInfo.GroupName = pair.Key;
            }
        }

        if (roomInfo.GroupName == null)
        {
            EditorWindowManager.ShowTips("错误", "组名错误!");
            return null;
        }
        
        roomInfo.RoomType = (DungeonRoomType)S_TypeSelect.Instance.GetSelectedId();
        
        //检测是否有同名房间
        var temp = roomInfo.GroupName + "/" + DungeonManager.DungeonRoomTypeToString(roomInfo.RoomType) + "/" + roomInfo.RoomName;
        var dirPath = MapProjectManager.CustomMapPath + temp;
        var dir = new DirectoryInfo(dirPath);
        if (dir.Exists && dir.GetFiles().Length > 0)
        {
            EditorWindowManager.ShowTips("错误", $"已经有相同路径的房间了!\n路径: {temp}");
            return null;
        }

        roomInfo.Weight = (int)S_WeightInput.Instance.Value;
        roomInfo.Size = new SerializeVector2();
        roomInfo.Position = new SerializeVector2();
        roomInfo.DoorAreaInfos = new List<DoorAreaInfo>();

        var roomSplit = new DungeonRoomSplit();
        roomSplit.RoomPath = dirPath + "/" + MapProjectManager.GetRoomInfoConfigName(roomInfo.RoomName);
        roomSplit.RoomInfo = roomInfo;

        var tileInfo = new DungeonTileInfo();
        tileInfo.NavigationList = new List<NavigationPolygonData>();
        tileInfo.Floor = new List<int>();
        tileInfo.Middle = new List<int>();
        tileInfo.Top = new List<int>();
        
        roomSplit.TilePath = dirPath + "/" + MapProjectManager.GetTileInfoConfigName(roomInfo.RoomName);
        roomSplit.TileInfo = tileInfo;

        roomSplit.PreinstallPath = dirPath + "/" + MapProjectManager.GetRoomPreinstallConfigName(roomInfo.RoomName);
        roomSplit.Preinstall = new List<RoomPreinstallInfo>();
        return roomSplit;
    }
}
