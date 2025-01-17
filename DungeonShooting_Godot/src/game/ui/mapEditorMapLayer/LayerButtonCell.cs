﻿using UI.MapEditor;

namespace UI.MapEditorMapLayer;

public class LayerButtonCell : UiCell<MapEditorMapLayer.LayerButton, MapEditorMapLayerPanel.LayerButtonData>
{
    private bool _visible;
    
    public override void OnInit()
    {
        CellNode.L_VisibleButton.Instance.Pressed += OnVisibleButtonClick;
    }

    public override void OnSetData(MapEditorMapLayerPanel.LayerButtonData data)
    {
        if (data.IsLock)
        {
            CellNode.Instance.Icon = ResourceManager.LoadTexture2D(ResourcePath.resource_sprite_ui_commonIcon_Lock_png);
        }
        else
        {
            CellNode.Instance.Icon = ResourceManager.LoadTexture2D(ResourcePath.resource_sprite_ui_commonIcon_Unlock_png);
        }

        CellNode.Instance.Text = data.Title;
        var panel = CellNode.UiPanel.ParentUi as MapEditorPanel;
        if (panel != null)
        {
            if (Data.Layer == EditorTileMap.MarkLayer) //标记层
            {
                _visible = true;
            }
            else
            {
                _visible = panel.S_TileMap.Instance.IsLayerEnabled(data.Layer);
            }
            SetVisibleIcon(_visible);
        }
    }

    private void OnVisibleButtonClick()
    {
        var panel = CellNode.UiPanel.ParentUi as MapEditorPanel;
        if (panel != null)
        {
            _visible = !_visible;
            if (Data.Layer == EditorTileMap.MarkLayer) //隐藏标记层
            {
                panel.S_MapEditorTools.Instance.S_ToolRoot.Instance.Visible = _visible;
            }
            else //隐藏地图层级
            {
                panel.S_TileMap.Instance.SetLayerEnabled(Data.Layer, _visible);
            }
            SetVisibleIcon(_visible);
        }
    }

    private void SetVisibleIcon(bool visible)
    {
        if (visible)
        {
            CellNode.L_VisibleButton.Instance.TextureNormal = ResourceManager.LoadTexture2D(ResourcePath.resource_sprite_ui_commonIcon_Visible_png);
        }
        else
        {
            CellNode.L_VisibleButton.Instance.TextureNormal = ResourceManager.LoadTexture2D(ResourcePath.resource_sprite_ui_commonIcon_Hide_png);
        }
    }
}