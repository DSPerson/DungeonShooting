
using Godot;

/// <summary>
/// 针对敌人生成位置的标记
/// </summary>
[Tool, GlobalClass]
public partial class EnemyMark : ActivityMark
{
    /// <summary>
    /// 脸默认朝向
    /// </summary>
    public enum FaceDirectionValueEnum
    {
        /// <summary>
        /// 随机
        /// </summary>
        Random,
        /// <summary>
        /// 左边
        /// </summary>
        Left,
        /// <summary>
        /// 右边
        /// </summary>
        Right
    }
    
    /// <summary>
    /// 武器1 id, id会自动加上武器前缀
    /// </summary>
    [Export(PropertyHint.Expression), ActivityExpression]
    public string Weapon1Id;
    /// <summary>
    /// 武器2 id, id会自动加上武器前缀
    /// </summary>
    [Export(PropertyHint.Expression), ActivityExpression]
    public string Weapon2Id;
    /// <summary>
    /// 脸默认的朝向
    /// </summary>
    [Export]
    public FaceDirectionValueEnum FaceDirection = FaceDirectionValueEnum.Random;

    public override void _Ready()
    {
        Type = ActivityType.Enemy;
        Layer = RoomLayerEnum.YSortLayer;
        Altitude = 0;
    }

    public override void Doing(ActivityObjectResult<ActivityObject> result, RoomInfo roomInfo)
    {
        //创建敌人
        var instance = (Enemy)result.ActivityObject;
        var pos = instance.Position;
        
        //脸的朝向
        if (FaceDirection == FaceDirectionValueEnum.Random)
        {
            instance.Face = Random.RandomBoolean() ? global::FaceDirection.Left : global::FaceDirection.Right;
        }
        else if (FaceDirection == FaceDirectionValueEnum.Left)
        {
            instance.Face = global::FaceDirection.Left;
        }
        else
        {
            instance.Face = global::FaceDirection.Right;
        }

        if (!string.IsNullOrWhiteSpace(Weapon1Id))
            CreateWeapon(instance, pos, nameof(Weapon1Id));
        if (!string.IsNullOrWhiteSpace(Weapon2Id))
            CreateWeapon(instance, pos, nameof(Weapon2Id));
    }

    //生成武器
    private void CreateWeapon(Enemy enemy, Vector2 pos, string fieldName)
    {
        var result = CreateActivityObjectFromExpression<Weapon>(ActivityType.Weapon, fieldName);
        if (result != null)
        {
            //如果不能放下， 则直接扔地上
            if (!enemy.PickUpWeapon(result.ActivityObject))
            {
                result.ActivityObject.PutDown(pos, RoomLayerEnum.NormalLayer);
            }
        }
    }
}