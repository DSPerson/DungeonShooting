namespace UI.RoomUI;

/// <summary>
/// Ui代码, 该类是根据ui场景自动生成的, 请不要手动编辑该类, 以免造成代码丢失
/// </summary>
public abstract partial class RoomUI : UiBase
{
    /// <summary>
    /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.Node2D"/>, 节点路径: RoomUI.InteractiveTipBar
    /// </summary>
    public UiNode1_InteractiveTipBar L_InteractiveTipBar
    {
        get
        {
            if (_L_InteractiveTipBar == null) _L_InteractiveTipBar = new UiNode1_InteractiveTipBar(GetNode<Godot.Node2D>("InteractiveTipBar"));
            return _L_InteractiveTipBar;
        }
    }
    private UiNode1_InteractiveTipBar _L_InteractiveTipBar;

    /// <summary>
    /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.Node2D"/>, 节点路径: RoomUI.ReloadBar
    /// </summary>
    public UiNode5_ReloadBar L_ReloadBar
    {
        get
        {
            if (_L_ReloadBar == null) _L_ReloadBar = new UiNode5_ReloadBar(GetNode<Godot.Node2D>("ReloadBar"));
            return _L_ReloadBar;
        }
    }
    private UiNode5_ReloadBar _L_ReloadBar;

    /// <summary>
    /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.Control"/>, 节点路径: RoomUI.Control
    /// </summary>
    public UiNode8_Control L_Control
    {
        get
        {
            if (_L_Control == null) _L_Control = new UiNode8_Control(GetNode<Godot.Control>("Control"));
            return _L_Control;
        }
    }
    private UiNode8_Control _L_Control;

    /// <summary>
    /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Cursor"/>, 节点路径: RoomUI.Cursor
    /// </summary>
    public UiNode18_Cursor L_Cursor
    {
        get
        {
            if (_L_Cursor == null) _L_Cursor = new UiNode18_Cursor(GetNode<Cursor>("Cursor"));
            return _L_Cursor;
        }
    }
    private UiNode18_Cursor _L_Cursor;



    /// <summary>
    /// 类型: <see cref="Godot.Sprite2D"/>, 路径: RoomUI.InteractiveTipBar.Icon
    /// </summary>
    public class UiNode2_Icon
    {
        /// <summary>
        /// Ui节点实例, 节点类型: <see cref="Godot.Sprite2D"/>, 节点路径: RoomUI.InteractiveTipBar.Icon
        /// </summary>
        public Godot.Sprite2D Instance { get; }

        public UiNode2_Icon(Godot.Sprite2D node) => Instance = node;
        public UiNode2_Icon Clone() => new ((Godot.Sprite2D)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.Sprite2D"/>, 路径: RoomUI.InteractiveTipBar.InteractiveIcon
    /// </summary>
    public class UiNode3_InteractiveIcon
    {
        /// <summary>
        /// Ui节点实例, 节点类型: <see cref="Godot.Sprite2D"/>, 节点路径: RoomUI.InteractiveTipBar.InteractiveIcon
        /// </summary>
        public Godot.Sprite2D Instance { get; }

        public UiNode3_InteractiveIcon(Godot.Sprite2D node) => Instance = node;
        public UiNode3_InteractiveIcon Clone() => new ((Godot.Sprite2D)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.Line2D"/>, 路径: RoomUI.InteractiveTipBar.Line2D
    /// </summary>
    public class UiNode4_Line2D
    {
        /// <summary>
        /// Ui节点实例, 节点类型: <see cref="Godot.Line2D"/>, 节点路径: RoomUI.InteractiveTipBar.Line2D
        /// </summary>
        public Godot.Line2D Instance { get; }

        public UiNode4_Line2D(Godot.Line2D node) => Instance = node;
        public UiNode4_Line2D Clone() => new ((Godot.Line2D)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.Node2D"/>, 路径: RoomUI.InteractiveTipBar
    /// </summary>
    public class UiNode1_InteractiveTipBar
    {
        /// <summary>
        /// Ui节点实例, 节点类型: <see cref="Godot.Node2D"/>, 节点路径: RoomUI.InteractiveTipBar
        /// </summary>
        public Godot.Node2D Instance { get; }

        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.Sprite2D"/>, 节点路径: RoomUI.Icon
        /// </summary>
        public UiNode2_Icon L_Icon
        {
            get
            {
                if (_L_Icon == null) _L_Icon = new UiNode2_Icon(Instance.GetNode<Godot.Sprite2D>("Icon"));
                return _L_Icon;
            }
        }
        private UiNode2_Icon _L_Icon;

        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.Sprite2D"/>, 节点路径: RoomUI.InteractiveIcon
        /// </summary>
        public UiNode3_InteractiveIcon L_InteractiveIcon
        {
            get
            {
                if (_L_InteractiveIcon == null) _L_InteractiveIcon = new UiNode3_InteractiveIcon(Instance.GetNode<Godot.Sprite2D>("InteractiveIcon"));
                return _L_InteractiveIcon;
            }
        }
        private UiNode3_InteractiveIcon _L_InteractiveIcon;

        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.Line2D"/>, 节点路径: RoomUI.Line2D
        /// </summary>
        public UiNode4_Line2D L_Line2D
        {
            get
            {
                if (_L_Line2D == null) _L_Line2D = new UiNode4_Line2D(Instance.GetNode<Godot.Line2D>("Line2D"));
                return _L_Line2D;
            }
        }
        private UiNode4_Line2D _L_Line2D;

        public UiNode1_InteractiveTipBar(Godot.Node2D node) => Instance = node;
        public UiNode1_InteractiveTipBar Clone() => new ((Godot.Node2D)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.Sprite2D"/>, 路径: RoomUI.ReloadBar.Slot.Block
    /// </summary>
    public class UiNode7_Block
    {
        /// <summary>
        /// Ui节点实例, 节点类型: <see cref="Godot.Sprite2D"/>, 节点路径: RoomUI.ReloadBar.Slot.Block
        /// </summary>
        public Godot.Sprite2D Instance { get; }

        public UiNode7_Block(Godot.Sprite2D node) => Instance = node;
        public UiNode7_Block Clone() => new ((Godot.Sprite2D)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.Sprite2D"/>, 路径: RoomUI.ReloadBar.Slot
    /// </summary>
    public class UiNode6_Slot
    {
        /// <summary>
        /// Ui节点实例, 节点类型: <see cref="Godot.Sprite2D"/>, 节点路径: RoomUI.ReloadBar.Slot
        /// </summary>
        public Godot.Sprite2D Instance { get; }

        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.Sprite2D"/>, 节点路径: RoomUI.ReloadBar.Block
        /// </summary>
        public UiNode7_Block L_Block
        {
            get
            {
                if (_L_Block == null) _L_Block = new UiNode7_Block(Instance.GetNode<Godot.Sprite2D>("Block"));
                return _L_Block;
            }
        }
        private UiNode7_Block _L_Block;

        public UiNode6_Slot(Godot.Sprite2D node) => Instance = node;
        public UiNode6_Slot Clone() => new ((Godot.Sprite2D)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.Node2D"/>, 路径: RoomUI.ReloadBar
    /// </summary>
    public class UiNode5_ReloadBar
    {
        /// <summary>
        /// Ui节点实例, 节点类型: <see cref="Godot.Node2D"/>, 节点路径: RoomUI.ReloadBar
        /// </summary>
        public Godot.Node2D Instance { get; }

        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.Sprite2D"/>, 节点路径: RoomUI.Slot
        /// </summary>
        public UiNode6_Slot L_Slot
        {
            get
            {
                if (_L_Slot == null) _L_Slot = new UiNode6_Slot(Instance.GetNode<Godot.Sprite2D>("Slot"));
                return _L_Slot;
            }
        }
        private UiNode6_Slot _L_Slot;

        public UiNode5_ReloadBar(Godot.Node2D node) => Instance = node;
        public UiNode5_ReloadBar Clone() => new ((Godot.Node2D)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.TextureRect"/>, 路径: RoomUI.Control.HealthBar.HpSlot.HpBar
    /// </summary>
    public class UiNode11_HpBar
    {
        /// <summary>
        /// Ui节点实例, 节点类型: <see cref="Godot.TextureRect"/>, 节点路径: RoomUI.Control.HealthBar.HpSlot.HpBar
        /// </summary>
        public Godot.TextureRect Instance { get; }

        public UiNode11_HpBar(Godot.TextureRect node) => Instance = node;
        public UiNode11_HpBar Clone() => new ((Godot.TextureRect)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.NinePatchRect"/>, 路径: RoomUI.Control.HealthBar.HpSlot
    /// </summary>
    public class UiNode10_HpSlot
    {
        /// <summary>
        /// Ui节点实例, 节点类型: <see cref="Godot.NinePatchRect"/>, 节点路径: RoomUI.Control.HealthBar.HpSlot
        /// </summary>
        public Godot.NinePatchRect Instance { get; }

        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.TextureRect"/>, 节点路径: RoomUI.Control.HealthBar.HpBar
        /// </summary>
        public UiNode11_HpBar L_HpBar
        {
            get
            {
                if (_L_HpBar == null) _L_HpBar = new UiNode11_HpBar(Instance.GetNode<Godot.TextureRect>("HpBar"));
                return _L_HpBar;
            }
        }
        private UiNode11_HpBar _L_HpBar;

        public UiNode10_HpSlot(Godot.NinePatchRect node) => Instance = node;
        public UiNode10_HpSlot Clone() => new ((Godot.NinePatchRect)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.TextureRect"/>, 路径: RoomUI.Control.HealthBar.ShieldSlot.ShieldBar
    /// </summary>
    public class UiNode13_ShieldBar
    {
        /// <summary>
        /// Ui节点实例, 节点类型: <see cref="Godot.TextureRect"/>, 节点路径: RoomUI.Control.HealthBar.ShieldSlot.ShieldBar
        /// </summary>
        public Godot.TextureRect Instance { get; }

        public UiNode13_ShieldBar(Godot.TextureRect node) => Instance = node;
        public UiNode13_ShieldBar Clone() => new ((Godot.TextureRect)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.NinePatchRect"/>, 路径: RoomUI.Control.HealthBar.ShieldSlot
    /// </summary>
    public class UiNode12_ShieldSlot
    {
        /// <summary>
        /// Ui节点实例, 节点类型: <see cref="Godot.NinePatchRect"/>, 节点路径: RoomUI.Control.HealthBar.ShieldSlot
        /// </summary>
        public Godot.NinePatchRect Instance { get; }

        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.TextureRect"/>, 节点路径: RoomUI.Control.HealthBar.ShieldBar
        /// </summary>
        public UiNode13_ShieldBar L_ShieldBar
        {
            get
            {
                if (_L_ShieldBar == null) _L_ShieldBar = new UiNode13_ShieldBar(Instance.GetNode<Godot.TextureRect>("ShieldBar"));
                return _L_ShieldBar;
            }
        }
        private UiNode13_ShieldBar _L_ShieldBar;

        public UiNode12_ShieldSlot(Godot.NinePatchRect node) => Instance = node;
        public UiNode12_ShieldSlot Clone() => new ((Godot.NinePatchRect)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.TextureRect"/>, 路径: RoomUI.Control.HealthBar
    /// </summary>
    public class UiNode9_HealthBar
    {
        /// <summary>
        /// Ui节点实例, 节点类型: <see cref="Godot.TextureRect"/>, 节点路径: RoomUI.Control.HealthBar
        /// </summary>
        public Godot.TextureRect Instance { get; }

        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.NinePatchRect"/>, 节点路径: RoomUI.Control.HpSlot
        /// </summary>
        public UiNode10_HpSlot L_HpSlot
        {
            get
            {
                if (_L_HpSlot == null) _L_HpSlot = new UiNode10_HpSlot(Instance.GetNode<Godot.NinePatchRect>("HpSlot"));
                return _L_HpSlot;
            }
        }
        private UiNode10_HpSlot _L_HpSlot;

        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.NinePatchRect"/>, 节点路径: RoomUI.Control.ShieldSlot
        /// </summary>
        public UiNode12_ShieldSlot L_ShieldSlot
        {
            get
            {
                if (_L_ShieldSlot == null) _L_ShieldSlot = new UiNode12_ShieldSlot(Instance.GetNode<Godot.NinePatchRect>("ShieldSlot"));
                return _L_ShieldSlot;
            }
        }
        private UiNode12_ShieldSlot _L_ShieldSlot;

        public UiNode9_HealthBar(Godot.TextureRect node) => Instance = node;
        public UiNode9_HealthBar Clone() => new ((Godot.TextureRect)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.TextureRect"/>, 路径: RoomUI.Control.MapBar
    /// </summary>
    public class UiNode14_MapBar
    {
        /// <summary>
        /// Ui节点实例, 节点类型: <see cref="Godot.TextureRect"/>, 节点路径: RoomUI.Control.MapBar
        /// </summary>
        public Godot.TextureRect Instance { get; }

        public UiNode14_MapBar(Godot.TextureRect node) => Instance = node;
        public UiNode14_MapBar Clone() => new ((Godot.TextureRect)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.TextureRect"/>, 路径: RoomUI.Control.GunBar.GunSprite
    /// </summary>
    public class UiNode16_GunSprite
    {
        /// <summary>
        /// Ui节点实例, 节点类型: <see cref="Godot.TextureRect"/>, 节点路径: RoomUI.Control.GunBar.GunSprite
        /// </summary>
        public Godot.TextureRect Instance { get; }

        public UiNode16_GunSprite(Godot.TextureRect node) => Instance = node;
        public UiNode16_GunSprite Clone() => new ((Godot.TextureRect)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.Label"/>, 路径: RoomUI.Control.GunBar.BulletText
    /// </summary>
    public class UiNode17_BulletText
    {
        /// <summary>
        /// Ui节点实例, 节点类型: <see cref="Godot.Label"/>, 节点路径: RoomUI.Control.GunBar.BulletText
        /// </summary>
        public Godot.Label Instance { get; }

        public UiNode17_BulletText(Godot.Label node) => Instance = node;
        public UiNode17_BulletText Clone() => new ((Godot.Label)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.Control"/>, 路径: RoomUI.Control.GunBar
    /// </summary>
    public class UiNode15_GunBar
    {
        /// <summary>
        /// Ui节点实例, 节点类型: <see cref="Godot.Control"/>, 节点路径: RoomUI.Control.GunBar
        /// </summary>
        public Godot.Control Instance { get; }

        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.TextureRect"/>, 节点路径: RoomUI.Control.GunSprite
        /// </summary>
        public UiNode16_GunSprite L_GunSprite
        {
            get
            {
                if (_L_GunSprite == null) _L_GunSprite = new UiNode16_GunSprite(Instance.GetNode<Godot.TextureRect>("GunSprite"));
                return _L_GunSprite;
            }
        }
        private UiNode16_GunSprite _L_GunSprite;

        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.Label"/>, 节点路径: RoomUI.Control.BulletText
        /// </summary>
        public UiNode17_BulletText L_BulletText
        {
            get
            {
                if (_L_BulletText == null) _L_BulletText = new UiNode17_BulletText(Instance.GetNode<Godot.Label>("BulletText"));
                return _L_BulletText;
            }
        }
        private UiNode17_BulletText _L_BulletText;

        public UiNode15_GunBar(Godot.Control node) => Instance = node;
        public UiNode15_GunBar Clone() => new ((Godot.Control)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.Control"/>, 路径: RoomUI.Control
    /// </summary>
    public class UiNode8_Control
    {
        /// <summary>
        /// Ui节点实例, 节点类型: <see cref="Godot.Control"/>, 节点路径: RoomUI.Control
        /// </summary>
        public Godot.Control Instance { get; }

        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.TextureRect"/>, 节点路径: RoomUI.HealthBar
        /// </summary>
        public UiNode9_HealthBar L_HealthBar
        {
            get
            {
                if (_L_HealthBar == null) _L_HealthBar = new UiNode9_HealthBar(Instance.GetNode<Godot.TextureRect>("HealthBar"));
                return _L_HealthBar;
            }
        }
        private UiNode9_HealthBar _L_HealthBar;

        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.TextureRect"/>, 节点路径: RoomUI.MapBar
        /// </summary>
        public UiNode14_MapBar L_MapBar
        {
            get
            {
                if (_L_MapBar == null) _L_MapBar = new UiNode14_MapBar(Instance.GetNode<Godot.TextureRect>("MapBar"));
                return _L_MapBar;
            }
        }
        private UiNode14_MapBar _L_MapBar;

        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.Control"/>, 节点路径: RoomUI.GunBar
        /// </summary>
        public UiNode15_GunBar L_GunBar
        {
            get
            {
                if (_L_GunBar == null) _L_GunBar = new UiNode15_GunBar(Instance.GetNode<Godot.Control>("GunBar"));
                return _L_GunBar;
            }
        }
        private UiNode15_GunBar _L_GunBar;

        public UiNode8_Control(Godot.Control node) => Instance = node;
        public UiNode8_Control Clone() => new ((Godot.Control)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.Sprite2D"/>, 路径: RoomUI.Cursor.LT
    /// </summary>
    public class UiNode19_LT
    {
        /// <summary>
        /// Ui节点实例, 节点类型: <see cref="Godot.Sprite2D"/>, 节点路径: RoomUI.Cursor.LT
        /// </summary>
        public Godot.Sprite2D Instance { get; }

        public UiNode19_LT(Godot.Sprite2D node) => Instance = node;
        public UiNode19_LT Clone() => new ((Godot.Sprite2D)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.Sprite2D"/>, 路径: RoomUI.Cursor.LB
    /// </summary>
    public class UiNode20_LB
    {
        /// <summary>
        /// Ui节点实例, 节点类型: <see cref="Godot.Sprite2D"/>, 节点路径: RoomUI.Cursor.LB
        /// </summary>
        public Godot.Sprite2D Instance { get; }

        public UiNode20_LB(Godot.Sprite2D node) => Instance = node;
        public UiNode20_LB Clone() => new ((Godot.Sprite2D)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.Sprite2D"/>, 路径: RoomUI.Cursor.RT
    /// </summary>
    public class UiNode21_RT
    {
        /// <summary>
        /// Ui节点实例, 节点类型: <see cref="Godot.Sprite2D"/>, 节点路径: RoomUI.Cursor.RT
        /// </summary>
        public Godot.Sprite2D Instance { get; }

        public UiNode21_RT(Godot.Sprite2D node) => Instance = node;
        public UiNode21_RT Clone() => new ((Godot.Sprite2D)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.Sprite2D"/>, 路径: RoomUI.Cursor.RB
    /// </summary>
    public class UiNode22_RB
    {
        /// <summary>
        /// Ui节点实例, 节点类型: <see cref="Godot.Sprite2D"/>, 节点路径: RoomUI.Cursor.RB
        /// </summary>
        public Godot.Sprite2D Instance { get; }

        public UiNode22_RB(Godot.Sprite2D node) => Instance = node;
        public UiNode22_RB Clone() => new ((Godot.Sprite2D)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Cursor"/>, 路径: RoomUI.Cursor
    /// </summary>
    public class UiNode18_Cursor
    {
        /// <summary>
        /// Ui节点实例, 节点类型: <see cref="Cursor"/>, 节点路径: RoomUI.Cursor
        /// </summary>
        public Cursor Instance { get; }

        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.Sprite2D"/>, 节点路径: RoomUI.LT
        /// </summary>
        public UiNode19_LT L_LT
        {
            get
            {
                if (_L_LT == null) _L_LT = new UiNode19_LT(Instance.GetNode<Godot.Sprite2D>("LT"));
                return _L_LT;
            }
        }
        private UiNode19_LT _L_LT;

        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.Sprite2D"/>, 节点路径: RoomUI.LB
        /// </summary>
        public UiNode20_LB L_LB
        {
            get
            {
                if (_L_LB == null) _L_LB = new UiNode20_LB(Instance.GetNode<Godot.Sprite2D>("LB"));
                return _L_LB;
            }
        }
        private UiNode20_LB _L_LB;

        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.Sprite2D"/>, 节点路径: RoomUI.RT
        /// </summary>
        public UiNode21_RT L_RT
        {
            get
            {
                if (_L_RT == null) _L_RT = new UiNode21_RT(Instance.GetNode<Godot.Sprite2D>("RT"));
                return _L_RT;
            }
        }
        private UiNode21_RT _L_RT;

        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.Sprite2D"/>, 节点路径: RoomUI.RB
        /// </summary>
        public UiNode22_RB L_RB
        {
            get
            {
                if (_L_RB == null) _L_RB = new UiNode22_RB(Instance.GetNode<Godot.Sprite2D>("RB"));
                return _L_RB;
            }
        }
        private UiNode22_RB _L_RB;

        public UiNode18_Cursor(Cursor node) => Instance = node;
        public UiNode18_Cursor Clone() => new ((Cursor)Instance.Duplicate());
    }

}
