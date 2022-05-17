using Godot;

/// <summary>
/// 鼠标指针
/// </summary>
public class Cursor : Node2D
{

    public Gun TargetGun = null;

    private Sprite lt;
    private Sprite lb;
    private Sprite rt;
    private Sprite rb;

    public override void _Ready()
    {
        lt = GetNode<Sprite>("LT");
        lb = GetNode<Sprite>("LB");
        rt = GetNode<Sprite>("RT");
        rb = GetNode<Sprite>("RB");
    }

    public override void _Input(InputEvent @event)
    {
        if (@event is InputEventMouse eve)
        {
            Position = eve.Position;
        }
    }

    public override void _Process(float delta)
    {
        if (TargetGun != null)
        {
            SetScope(TargetGun.CurrScatteringRange);
        }
        else
        {
            SetScope(0);
        }
    }

    private void SetScope(float scope)
    {
        if (TargetGun != null)
        {
            var len = GlobalPosition.DistanceTo(TargetGun.GlobalPosition);
            if (TargetGun.Attribute != null)
            {
                len = Mathf.Max(0, len - TargetGun.Attribute.BarrelLength);
            }
            scope = len / GameConfig.ScatteringDistance * scope;
        }
        lt.Position = new Vector2(-scope, -scope);
        lb.Position = new Vector2(-scope, scope);
        rt.Position = new Vector2(scope, -scope);
        rb.Position = new Vector2(scope, scope);
    }
}