using Godot;


/// <summary>
/// 普通的枪
/// </summary>
public class OrdinaryGun : Gun
{

    [Export] public PackedScene bulletPacked;

    [Export] public PackedScene shell;

    protected override void Init()
    {

    }

    protected override void Fire()
    {
        //创建一个弹壳
        var temp = new Shell();
        var startPos = GlobalPosition + new Vector2(0, 5);
        var startHeight = 6;
        var direction = GlobalRotationDegrees + MathUtils.RandRangeInt(-30, 30) + 180;
        var xf = MathUtils.RandRangeInt(20, 60);
        var yf = MathUtils.RandRangeInt(60, 120);
        var rotate = MathUtils.RandRangeInt(-720, 720);
        var sprite = shell.Instance<Sprite>();
        temp.InitThrow(new Vector2(5, 10), startPos, startHeight, direction, xf, yf, rotate, sprite, sprite.Texture);
        RoomManager.Current.ItemRoot.AddChild(temp);
    }

    protected override void ShootBullet()
    {
        //创建子弹
        CreateBullet(bulletPacked, FirePoint.GlobalPosition, (FirePoint.GlobalPosition - OriginPoint.GlobalPosition).Angle());
    }
}