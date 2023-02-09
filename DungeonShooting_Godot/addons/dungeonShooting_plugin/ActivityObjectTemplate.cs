using Godot;

namespace Plugin
{
    /// <summary>
    /// ActivityObject 节点模板对象
    /// </summary>
    [Tool]
    public partial class ActivityObjectTemplate : Node
    {
        /// <summary>
        /// 是否放入 ySort 节点下
        /// </summary>
        [Export] public bool UseYSort;

        /// <summary>
        /// 物体初始缩放
        /// </summary>
        [Export] public Vector2 scale = Vector2.One;
        
        /// <summary>
        /// 当前物体所属物理层
        /// </summary>
        [Export(PropertyHint.Layers2DPhysics)] public uint collision_layer;

        /// <summary>
        /// 当前物体扫描的物理层
        /// </summary>
        [Export(PropertyHint.Layers2DPhysics)] public uint collision_mask;

        /// <summary>
        /// 显示状态
        /// </summary>
        [Export] public bool visible = true;
        
        /// <summary>
        /// 当前物体渲染层级
        /// </summary>
        [Export] public int z_index;

        public override void _Ready()
        {
#if TOOLS
            // 在工具模式下创建的 template 节点自动创建对应的必要子节点
            if (Engine.IsEditorHint())
            {
                var parent = GetParent();
                if (parent != null)
                {
                    //寻找 owner
                    Node owner;
                    if (parent.Owner != null)
                    {
                        owner = parent.Owner;
                    }
                    else if (Plugin.Instance.GetEditorInterface().GetEditedSceneRoot() == this)
                    {
                        owner = this;
                    }
                    else
                    {
                        owner = parent;
                    }

                    var sprite = GetNodeOrNull<Sprite2D>("ShadowSprite");
                    //创建Shadow
                    if (sprite == null)
                    {
                        sprite = new Sprite2D();
                        sprite.Name = "ShadowSprite";
                        sprite.ZIndex = -5;
                        var material =
                            ResourceManager.Load<ShaderMaterial>(ResourcePath.resource_materlal_Blend_tres, false);
                        material.SetShaderParameter("blend", new Color(0, 0, 0, 0.47058824F));
                        material.SetShaderParameter("schedule", 1);
                        sprite.Material = material;
                        AddChild(sprite);
                        sprite.Owner = owner;
                    }
                    else if (sprite.Material == null)
                    {
                        var material =
                            ResourceManager.Load<ShaderMaterial>(ResourcePath.resource_materlal_Blend_tres, false);
                        material.SetShaderParameter("blend", new Color(0, 0, 0, 0.47058824F));
                        material.SetShaderParameter("schedule", 1);
                        sprite.Material = material;
                    }

                    var animatedSprite = GetNodeOrNull<AnimatedSprite2D>("AnimatedSprite");
                    //创建 Sprite2D
                    if (animatedSprite == null)
                    {
                        animatedSprite = new AnimatedSprite2D();
                        animatedSprite.Name = "AnimatedSprite";
                        var material =
                            ResourceManager.Load<ShaderMaterial>(ResourcePath.resource_materlal_Blend_tres, false);
                        material.SetShaderParameter("blend", new Color(1, 1, 1, 1));
                        material.SetShaderParameter("schedule", 0);
                        animatedSprite.Material = material;
                        AddChild(animatedSprite);
                        animatedSprite.Owner = owner;
                    }
                    else if (animatedSprite.Material == null)
                    {
                        var material =
                            ResourceManager.Load<ShaderMaterial>(ResourcePath.resource_materlal_Blend_tres, false);
                        material.SetShaderParameter("blend", new Color(1, 1, 1, 1));
                        material.SetShaderParameter("schedule", 0);
                        animatedSprite.Material = material;
                    }

                    //创建Collision
                    if (GetNodeOrNull("Collision") == null)
                    {
                        var co = new CollisionShape2D();
                        co.Name = "Collision";
                        AddChild(co);
                        co.Owner = owner;
                    }

                    //创建AnimationPlayer
                    if (GetNodeOrNull("AnimationPlayer") == null)
                    {
                        var ap = new AnimationPlayer();
                        ap.Name = "AnimationPlayer";
                        AddChild(ap);
                        ap.Owner = owner;
                    }
                }
            }
#endif
        }
    }
}