using Godot;

/// <summary>
/// 高速子弹
/// </summary>
public class HighSpeedBullet : Bullet
{
    //射线检测节点
    private RayCast2D RayCast2D;
    //最大飞行距离
    private float Distance;
    private Line2D Line;
    private float ca = 1;

    public void InitData(float distance, Color color)
    {
        RayCast2D = GetNode<RayCast2D>("RayCast2D");
        Line = GetNode<Line2D>("Line");
        Distance = distance;
        Modulate = color;

        Vector2 targetPos = new Vector2(distance, 0);

        //
        RayCast2D.CastTo = targetPos;
        RayCast2D.ForceRaycastUpdate();
        if (RayCast2D.IsColliding()) {
            //划线的点坐标
            Line.SetPointPosition(1, new Vector2(Line.GlobalPosition.DistanceTo(RayCast2D.GetCollisionPoint()), 0));
        }
        else
        {
            //划线的点坐标
            Line.SetPointPosition(1, targetPos);
        }
        RayCast2D.Enabled = false;
    }

    public override void _Process(float delta)
    {
        ca -= 12 * delta;
        if (ca <= 0) {
            QueueFree();
            return;
        }
        Color c = Modulate;
        c.a = ca;
        Modulate = c;
    }
}