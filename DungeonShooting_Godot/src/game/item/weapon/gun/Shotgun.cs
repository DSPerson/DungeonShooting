using Godot;

[RegisterWeapon("1002", typeof(Shotgun.ShotgunAttribute))]
public partial class Shotgun : Weapon
{

    private class ShotgunAttribute : WeaponAttribute
    {
        public ShotgunAttribute()
        {
            Name = "霰弹枪";
            Sprite2D = ResourcePath.resource_sprite_gun_gun2_png;
            Weight = 40;
            CenterPosition = new Vector2(0.4f, -2.6f);
            StartFiringSpeed = 120;
            StartScatteringRange = 30;
            FinalScatteringRange = 90;
            ScatteringRangeAddValue = 50f;
            ScatteringRangeBackSpeed = 50;
            //连发
            ContinuousShoot = false;
            AmmoCapacity = 7;
            StandbyAmmoCapacity = 42;
            MaxAmmoCapacity = 42;
            AloneReload = true;
            AloneReloadCanShoot = true;
            ReloadTime = 0.3f;
            //连发数量
            MinContinuousCount = 1;
            MaxContinuousCount = 1;
            //开火前延时
            DelayedTime = 0f;
            //攻击距离
            MinDistance = 200;
            MaxDistance = 250;
            //发射子弹数量
            MinFireBulletCount = 5;
            MaxFireBulletCount = 5;
            //抬起角度
            UpliftAngle = 15;
            MaxBacklash = 6;
            MinBacklash = 5;
            //开火位置
            FirePosition = new Vector2(18, 4);

            AiUseAttribute = Clone();
            AiUseAttribute.TriggerInterval = 3.5f;
        }
    }
    
    /// <summary>
    /// 弹壳预制体
    /// </summary>
    public PackedScene ShellPack;

    public Shotgun(string typeId, WeaponAttribute attribute) : base(typeId, attribute)
    {
        ShellPack = ResourceManager.Load<PackedScene>(ResourcePath.prefab_weapon_shell_ShellCase_tscn);
    }

    protected override void OnFire()
    {
        //创建一个弹壳
        var startPos = GlobalPosition + new Vector2(0, 5);
        var startHeight = 6;
        var direction = GlobalRotationDegrees + Utils.RandRangeInt(-30, 30) + 180;
        var xf = Utils.RandRangeInt(20, 60);
        var yf = Utils.RandRangeInt(60, 120);
        var rotate = Utils.RandRangeInt(-720, 720);
        var shell = new ShellCase();
        shell.Throw(new Vector2(5, 10), startPos, startHeight, direction, xf, yf, rotate, true);
        
        if (Master == GameApplication.Instance.RoomManager.Player)
        {
            //创建抖动
            GameCamera.Main.ProcessDirectionalShake(Vector2.Right.Rotated(GlobalRotation) * 2f);
        }
        
        //创建开火特效
        var packedScene = ResourceManager.Load<PackedScene>(ResourcePath.prefab_effect_ShotFire_tscn);
        var sprite = packedScene.Instantiate<Sprite2D>();
        sprite.GlobalPosition = FirePoint.GlobalPosition;
        sprite.GlobalRotation = FirePoint.GlobalRotation;
        sprite.AddToActivityRoot(RoomLayerEnum.YSortLayer);
        
        //播放射击音效
        SoundManager.PlaySoundEffectPosition(ResourcePath.resource_sound_sfx_ordinaryBullet3_mp3, GameApplication.Instance.ViewToGlobalPosition(GlobalPosition), -15);
    }

    protected override void OnShoot(float fireRotation)
    {
        //创建子弹
        var bullet = new Bullet(
            ResourcePath.prefab_weapon_bullet_Bullet_tscn,
            Utils.RandRangeInt(280, 380),
            Utils.RandfRange(Attribute.MinDistance, Attribute.MaxDistance),
            FirePoint.GlobalPosition,
            fireRotation + Utils.RandfRange(-20 / 180f * Mathf.Pi, 20 / 180f * Mathf.Pi),
            GetAttackLayer()
        );
        bullet.PutDown(RoomLayerEnum.YSortLayer);
    }
}
