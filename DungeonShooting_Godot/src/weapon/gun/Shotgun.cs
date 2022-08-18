using Godot;

public class Shotgun : Weapon
{
    protected override void Init()
    {

    }

    protected override void OnFire()
    {
        //创建一个弹壳
        var startPos = GlobalPosition + new Vector2(0, 5);
        var startHeight = 6;
        var direction = GlobalRotationDegrees + MathUtils.RandRangeInt(-30, 30) + 180;
        var xf = MathUtils.RandRangeInt(20, 60);
        var yf = MathUtils.RandRangeInt(60, 120);
        var rotate = MathUtils.RandRangeInt(-720, 720);
        var sprite = Attribute.ShellPack.Instance<Sprite>();
        sprite.StartThrow<ThrowWeapon>(new Vector2(5, 10), startPos, startHeight, direction, xf, yf, rotate, sprite);
        //创建抖动
        MainCamera.Main.ProssesDirectionalShake(Vector2.Right.Rotated(GlobalRotation) * 1.5f);
    }

    protected override void OnShootBullet()
    {
        for (int i = 0; i < 5; i++)
        {
            //创建子弹
            CreateBullet(Attribute.BulletPack, FirePoint.GlobalPosition, (FirePoint.GlobalPosition - OriginPoint.GlobalPosition).Angle() + MathUtils.RandRange(-20 / 180f * Mathf.Pi, 20 / 180f * Mathf.Pi));
        }
    }

    protected override void OnReload()
    {
        
    }

    protected override void OnPickUp(Role master)
    {
        
    }

    protected override void OnThrowOut()
    {

    }

    protected override void OnActive()
    {
        
    }

    protected override void OnConceal()
    {
        
    }
}
