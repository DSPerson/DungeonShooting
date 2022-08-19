using System;
using Godot;

[RegisterWeapon("1001", typeof(WeaponAttribute))]
[RegisterWeapon("1002")]
/// <summary>
/// 普通的枪
/// </summary>
public class Gun : Weapon
{
    
    /// <summary>
    /// 子弹预制体
    /// </summary>
    public PackedScene BulletPack;
    /// <summary>
    /// 弹壳预制体
    /// </summary>
    public PackedScene ShellPack;

    [RegisterWeaponFunction("1003")]
    private static Gun Test(string id)
    {
        return new Gun(id, new WeaponAttribute());
    }

    public Gun(string id, WeaponAttribute attribute): base(id, attribute)
    {
        BulletPack = ResourceManager.Load<PackedScene>("res://prefab/weapon/bullet/OrdinaryBullets.tscn");
        ShellPack = ResourceManager.Load<PackedScene>("res://prefab/weapon/shell/ShellCase.tscn");
    }

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
        var sprite = ShellPack.Instance<Sprite>();
        sprite.StartThrow<ThrowWeapon>(new Vector2(5, 10), startPos, startHeight, direction, xf, yf, rotate, sprite);
        //创建抖动
        MainCamera.Main.ProssesDirectionalShake(Vector2.Right.Rotated(GlobalRotation) * 1.5f);
    }

    protected override void OnShootBullet()
    {
        //创建子弹
        CreateBullet(BulletPack, FirePoint.GlobalPosition, (FirePoint.GlobalPosition - OriginPoint.GlobalPosition).Angle());
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