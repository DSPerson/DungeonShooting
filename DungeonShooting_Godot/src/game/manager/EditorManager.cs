﻿
using System.Collections.Generic;

public static class EditorManager
{
    /// <summary>
    /// 当前使用的地牢组
    /// </summary>
    public static DungeonRoomGroup SelectDungeonGroup { get; private set; }

    /// <summary>
    /// 当使用的地牢房间
    /// </summary>
    public static DungeonRoomSplit SelectRoom { get; private set; }

    /// <summary>
    /// 当前使用的预设索引
    /// </summary>
    public static int SelectPreinstallIndex { get; private set; } = -1;

    /// <summary>
    /// 当前使用的预设
    /// </summary>
    public static RoomPreinstallInfo SelectPreinstall { get; private set; }

    /// <summary>
    /// 当前选中的波索引
    /// </summary>
    public static int SelectWaveIndex { get; private set; } = -1;

    /// <summary>
    /// 当前选中的波
    /// </summary>
    public static List<MarkInfo> SelectWave { get; private set; }
    
    /// <summary>
    /// 当前选中的标记
    /// </summary>
    public static MarkInfo SelectMark { get; private set; }

    public static void SetSelectDungeonGroup(DungeonRoomGroup roomGroup)
    {
        if (SelectDungeonGroup != roomGroup)
        {
            SelectDungeonGroup = roomGroup;
            EventManager.EmitEvent(EventEnum.OnSelectGroup, SelectDungeonGroup);
        }
    }

    public static void SetSelectRoom(DungeonRoomSplit roomSplit)
    {
        if (SelectRoom != roomSplit)
        {
            SelectRoom = roomSplit;
            EventManager.EmitEvent(EventEnum.OnSelectRoom, SelectRoom);
        }
    }

    public static void SetSelectPreinstallIndex(int index)
    {
        if (SelectRoom == null)
        {
            if (SelectPreinstallIndex != -1)
            {
                SelectPreinstallIndex = -1;
                SelectPreinstall = null;
                EventManager.EmitEvent(EventEnum.OnSelectPreinstall, SelectPreinstall);
            }
        }
        else if (SelectPreinstallIndex != index)
        {
            if (index < 0 || SelectRoom.Preinstall == null || index >= SelectRoom.Preinstall.Count)
            {
                if (SelectPreinstallIndex != -1)
                {
                    SelectPreinstallIndex = -1;
                    SelectPreinstall = null;
                    EventManager.EmitEvent(EventEnum.OnSelectPreinstall, SelectPreinstall);
                }
            }
            else
            {
                if (SelectPreinstallIndex != index)
                {
                    SelectPreinstallIndex = index;
                    SelectPreinstall = SelectRoom.Preinstall[index];
                    EventManager.EmitEvent(EventEnum.OnSelectPreinstall, SelectPreinstall);
                }
            }
        }
    }

    public static void SetSelectWaveIndex(int index)
    {
        if (SelectPreinstall == null)
        {
            if (SelectWaveIndex != -1)
            {
                SelectWaveIndex = -1;
                SelectWave = null;
                EventManager.EmitEvent(EventEnum.OnSelectWave, SelectWave);
            }
        }
        else if (SelectWaveIndex != index)
        {
            SelectWaveIndex = index;
            if (index < 0 || SelectPreinstall.WaveList == null || index >= SelectPreinstall.WaveList.Count)
            {
                if (SelectWaveIndex != -1)
                {
                    SelectWaveIndex = -1;
                    SelectWave = null;
                    EventManager.EmitEvent(EventEnum.OnSelectWave, SelectWave);
                }
            }
            else
            {
                if (SelectWaveIndex != index)
                {
                    SelectWaveIndex = index;
                    SelectWave = SelectPreinstall.WaveList[index];
                    EventManager.EmitEvent(EventEnum.OnSelectWave, SelectWave);
                }
            }
        }
    }

    public static void SetSelectMark(MarkInfo markInfo)
    {
        if (SelectMark != markInfo)
        {
            SelectMark = markInfo;
            EventManager.EmitEvent(EventEnum.OnSelectMark, markInfo);
        }
    }
}