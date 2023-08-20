using System.Collections.Generic;
using Godot;
using UI.MapEditor;

namespace UI.MapEditorMapMark;

public partial class MapEditorMapMarkPanel : MapEditorMapMark
{
    public enum SelectToolType
    {
        None,
        Wave,
        Mark
    }

    public class MarkCellData
    {
        public EditorWaveCell ParentCell;
        public MarkInfo MarkInfo;

        public MarkCellData(EditorWaveCell parentCell, MarkInfo markInfo)
        {
            ParentCell = parentCell;
            MarkInfo = markInfo;
        }
    }
    
    /// <summary>
    /// 选中的cell选项
    /// </summary>
    public IUiCell SelectCell { get; private set; }

    /// <summary>
    /// Cell 上的工具类型
    /// </summary>
    public SelectToolType ToolType { get; private set; } = SelectToolType.None;
    
    /// <summary>
    /// 编辑器Tile对象
    /// </summary>
    public EditorTileMap EditorTileMap { get; private set; }
    
    //波数网格组件
    private UiGrid<WaveItem, List<MarkInfo>> _grid;
    private EventFactory _eventFactory;

    public override void OnCreateUi()
    {
        var editorPanel = (MapEditorPanel)ParentUi;
        EditorTileMap = editorPanel.S_TileMap.Instance;

        //S_DynamicTool.Instance.GetParent().RemoveChild(S_DynamicTool.Instance);
        S_DynamicTool.Instance.Visible = false;
        
        _grid = new UiGrid<WaveItem, List<MarkInfo>>(S_WaveItem, typeof(EditorWaveCell));
        _grid.SetCellOffset(new Vector2I(0, 10));
        _grid.SetColumns(1);

        S_PreinstallOption.Instance.ItemSelected += OnItemSelected;
        S_AddPreinstall.Instance.Pressed += OnAddPreinstall;
        S_EditPreinstall.Instance.Pressed += OnEditPreinstall;
        S_DeletePreinstall.Instance.Pressed += OnDeletePreinstall;
        S_AddWaveButton.Instance.Pressed += OnAddWave;

        S_EditButton.Instance.Pressed += OnToolEditClick;
        S_DeleteButton.Instance.Pressed += OnToolDeleteClick;
    }

    public override void OnShowUi()
    {
        _eventFactory = EventManager.CreateEventFactory();
        _eventFactory.AddEventListener(EventEnum.OnSelectMark, OnSelectMark);
        RefreshPreinstallSelect();
    }

    public override void OnHideUi()
    {
        _eventFactory.RemoveAllEventListener();
        _eventFactory = null;
    }

    public override void OnDestroyUi()
    {
        _grid.Destroy();
    }

    //选中标记回调
    private void OnSelectMark(object arg)
    {
        if (arg is MarkInfo markInfo && (SelectCell is not EditorMarkCell || (SelectCell is EditorMarkCell markCell && markCell.Data.MarkInfo != markInfo)))
        {
            var selectPreinstall = GetSelectPreinstall();
            if (selectPreinstall != null)
            {
                for (var i = 0; i < selectPreinstall.WaveList.Count; i++)
                {
                    var wave = selectPreinstall.WaveList[i];
                    for (var j = 0; j < wave.Count; j++)
                    {
                        var tempMark = wave[j];
                        if (tempMark == markInfo)
                        {
                            var waveCell = (EditorWaveCell)_grid.GetCell(i);
                            var cell = (EditorMarkCell)waveCell.MarkGrid.GetCell(j);
                            //如果没有展开, 则调用展开方法
                            if (!waveCell.IsExpand())
                            {
                                waveCell.OnExpandOrClose();
                            }
                            //选中物体
                            cell.OnClick();
                        }
                    }
                }
            }
        }
    }
    
    /// <summary>
    /// 获取当前选中的预设
    /// </summary>
    public RoomPreinstall GetSelectPreinstall()
    {
        var index = S_PreinstallOption.Instance.Selected;
        var preinstall = EditorTileMap.RoomSplit.Preinstall;
        if (index >= preinstall.Count)
        {
            return null;
        }
        return preinstall[index];
    }

    /// <summary>
    /// 刷新预设下拉框
    /// </summary>
    public void RefreshPreinstallSelect(int index = -1)
    {
        var preinstall = EditorTileMap.RoomSplit.Preinstall;
        var optionButton = S_PreinstallOption.Instance;
        var selectIndex = index < 0 ? (preinstall.Count > 0 ? 0 : -1) : index;
        optionButton.Clear();
        foreach (var item in preinstall)
        {
            if (item.WaveList == null)
            {
                item.WaveList = new List<List<MarkInfo>>();
            }

            optionButton.AddItem($"{item.Name} ({item.Weight})");
        }
        
        //下拉框选中项
        optionButton.Selected = selectIndex;
        OnItemSelected(selectIndex);
    }

    /// <summary>
    /// 下拉框选中项
    /// </summary>
    public void OnItemSelected(long index)
    {
        //清除选中项
        RemoveSelectCell();
        EditorTileMap.SelectWaveIndex = -1;
        //记录选中波数
        EditorTileMap.SelectPreinstallIndex = (int)index;
        var preinstall = EditorTileMap.RoomSplit.Preinstall;
        if (index >= 0 && index <= preinstall.Count)
        {
            _grid.SetDataList(preinstall[(int)index].WaveList.ToArray());
        }
        else
        {
            _grid.RemoveAll();
        }
    }
    
    /// <summary>
    /// 选中 cell, 并设置显示的工具
    /// </summary>
    /// <param name="uiCell">选中 cell 对象</param>
    /// <param name="toolRoot">按钮工具绑定的父节点</param>
    /// <param name="toolType">选择工具类型</param>
    public void SetSelectCell(IUiCell uiCell, Node toolRoot, SelectToolType toolType)
    {
        if (SelectCell == uiCell)
        {
            return;
        }

        if (uiCell == null)
        {
            RemoveSelectCell();
            return;
        }

        if (toolType == SelectToolType.Wave) //不需要显示编辑波数按钮
        {
            S_DynamicTool.L_EditButton.Instance.Visible = false;
        }
        else
        {
            S_DynamicTool.L_EditButton.Instance.Visible = true;
        }

        //显示工具
        S_DynamicTool.Instance.Visible = true;
        
        //改变所在父节点
        var parent = S_DynamicTool.Instance.GetParent();
        if (parent != null)
        {
            parent.RemoveChild(S_DynamicTool.Instance);
        }

        toolRoot.AddChild(S_DynamicTool.Instance);
        if (SelectCell != null)
        {
            SelectCell.OnUnSelect();
        }
        SelectCell = uiCell;
        ToolType = toolType;
        SelectCell.OnSelect();
    }

    /// <summary>
    /// 移除选中的 cell 对象
    /// </summary>
    public void RemoveSelectCell()
    {
        if (SelectCell == null)
        {
            return;
        }
        var parent = S_DynamicTool.GetParent();
        if (parent != null)
        {
            parent.RemoveChild(S_DynamicTool.Instance);
        }
        SelectCell.OnUnSelect();
        SelectCell = null;
    }

    /// <summary>
    /// 创建预设
    /// </summary>
    public void OnAddPreinstall()
    {
        var roomSplitPreinstall = EditorTileMap.RoomSplit.Preinstall;
        EditorWindowManager.ShowCreatePreinstall(roomSplitPreinstall, preinstall =>
        {
            //创建逻辑
            roomSplitPreinstall.Add(preinstall);
            RefreshPreinstallSelect(roomSplitPreinstall.Count - 1);
        });
    }

    /// <summary>
    /// 编辑预设
    /// </summary>
    public void OnEditPreinstall()
    {
        var roomSplitPreinstall = EditorTileMap.RoomSplit.Preinstall;
        var selectPreinstall = GetSelectPreinstall();
        EditorWindowManager.ShowEditPreinstall(roomSplitPreinstall, selectPreinstall, preinstall =>
        {
            //修改逻辑
            selectPreinstall.CloneFrom(preinstall);
            //修改下拉菜单数据
            var optionButton = S_PreinstallOption.Instance;
            optionButton.SetItemText(optionButton.Selected, $"{preinstall.Name} ({preinstall.Weight})");
        });
    }

    /// <summary>
    /// 删除预设
    /// </summary>
    public void OnDeletePreinstall()
    {
        var index = EditorTileMap.SelectPreinstallIndex;
        if (index < 0)
        {
            return;
        }
        
        EditorWindowManager.ShowConfirm("提示", "是否删除当前预设？", v =>
        {
            if (v)
            {
                //先把选中项置为-1
                EditorTileMap.SelectPreinstallIndex = -1;
                //移除预设数据
                EditorTileMap.RoomSplit.Preinstall.RemoveAt(index);
                //刷新选项
                RefreshPreinstallSelect(EditorTileMap.RoomSplit.Preinstall.Count - 1);
            }
        });
    }

    /// <summary>
    /// 添加波数
    /// </summary>
    public void OnAddWave()
    {
        var index = S_PreinstallOption.Instance.Selected;
        if (index == -1)
        {
            EditorWindowManager.ShowTips("警告", "请先选择预设!");
            return;
        }

        var preinstall = EditorTileMap.RoomSplit.Preinstall;
        if (index >= preinstall.Count)
        {
            EditorWindowManager.ShowTips("警告", "未知预设选项!");
            return;
        }
        var item = preinstall[index];
        var wave = new List<MarkInfo>();
        item.WaveList.Add(wave);
        _grid.Add(wave);
    }

    //工具节点编辑按钮点击
    private void OnToolEditClick()
    {
        if (ToolType == SelectToolType.Mark)
        {
            OnEditMark();
        }
    }
    
    //工具节点删除按钮点击
    private void OnToolDeleteClick()
    {
        if (ToolType == SelectToolType.Wave)
        {
            OnDeleteWave();
        }
        else if (ToolType == SelectToolType.Mark)
        {
            OnDeleteMark();
        }
    }


    /// <summary>
    /// 删除波数据
    /// </summary>
    public void OnDeleteWave()
    {
        var index = EditorTileMap.SelectWaveIndex;
        if (index < 0)
        {
            return;
        }
        
        var selectPreinstall = GetSelectPreinstall();
        if (selectPreinstall == null)
        {
            return;
        }

        var wave = selectPreinstall.WaveList[index];
        EditorWindowManager.ShowConfirm("提示", $"是否删除当前波？\n当前波数包含{wave.Count}个标记", v =>
        {
            if (v)
            {
                //隐藏工具
                S_DynamicTool.Reparent(this);
                S_DynamicTool.Instance.Visible = false;
                //派发移除标记事件
                foreach (var markInfo in wave)
                {
                    EventManager.EmitEvent(EventEnum.OnDeleteMark, markInfo);
                }
                //移除数据
                selectPreinstall.WaveList.RemoveAt(index);
                _grid.RemoveByIndex(index);
                EditorTileMap.SelectWaveIndex = -1;
            }
        });
    }
    
    /// <summary>
    /// 编辑标记数据
    /// </summary>
    public void OnEditMark()
    {
        if (SelectCell is EditorMarkCell markCell)
        {
            var dataMarkInfo = markCell.Data.MarkInfo;
            //打开编辑面板
            EditorWindowManager.ShowEditMark(dataMarkInfo, (mark) =>
            {
                //为了引用不变, 所以这里使用克隆数据
                dataMarkInfo.CloneFrom(mark);
                //刷新 Cell
                markCell.SetData(markCell.Data);
                EventManager.EmitEvent(EventEnum.OnEditMark, dataMarkInfo);
            });
        }
    }
    
    /// <summary>
    /// 删除标记数据
    /// </summary>
    public void OnDeleteMark()
    {
        if (SelectCell is EditorMarkCell markCell)
        {
            var index = EditorTileMap.SelectWaveIndex;
            if (index < 0)
            {
                return;
            }

            var selectPreinstall = GetSelectPreinstall();
            if (selectPreinstall == null)
            {
                return;
            }

            EditorWindowManager.ShowConfirm("提示", "是否删除当前标记？", v =>
            {
                if (v)
                {
                    var waveCell = (EditorWaveCell)_grid.GetCell(index);
                    //隐藏工具
                    S_DynamicTool.Reparent(this);
                    S_DynamicTool.Instance.Visible = false;
                    var markCellIndex = markCell.Index;
                    var markInfo = waveCell.Data[markCellIndex];
                    //派发移除标记事件
                    EventManager.EmitEvent(EventEnum.OnDeleteMark, markInfo);
                    waveCell.MarkGrid.RemoveByIndex(markCellIndex);
                    waveCell.Data.RemoveAt(markCellIndex);
                }
            });
        }
    }
}
