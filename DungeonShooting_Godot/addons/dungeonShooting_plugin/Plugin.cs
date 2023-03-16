#if TOOLS
using Godot;

namespace Plugin
{
    [Tool]
    public partial class Plugin : EditorPlugin
    {
        public static Plugin Instance => _instance;
        private static Plugin _instance;

        private Control dock;

        public override void _Process(double delta)
        {
            _instance = this;
        }

        public override void _EnterTree()
        {
            _instance = this;
            var script = GD.Load<Script>("res://src/framework/activity/ActivityObjectTemplate.cs");
            var texture = GD.Load<Texture2D>("res://addons/dungeonShooting_plugin/ActivityObject.svg");
            AddCustomType("ActivityObjectTemplate", "Node", script, texture);

            var script2 = GD.Load<Script>("res://src/framework/map/DungeonRoomTemplate.cs");
            var texture2 = GD.Load<Texture2D>("res://addons/dungeonShooting_plugin/Map.svg");
            AddCustomType("DungeonRoomTemplate", "TileMap", script2, texture2);
            
            var script3 = GD.Load<Script>("res://src/framework/map/mark/ActivityMark.cs");
            var texture3 = GD.Load<Texture2D>("res://addons/dungeonShooting_plugin/Mark.svg");
            AddCustomType("ActivityMark", "Node2D", script3, texture3);
            
            var script4 = GD.Load<Script>("res://src/framework/map/mark/EnemyMark.cs");
            AddCustomType("EnemyMark", "Node2D", script4, texture3);
            
            var script5 = GD.Load<Script>("res://src/framework/map/mark/WeaponMark.cs");
            AddCustomType("WeaponMark", "Node2D", script5, texture3);
            
            dock = GD.Load<PackedScene>("res://addons/dungeonShooting_plugin/Tools.tscn").Instantiate<Control>();
            //AddControlToDock(DockSlot.LeftUr, dock);
            
            //AddControlToContainer();
            var editorMainScreen = GetEditorInterface().GetEditorMainScreen();
            editorMainScreen.AddChild(dock);
            _MakeVisible(false);
        }

        public override void _ExitTree()
        {
            RemoveCustomType("ActivityObjectTemplate");
            RemoveCustomType("DungeonRoomTemplate");
            RemoveCustomType("ActivityMark");
            RemoveCustomType("EnemyMark");
            RemoveCustomType("WeaponMark");

            if (dock != null)
            {
                //RemoveControlFromDocks(dock);
                dock.Free();
                dock = null;
            }
        }

        public override bool _HasMainScreen()
        {
            return true;
        }

        public override Texture2D _GetPluginIcon()
        {
            return GD.Load<Texture2D>("res://addons/dungeonShooting_plugin/Tool.svg");
        }

        public override string _GetPluginName()
        {
            return "Tools";
        }

        public override void _MakeVisible(bool visible)
        {
            if (dock != null)
            {
                dock.Visible = visible;
            }
        }
    }
}
#endif
