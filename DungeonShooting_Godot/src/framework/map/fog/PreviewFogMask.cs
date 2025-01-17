﻿
using System;
using System.Collections;
using Godot;

/// <summary>
/// 绑定在门上的预览迷雾
/// </summary>
public partial class PreviewFogMask : FogMaskBase
{
    public enum PreviewFogType
    {
        Aisle,
        Room
    }

    private static bool _initTexture;
    private static Texture2D _previewAisle;
    private static Texture2D _previewRoom;
    
    /// <summary>
    /// 房间门
    /// </summary>
    public RoomDoorInfo DoorInfo;
    
    /// <summary>
    /// 迷雾类型
    /// </summary>
    public PreviewFogType FogType { get; private set; }
    
    // private float _previewAisleAngle;
    // private float _previewRoomAngle;
    // private Vector2 _previewAislePosition;
    // private Vector2 _previewRoomPosition;
    private long _cid;
    
    private static void InitTexture()
    {
        if (_initTexture)
        {
            return;
        }

        _initTexture = true;
        _previewAisle = ResourceManager.LoadTexture2D(ResourcePath.resource_sprite_map_PreviewTransition_png);
        _previewRoom = ResourceManager.LoadTexture2D(ResourcePath.resource_sprite_map_PreviewTransition2_png);
    }
    
    /// <summary>
    /// 初始化迷雾
    /// </summary>
    public void Init(RoomDoorInfo doorInfo, PreviewFogType fogType)
    {
        InitTexture();

        DoorInfo = doorInfo;
        FogType = fogType;
        var globalPosition = doorInfo.Door.GlobalPosition;
        if (doorInfo.Direction == DoorDirection.E)
        {
            if (fogType == PreviewFogType.Aisle)
            {
                Texture = _previewAisle;
                Position = globalPosition + new Vector2(GameConfig.TileCellSize, 0);
                RotationDegrees = 90;
            }
            else
            {
                Texture = _previewRoom;
                Position = globalPosition + new Vector2(-GameConfig.TileCellSize, 0);
                RotationDegrees = 270;
            }
        }
        else if (doorInfo.Direction == DoorDirection.W)
        {
            if (fogType == PreviewFogType.Aisle)
            {
                Texture = _previewAisle;
                Position = globalPosition + new Vector2(-GameConfig.TileCellSize, 0);
                RotationDegrees = 270;
            }
            else
            {
                Texture = _previewRoom;
                Position = globalPosition + new Vector2(GameConfig.TileCellSize, 0);
                RotationDegrees = 90;
            }
        }
        else if (doorInfo.Direction == DoorDirection.N)
        {
            if (fogType == PreviewFogType.Aisle)
            {
                Texture = _previewAisle;
                Position = globalPosition + new Vector2(0, -GameConfig.TileCellSize);
                RotationDegrees = 0;
            }
            else
            {
                Texture = _previewRoom;
                Position = globalPosition + new Vector2(0, GameConfig.TileCellSize);
                RotationDegrees = 180;
            }
        }
        else if (doorInfo.Direction == DoorDirection.S)
        {
            if (fogType == PreviewFogType.Aisle)
            {
                Texture = _previewAisle;
                Position = globalPosition;
                RotationDegrees = 180;
            }
            else
            {
                Texture = _previewRoom;
                Position = globalPosition + new Vector2(0, -GameConfig.TileCellSize * 2);
                RotationDegrees = 0;
            }
        }
    }
}