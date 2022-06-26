using System.Collections.Generic;
using Godot;

public static class GunManager
{

    public static Gun GetGun1()
    {
        //加载枪的 prefab
        var gun = ResourceManager.LoadGunAndInstance("res://prefab/weapon/Gun.tscn");
        //设置基础属性
        var attr = new GunAttribute();
        attr.Id = "1";
        attr.Name = "Gun1";
        attr.Weight = 40;
        attr.CenterPosition = new Vector2(0.4f, -2.6f);
        attr.StartFiringSpeed = 480;
        attr.StartScatteringRange = 30;
        attr.FinalScatteringRange = 90;
        attr.ScatteringRangeAddValue = 2f;
        attr.ScatteringRangeBackSpeed = 40;
        //连发
        attr.ContinuousShoot = true;
        //扳机检测间隔
        attr.TriggerInterval = 0f;
        //连发数量
        attr.MinContinuousCount = 3;
        attr.MaxContinuousCount = 3;
        //开火前延时
        attr.DelayedTime = 0f;
        //攻击距离
        attr.MinDistance = 500;
        attr.MaxDistance = 600;
        //发射子弹数量
        attr.MinFireBulletCount = 1;
        attr.MaxFireBulletCount = 1;
        //抬起角度
        attr.UpliftAngle = 10;
        //枪身长度
        attr.FirePosition = new Vector2(16, 1.5f);
        attr.Sprite = ResourceManager.Load<Texture>("res://resource/sprite/gun/gun4.png");
        attr.BulletPack = ResourceManager.Load<PackedScene>("res://prefab/weapon/bullet/OrdinaryBullets.tscn");
        attr.ShellPack = ResourceManager.Load<PackedScene>("res://prefab/weapon/shell/ShellCase.tscn");
        gun.Init(attr);
        return gun;
    }

    public static Gun GetGun2()
    {
        //加载枪的 prefab
        var gun = ResourceManager.LoadGunAndInstance("res://prefab/weapon/Gun.tscn");
        //设置基础属性
        var attr = new GunAttribute();
        attr.Id = "2";
        attr.Name = "Gun2";
        attr.Weight = 20;
        attr.CenterPosition = new Vector2(0.4f, -2.6f);
        attr.WeightType = GunWeightType.DeputyWeapon;
        attr.StartFiringSpeed = 600;
        attr.StartScatteringRange = 5;
        attr.FinalScatteringRange = 60;
        attr.ScatteringRangeAddValue = 8f;
        attr.ScatteringRangeBackSpeed = 40;
        //连发
        attr.ContinuousShoot = false;
        //扳机检测间隔
        attr.TriggerInterval = 0.4f;
        //连发数量
        attr.MinContinuousCount = 3;
        attr.MaxContinuousCount = 3;
        //开火前延时
        attr.DelayedTime = 0f;
        //攻击距离
        attr.MinDistance = 500;
        attr.MaxDistance = 600;
        //发射子弹数量
        attr.MinFireBulletCount = 1;
        attr.MaxFireBulletCount = 1;
        //抬起角度
        attr.UpliftAngle = 30;
        //枪身长度
        attr.FirePosition = new Vector2(10, 1.5f);
        attr.Sprite = ResourceManager.Load<Texture>("res://resource/sprite/gun/gun3.png");
        attr.BulletPack = ResourceManager.Load<PackedScene>("res://prefab/weapon/bullet/HighSpeedBullet.tscn");
        attr.ShellPack = ResourceManager.Load<PackedScene>("res://prefab/weapon/shell/ShellCase.tscn");
        gun.Init(attr);
        return gun;
    }

    public static Gun GetGun3()
    {
        //加载枪的 prefab
        var gun = ResourceManager.LoadGunAndInstance("res://prefab/weapon/Gun.tscn");
        //设置基础属性
        var attr = new GunAttribute();
        attr.Id = "3";
        attr.Name = "Gun3";
        attr.Weight = 30;
        attr.CenterPosition = new Vector2(0.4f, -2.6f);
        attr.StartFiringSpeed = 480;
        attr.StartScatteringRange = 30;
        attr.FinalScatteringRange = 90;
        attr.ScatteringRangeAddValue = 2f;
        attr.ScatteringRangeBackSpeed = 40;
        //连发
        attr.ContinuousShoot = false;
        //扳机检测间隔
        attr.TriggerInterval = 0f;
        //连发数量
        attr.MinContinuousCount = 3;
        attr.MaxContinuousCount = 3;
        //开火前延时
        attr.DelayedTime = 0f;
        //攻击距离
        attr.MinDistance = 500;
        attr.MaxDistance = 600;
        //发射子弹数量
        attr.MinFireBulletCount = 1;
        attr.MaxFireBulletCount = 1;
        //抬起角度
        attr.UpliftAngle = 10;
        //枪身长度
        attr.FirePosition = new Vector2(16, 1.5f);
        attr.Sprite = ResourceManager.Load<Texture>("res://resource/sprite/gun/gun2.png");
        attr.BulletPack = ResourceManager.Load<PackedScene>("res://prefab/weapon/bullet/OrdinaryBullets.tscn");
        attr.ShellPack = ResourceManager.Load<PackedScene>("res://prefab/weapon/shell/ShellCase.tscn");
        gun.Init(attr);
        return gun;
    }

    public static Gun GetGun4()
    {
        //加载枪的 prefab
        var gun = ResourceManager.LoadGunAndInstance("res://prefab/weapon/Gun.tscn");
        //设置基础属性
        var attr = new GunAttribute();
        attr.Id = "4";
        attr.Name = "Gun4";
        attr.Weight = 10;
        attr.CenterPosition = new Vector2(0.4f, -2.6f);
        attr.WeightType = GunWeightType.DeputyWeapon;
        attr.StartFiringSpeed = 600;
        attr.StartScatteringRange = 5;
        attr.FinalScatteringRange = 60;
        attr.ScatteringRangeAddValue = 8f;
        attr.ScatteringRangeBackSpeed = 40;
        //连发
        attr.ContinuousShoot = false;
        //扳机检测间隔
        attr.TriggerInterval = 0.4f;
        //连发数量
        attr.MinContinuousCount = 3;
        attr.MaxContinuousCount = 3;
        //开火前延时
        attr.DelayedTime = 0f;
        //攻击距离
        attr.MinDistance = 500;
        attr.MaxDistance = 600;
        //发射子弹数量
        attr.MinFireBulletCount = 1;
        attr.MaxFireBulletCount = 1;
        //抬起角度
        attr.UpliftAngle = 30;
        //枪身长度
        attr.FirePosition = new Vector2(10, 1.5f);
        attr.Sprite = ResourceManager.Load<Texture>("res://resource/sprite/gun/gun7.png");
        attr.BulletPack = ResourceManager.Load<PackedScene>("res://prefab/weapon/bullet/HighSpeedBullet.tscn");
        attr.ShellPack = ResourceManager.Load<PackedScene>("res://prefab/weapon/shell/ShellCase.tscn");
        gun.Init(attr);
        return gun;
    }
}
