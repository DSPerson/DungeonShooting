
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using Godot;

namespace Generator;

/// <summary>
/// 地牢房间数据生成器
/// </summary>
public static class DungeonRoomGenerator
{
	/// <summary>
	/// 根据名称在编辑器中创建地牢的预制房间, open 表示创建完成后是否在编辑器中打开这个房间
	/// </summary>
	public static bool CreateDungeonRoom(string roomName, bool open = false)
	{
		try
		{
			//加载脚本资源
			var scriptRes = GD.Load<CSharpScript>("res://src/framework/activity/ActivityObjectTemplate.cs");
			//创建场景资源
			var prefabFile = GameConfig.RoomTileDir + roomName + ".tscn";
			var prefabResPath = "res://" + prefabFile;
			if (!Directory.Exists(GameConfig.RoomTileDir))
			{
				Directory.CreateDirectory(GameConfig.RoomTileDir);
			}
			var tileMap = new TileMap();
			tileMap.Name = roomName;
			tileMap.SetScript(scriptRes);
			var scene = new PackedScene();
			scene.Pack(tileMap);
			ResourceSaver.Save(scene, prefabResPath);
		
			//打包房间配置
			GenerateRoomConfig();
#if TOOLS
			//打开房间
			if (open)
			{
				Plugin.Plugin.Instance.GetEditorInterface().OpenSceneFromPath(prefabResPath);
			}   
#endif
		}
		catch (Exception e)
		{
			GD.PrintErr(e.ToString());
			return false;
		}
		
		return true;
	}
	
	/// <summary>
	/// 执行生成 RoomConfig.json 操作, 返回是否执行成功
	/// </summary>
    public static bool GenerateRoomConfig()
    {
	    try
	    {
		    //地图路径
		    var tileDir = GameConfig.RoomTileDir;
		    //地图描述数据路径
		    var tileDataDir = GameConfig.RoomTileDataDir;
		
		    var tileDirInfo = new DirectoryInfo(tileDir);
		    var tileDataDirInfo = new DirectoryInfo(tileDataDir);

		    //所有地图列表
		    var mapList = new HashSet<string>();
		
		    //收集所有名称
		    var fileDataInfos = tileDataDirInfo.GetFiles();
		    foreach (var fileInfo in fileDataInfos)
		    {
			    mapList.Add(RemoveExtension(fileInfo.Name));
		    }
		    //收集所有名称
		    var fileInfos = tileDirInfo.GetFiles();
		    foreach (var fileInfo in fileInfos)
		    {
			    if (fileInfo.Extension == ".tscn")
			    {
				    mapList.Add(RemoveExtension(fileInfo.Name));
			    }
		    }
		
		    //剔除多余的 tile.json
		    var arrays = mapList.ToArray();
		    foreach (var item in arrays)
		    {
			    if (!File.Exists(tileDir + item + ".tscn"))
			    {
				    mapList.Remove(item);
				    var filePath = tileDataDir + item + ".json";
				    if (File.Exists(filePath))
				    {
					    GD.Print($"未找到'{tileDir + item}.tscn', 删除配置文件: {filePath}");
					    File.Delete(filePath);
				    }
			    }
		    }

		    //手动生成缺失的 tile.json
		    foreach (var item in mapList)
		    {
			    if (!File.Exists(tileDataDir + item + ".json"))
			    {
				    var tscnName = tileDir + item + ".tscn";
				    var packedScene = ResourceManager.Load<PackedScene>(tscnName, false);
				    if (packedScene != null)
				    {
					    var dungeonRoomTemplate = packedScene.Instantiate<DungeonRoomTemplate>();
					    var usedRect = dungeonRoomTemplate.GetUsedRect();
					    var dungeonTile = new DungeonTile(dungeonRoomTemplate);
					    dungeonTile.SetFloorAtlasCoords(new List<Vector2I>() { new Vector2I(0, 8) });
					    //计算导航网格
					    dungeonTile.GenerateNavigationPolygon(0);
					    var polygonData = dungeonTile.GetPolygonData();
					    DungeonRoomTemplate.SaveConfig(new List<DoorAreaInfo>(), usedRect.Position, usedRect.Size, polygonData.ToList(), item);
					    dungeonRoomTemplate.QueueFree();
				    }
			    }
		    }

		    var list = new List<DungeonRoomSplit>();
		    //整合操作
		    foreach (var item in mapList)
		    {
			    var configPath = tileDataDir + item + ".json";
			    var split = new DungeonRoomSplit();
			    split.ScenePath = ToResPath(tileDir + item + ".tscn");
			    split.ConfigPath = ToResPath(configPath);
			    list.Add(split);
		    }

		    //写出配置
		    var config = new JsonSerializerOptions();
		    config.WriteIndented = true;
		    var text = JsonSerializer.Serialize(list, config);
		    File.WriteAllText(GameConfig.RoomTileConfigFile, text);

		    GD.Print("地牢房间配置, 重新打包完成!");
	    }
	    catch (Exception e)
	    {
		    GD.PrintErr(e.ToString());
		    return false;
	    }

	    return true;
    }
    
    private static string ToResPath(string path)
    {
	    var field = path.Replace("\\", "/");
	    return "res://" + field;
    }

    private static string RemoveExtension(string name)
    {
	    var index = name.LastIndexOf(".", StringComparison.Ordinal);
	    if (index >= 0)
	    {
		    return name.Substring(0, index);
	    }

	    return name;
    }
}