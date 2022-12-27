using Godot;

/// <summary>
/// 常用函数工具类
/// </summary>
public static class Utils
{
    
    /// <summary>
    /// 返回随机 boolean 值
    /// </summary>
    public static bool RandBoolean()
    {
        return GD.Randf() >= 0.5f;
    }
    
    /// <summary>
    /// 返回一个区间内的随机小数
    /// </summary>
    public static float RandRange(float min, float max)
    {
        if (min == max) return min; 
        if (min > max)
            return GD.Randf() * (min - max) + max;
        return GD.Randf() * (max - min) + min;
    }

    /// <summary>
    /// 返回一个区间内的随机整数
    /// </summary>
    public static int RandRangeInt(int min, int max)
    {
        if (min == max) return min; 
        if (min > max)
            return Mathf.FloorToInt(GD.Randf() * (min - max + 1) + max);
        return Mathf.FloorToInt(GD.Randf() * (max - min + 1) + min);
    }
}