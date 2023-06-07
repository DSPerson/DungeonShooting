
using Godot;

/// <summary>
/// 弹壳类
/// </summary>
[Tool, GlobalClass]
public partial class Shell : ActivityObject
{
    public override void OnInit()
    {
        base.OnInit();
        ShadowOffset = new Vector2(0, 1);
        ThrowCollisionSize = new Vector2(5, 5);
    }

    protected override void OnThrowOver()
    {
        EnableBehavior = false;
        Collision.QueueFree();
    }
}