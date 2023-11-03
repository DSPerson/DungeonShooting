
/// <summary>
/// 物体材质
/// </summary>
public class ActivityMaterial
{
    /// <summary>
    /// 摩擦力
    /// </summary>
    public float Friction { get; set; } = 140;
    
    /// <summary>
    /// 旋转摩擦力
    /// </summary>
    public float RotationFriction { get; set; } = 140;
    
    /// <summary>
    /// 落地之后是否回弹
    /// </summary>
    public bool Bounce { get; set; } = true;

    /// <summary>
    /// 物体回弹的强度
    /// </summary>
    public float BounceStrength { get; set; } = 0.5f;

    /// <summary>
    /// 物体下坠回弹后的运动速度衰减量
    /// </summary>
    public float FallBounceSpeed { get; set; } = 0.75f;
    
    /// <summary>
    /// 物体下坠回弹后的旋转速度衰减量
    /// </summary>
    public float FallBounceRotation { get; set; } = 0.5f;
}