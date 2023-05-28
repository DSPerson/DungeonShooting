using Godot;

/// <summary>
/// 普通的枪
/// </summary>
[RegisterWeapon(ActivityIdPrefix.Weapon + "0001", typeof(RifleAttribute))]
[RegisterWeapon(ActivityIdPrefix.Weapon + "0003", typeof(PistolAttribute))]
public partial class Gun : Weapon
{
    //步枪属性数据
    private class RifleAttribute : WeaponAttribute
    {
        public RifleAttribute()
        {
            Name = "步枪";
            Icon = ResourcePath.resource_sprite_gun_gun4_png;
            SpriteFrames = ResourcePath.resource_spriteFrames_Weapon0001_tres;
            Weight = 40;
            ThrowSpritePosition = new Vector2(0.4f, -2.6f);
            StartFiringSpeed = 480;
            StartScatteringRange = 30;
            FinalScatteringRange = 90;
            ScatteringRangeAddValue = 2f;
            ScatteringRangeBackSpeed = 40;
            //连发
            ContinuousShoot = true;
            AmmoCapacity = 30;
            StandbyAmmoCapacity = 30 * 3;
            MaxAmmoCapacity = 30 * 3;
            //扳机检测间隔
            TriggerInterval = 0f;

            //开火前延时
            DelayedTime = 0f;
            //攻击距离
            MinDistance = 300;
            MaxDistance = 400;
            //发射子弹数量
            MinFireBulletCount = 1;
            MaxFireBulletCount = 1;
            //抬起角度
            UpliftAngle = 10;
            //开火位置
            FirePosition = new Vector2(19, -1.5f);
            //握把位置
            SpritePosition = new Vector2(4, 0);
            //ShellPosition = new Vector2(4, -2.5f);
            
            AiUseAttribute = Clone();
            AiUseAttribute.AiTargetLockingTime = 0.5f;
            AiUseAttribute.TriggerInterval = 3f;
            AiUseAttribute.ContinuousShoot = false;
            AiUseAttribute.MinContinuousCount = 3;
            AiUseAttribute.MaxContinuousCount = 3;
        }
    }

    //手枪属性数据
    private class PistolAttribute : WeaponAttribute
    {
        public PistolAttribute()
        {
            Name = "手枪";
            Icon = ResourcePath.resource_sprite_gun_gun3_png;
            SpriteFrames = ResourcePath.resource_spriteFrames_Weapon0003_tres;
            Weight = 20;
            ThrowSpritePosition = new Vector2(0.4f, -2.6f);
            WeightType = WeaponWeightType.DeputyWeapon;
            StartFiringSpeed = 300;
            FinalFiringSpeed = 300;
            StartScatteringRange = 5;
            FinalScatteringRange = 60;
            ScatteringRangeAddValue = 8f;
            ScatteringRangeBackSpeed = 40;
            //连发
            ContinuousShoot = false;
            AmmoCapacity = 12;
            StandbyAmmoCapacity = 72;
            MaxAmmoCapacity = 72;
            //扳机检测间隔
            TriggerInterval = 0.1f;
            //连发数量
            MinContinuousCount = 1;
            MaxContinuousCount = 1;
            //开火前延时
            DelayedTime = 0f;
            //攻击距离
            MinDistance = 250;
            MaxDistance = 300;
            //发射子弹数量
            MinFireBulletCount = 1;
            MaxFireBulletCount = 1;
            //抬起角度
            UpliftAngle = 30;
            //开火位置
            FirePosition = new Vector2(12.5F, 2.5F);

            AiUseAttribute = Clone();
            AiUseAttribute.AiTargetLockingTime = 1f;
            AiUseAttribute.TriggerInterval = 2f;
        }
    }
    
    protected override void OnFire()
    {
        //创建一个弹壳
        ThrowShell("0001");
        
        if (Master == Player.Current)
        {
            //创建抖动
            GameCamera.Main.DirectionalShake(Vector2.Right.Rotated(GlobalRotation) * 2f);
        }

        //创建开火特效
        var packedScene = ResourceManager.Load<PackedScene>(ResourcePath.prefab_effect_ShotFire_tscn);
        var sprite = packedScene.Instantiate<Sprite2D>();
        sprite.GlobalPosition = FirePoint.GlobalPosition;
        sprite.GlobalRotation = FirePoint.GlobalRotation;
        sprite.AddToActivityRoot(RoomLayerEnum.YSortLayer);

        //播放射击音效
        SoundManager.PlaySoundEffectPosition(ResourcePath.resource_sound_sfx_ordinaryBullet2_mp3, GameApplication.Instance.ViewToGlobalPosition(GlobalPosition), -8);
    }

    protected override void OnShoot(float fireRotation)
    {
        //创建子弹
        var bullet = ActivityObject.Create<Bullet>(Attribute.BulletId);
        bullet.Init(
            this,
            350,
            Utils.RandomRangeFloat(Attribute.MinDistance, Attribute.MaxDistance),
            FirePoint.GlobalPosition,
            fireRotation,
            GetAttackLayer()
        );
        bullet.PutDown(RoomLayerEnum.YSortLayer);
    }
}