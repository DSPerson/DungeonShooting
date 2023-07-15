﻿
using System.Text.Json;
using System.Text.Json.Serialization;

/// <summary>
/// 房间配置文件相关信息, 将会在 RoomConfig.json 中汇总
/// </summary>
public class DungeonRoomSplit
{
    /// <summary>
    /// 房间场景路径
    /// </summary>
    [JsonInclude]
    public string ScenePath;
    
    /// <summary>
    /// 房间配置路径
    /// </summary>
    [JsonInclude]
    public string RoomPath;

    /// <summary>
    /// 房间地块配置数据
    /// </summary>
    [JsonInclude]
    public string TilePath;

    /// <summary>
    /// 房间配置数据, 第一次获取会要在资源中加载数据
    /// </summary>
    [JsonIgnore]
    public DungeonRoomInfo RoomInfo
    {
        get
        {
            if (_roomInfo == null)
            {
                var asText = ResourceManager.LoadText(RoomPath);
                _roomInfo = JsonSerializer.Deserialize<DungeonRoomInfo>(asText);

                //需要处理 DoorAreaInfos 长度为 0 的房间, 并为其配置默认值
                var areaInfos = _roomInfo.DoorAreaInfos;
                if (areaInfos.Count == 0)
                {
                    areaInfos.Add(new DoorAreaInfo(DoorDirection.N, GameConfig.TileCellSize, (_roomInfo.Size.X - 1) * GameConfig.TileCellSize));
                    areaInfos.Add(new DoorAreaInfo(DoorDirection.S, GameConfig.TileCellSize, (_roomInfo.Size.X - 1) * GameConfig.TileCellSize));
                    areaInfos.Add(new DoorAreaInfo(DoorDirection.W, GameConfig.TileCellSize, (_roomInfo.Size.Y - 1) * GameConfig.TileCellSize));
                    areaInfos.Add(new DoorAreaInfo(DoorDirection.E, GameConfig.TileCellSize, (_roomInfo.Size.Y - 1) * GameConfig.TileCellSize));
                }
            }

            return _roomInfo;
        }
    }

    private DungeonRoomInfo _roomInfo;
}