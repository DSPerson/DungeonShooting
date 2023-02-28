using System.Collections.Generic;
using Godot;

/// <summary>
/// 游戏相机
/// </summary>
public partial class GameCamera : Camera2D
{
    /// <summary>
    /// 当前场景的相机对象
    /// </summary>
    public static GameCamera Main { get; private set; }

    /// <summary>
    /// 恢复系数
    /// </summary>
    [Export]
    public float RecoveryCoefficient = 100f;
    /// <summary>
    /// 抖动开关
    /// </summary>
    public bool Enable { get; set; } = true;
    
    public Vector2 SubPixelPosition { get; private set; }

    private long _index = 0;
    private Vector2 _processDistance = Vector2.Zero;
    private Vector2 _processDirection = Vector2.Zero;
    private readonly Dictionary<long, Vector2> _shakeMap = new Dictionary<long, Vector2>();
    
    private Vector2 _camPos;
    private Vector2 _shakeOffset = Vector2.Zero;

    public override void _Ready()
    {
        Main = this;
        _camPos = GlobalPosition;
    }

    public override void _Process(double delta)
    {
        var newDelta = (float)delta;
        _Shake(newDelta);
        
        // 3.5 写法
        // var player = GameApplication.Instance.RoomManager.Player;
        // var viewportContainer = GameApplication.Instance.SubViewportContainer;
        // var camPos = player.GlobalPosition;
        // _camPos = _camPos.Lerp(camPos, Mathf.Min(6 * newDelta, 1)) + _shakeOffset;
        // SubPixelPosition = _camPos.Round() - _camPos;
        // (viewportContainer.Material as ShaderMaterial)?.SetShaderParameter("offset", SubPixelPosition);
        // GlobalPosition = _camPos.Round();
        
        var player = GameApplication.Instance.RoomManager.Player;
        var mousePosition = InputManager.GetViewportMousePosition();
        var playerPosition = player.GlobalPosition;
        Vector2 targetPos;
        if (playerPosition.DistanceSquaredTo(mousePosition) >= (60 / 0.3f) * (60 / 0.3f))
        {
            targetPos = playerPosition.MoveToward(mousePosition, 60);
        }
        else
        {
            targetPos = playerPosition.Lerp(mousePosition, 0.3f);
        }
        _camPos = _camPos.Lerp(targetPos, Mathf.Min(6 * newDelta, 1)) + _shakeOffset;
        SubPixelPosition = _camPos.Round() - _camPos;
        GlobalPosition = _camPos.Round();
    }
    
    /// <summary>
    /// 设置帧抖动, 结束后自动清零, 需要每一帧调用
    /// </summary>
    /// <param name="value">抖动的力度</param>
    public void ProcessShake(Vector2 value)
    {
        if (value.Length() > _processDistance.Length())
        {
            _processDistance = value;
        }
    }

    public void ProcessDirectionalShake(Vector2 value)
    {
        _processDirection += value;
    }

    /// <summary>
    /// 创建一个抖动, 并设置抖动时间
    /// </summary>
    /// <param name="value">抖动力度</param>
    /// <param name="time">抖动生效时间</param>
    public async void CreateShake(Vector2 value, float time)
    {
        if (time > 0)
        {
            long tempIndex = _index++;
            SceneTreeTimer sceneTreeTimer = GetTree().CreateTimer(time);
            _shakeMap[tempIndex] = value;
            await ToSignal(sceneTreeTimer, "timeout");
            _shakeMap.Remove(tempIndex);
        }
    }

    //抖动调用
    private void _Shake(float delta)
    {
        if (Enable)
        {
            var distance = _CalculateDistance();
            _shakeOffset += _processDirection + new Vector2(
                Utils.RandomRangeFloat(-distance.X, distance.X) - _shakeOffset.X,
                Utils.RandomRangeFloat(-distance.Y, distance.Y) - _shakeOffset.Y
            );
            _processDistance = Vector2.Zero;
            _processDirection = Vector2.Zero;
        }
        else
        {
            _shakeOffset = _shakeOffset.Lerp(Vector2.Zero, RecoveryCoefficient * delta);
        }
    }

    //计算相机需要抖动的值
    private Vector2 _CalculateDistance()
    {
        Vector2 temp = Vector2.Zero;
        float length = 0;
        foreach (var item in _shakeMap)
        {
            var value = item.Value;
            float tempLenght = value.Length();
            if (tempLenght > length)
            {
                length = tempLenght;
                temp = value;
            }
        }
        return _processDistance.Length() > length ? _processDistance : temp;
    }

}
