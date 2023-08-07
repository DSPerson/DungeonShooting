﻿
using System;
using System.Collections.Generic;
using Godot;
using UI.EditorTips;
using UI.EditorWindow;
using UI.MapEditorCreateGroup;
using UI.MapEditorCreateMark;
using UI.MapEditorCreatePreinstall;
using UI.MapEditorCreateRoom;

public static class EditorWindowManager
{
    /// <summary>
    /// 弹出通用提示面板
    /// </summary>
    /// <param name="title">标题</param>
    /// <param name="message">显示内容</param>
    /// <param name="onClose">关闭时的回调</param>
    public static void ShowTips(string title, string message, Action onClose = null)
    {
        var window = UiManager.Open_EditorWindow();
        window.SetWindowTitle(title);
        if (onClose != null)
        {
            window.CloseEvent += onClose;
        }
        window.SetButtonList(
            new EditorWindowPanel.ButtonData("确定", () =>
            {
                window.CloseWindow();
            })
        );
        var body = window.OpenBody<EditorTipsPanel>(UiManager.UiName.EditorTips);
        body.SetMessage(message);
    }

    /// <summary>
    /// 弹出询问窗口
    /// </summary>
    /// <param name="title">标题</param>
    /// <param name="message">显示内容</param>
    /// <param name="onClose">关闭时的回调, 参数如果为 true 表示点击了确定</param>
    public static void ShowConfirm(string title, string message, Action<bool> onClose)
    {
        var window = UiManager.Open_EditorWindow();
        window.SetWindowTitle(title);
        window.CloseEvent += () =>
        {
            onClose(false);
        };
        window.SetButtonList(
            new EditorWindowPanel.ButtonData("确定", () =>
            {
                window.CloseWindow();
                onClose(true);
            }),
            new EditorWindowPanel.ButtonData("取消", () =>
            {
                window.CloseWindow();
                onClose(false);
            })
        );
        var body = window.OpenBody<EditorTipsPanel>(UiManager.UiName.EditorTips);
        body.SetMessage(message);
    }

    /// <summary>
    /// 打开创建地牢组弹窗
    /// </summary>
    /// <param name="onCreateGroup">创建成功时回调</param>
    public static void ShowCreateGroup(Action<DungeonRoomGroup> onCreateGroup)
    {
        var window = UiManager.Open_EditorWindow();
        window.SetWindowTitle("创建地牢组");
        window.SetWindowSize(new Vector2I(700, 500));
        var body = window.OpenBody<MapEditorCreateGroupPanel>(UiManager.UiName.MapEditorCreateGroup);
        window.SetButtonList(
            new EditorWindowPanel.ButtonData("确定", () =>
            {
                //获取填写的数据, 并创建ui
                var groupInfo = body.GetGroupInfo();
                if (groupInfo != null)
                {
                    window.CloseWindow();
                    onCreateGroup(groupInfo);
                }
            }),
            new EditorWindowPanel.ButtonData("取消", () =>
            {
                window.CloseWindow();
            })
        );
    }
    
    /// <summary>
    /// 打开创建地牢房间弹窗
    /// </summary>
    /// <param name="groupName">选择的组名称, 如果不需要有选择的项, 则传 null</param>
    /// <param name="roomType">选择的房间类型</param>
    /// <param name="onCreateRoom">创建成功时回调</param>
    public static void ShowCreateRoom(string groupName, int roomType, Action<DungeonRoomSplit> onCreateRoom)
    {
        var window = UiManager.Open_EditorWindow();
        window.SetWindowTitle("创建地牢房间");
        window.SetWindowSize(new Vector2I(700, 600));
        var body = window.OpenBody<MapEditorCreateRoomPanel>(UiManager.UiName.MapEditorCreateRoom);
        if (groupName != null)
        {
            body.SetSelectGroup(groupName);
        }
        body.SetSelectType(roomType);
        
        window.SetButtonList(
            new EditorWindowPanel.ButtonData("确定", () =>
            {
                //获取填写的数据, 并创建ui
                var roomSplit = body.GetRoomInfo();
                if (roomSplit != null)
                {
                    window.CloseWindow();
                    onCreateRoom(roomSplit);
                }
            }),
            new EditorWindowPanel.ButtonData("取消", () =>
            {
                window.CloseWindow();
            })
        );
    }

    /// <summary>
    /// 打开创建房间预设弹窗
    /// </summary>
    /// <param name="list">当前房间已经包含的所有预设列表</param>
    /// <param name="onCreatePreinstall">创建成功时的回调</param>
    public static void ShowCreatePreinstall(List<RoomPreinstall> list, Action<RoomPreinstall> onCreatePreinstall)
    {
        var window = UiManager.Open_EditorWindow();
        window.SetWindowTitle("创建房间预设");
        window.SetWindowSize(new Vector2I(700, 600));
        var body = window.OpenBody<MapEditorCreatePreinstallPanel>(UiManager.UiName.MapEditorCreatePreinstall);
        window.SetButtonList(
            new EditorWindowPanel.ButtonData("确定", () =>
            {
                var roomPreinstall = body.GetRoomPreinstall(list);
                if (roomPreinstall != null)
                {
                    window.CloseWindow();
                    onCreatePreinstall(roomPreinstall);
                }
            }),
            new EditorWindowPanel.ButtonData("取消", () =>
            {
                window.CloseWindow();
            })
        );
    }

    public static void ShowCreateMark()
    {
        var window = UiManager.Open_EditorWindow();
        window.SetWindowTitle("创建标记");
        window.SetWindowSize(new Vector2I(700, 600));
        var body = window.OpenBody<MapEditorCreateMarkPanel>(UiManager.UiName.MapEditorCreateMark);
        window.SetButtonList(
            new EditorWindowPanel.ButtonData("确定", () =>
            {
                // var roomPreinstall = body.GetRoomPreinstall(list);
                // if (roomPreinstall != null)
                // {
                //     window.CloseWindow();
                //     onCreatePreinstall(roomPreinstall);
                // }
            }),
            new EditorWindowPanel.ButtonData("取消", () =>
            {
                window.CloseWindow();
            })
        );
    }
    
    public static void ShowSelectObject(string title)
    {
        var window = UiManager.Open_EditorWindow();
        window.S_Window.Instance.Size = new Vector2I(900, 600);
        window.SetWindowTitle(title);
        window.OpenBody(UiManager.UiName.MapEditorSelectObject);
    }
}