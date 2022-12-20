
using System.Collections.Generic;
using System.Linq;
using Godot;

/// <summary>
/// 移动控制器, 物体运动由力来控制, 不同方向的力速度最终会汇总
/// </summary>
public class MoveController : Component
{
    private static long _index = 0;

    /// <summary>
    /// 玩家受到的外力的集合
    /// </summary>
    private readonly List<ExternalForce> _forceList = new List<ExternalForce>();

    /// <summary>
    /// 这个速度就是玩家当前物理帧移动的真实速率, 该速度由物理帧循环更新, 并不会马上更新
    /// 该速度就是 BasisVelocity + 外力总和
    /// </summary>
    public Vector2 Velocity => _velocity;

    private Vector2 _velocity = Vector2.Zero;


    /// <summary>
    /// 玩家的基础移动速率
    /// </summary>
    public Vector2 BasisVelocity
    {
        get => _basisVelocity;
        set => _basisVelocity = value;
    }

    private Vector2 _basisVelocity = Vector2.Zero;

    /// <summary>
    /// 获取所有外力对象
    /// </summary>
    public ExternalForce[] GetAllForce()
    {
        return _forceList.ToArray();
    }

    /// <summary>
    /// 快速窗口一个外力, 该外力为匿名外力, 当速率变为 0 时自动销毁
    /// </summary>
    /// <param name="velocity">外力速率</param>
    /// <param name="resistance">阻力大小</param>
    public ExternalForce AddForce(Vector2 velocity, float resistance)
    {
        var force = AddForce("_anonymity_" + _index++);
        force.Velocity = velocity;
        force.Resistance = resistance;
        return force;
    }

    /// <summary>
    /// 根据名称添加一个外力, 并返回创建的外力的对象, 如果存在这个名称的外力, 移除之前的外力
    /// </summary>
    public ExternalForce AddForce(string name)
    {
        var f = new ExternalForce(name);
        AddForce(f);
        return f;
    }

    /// <summary>
    /// 根据对象添加一个外力力, 如果存在这个名称的外力, 移除之前的外力
    /// </summary>
    public T AddForce<T>(T force) where T : ExternalForce
    {
        RemoveForce(force.Name);
        _forceList.Add(force);
        return force;
    }

    /// <summary>
    /// 根据名称移除一个外力
    /// </summary>
    public void RemoveForce(string name)
    {
        for (var i = 0; i < _forceList.Count; i++)
        {
            if (_forceList[i].Name == name)
            {
                _forceList.RemoveAt(i);
                return;
            }
        }
    }

    /// <summary>
    /// 根据名称获取一个外力
    /// </summary>
    public ExternalForce GetForce(string name)
    {
        for (var i = 0; i < _forceList.Count; i++)
        {
            var externalForce = _forceList[i];
            if (externalForce.Name == name)
            {
                return externalForce;
            }
        }

        return null;
    }

    /// <summary>
    /// 检车是否有当前名称的外力对象
    /// </summary>
    public bool ContainsForce(string name)
    {
        for (var i = 0; i < _forceList.Count; i++)
        {
            var externalForce = _forceList[i];
            if (externalForce.Name == name)
            {
                return true;
            }
        }

        return false;
    }

    /// <summary>
    /// 根据对象移除一个外力
    /// </summary>
    public void RemoveForce(ExternalForce force)
    {
        RemoveForce(force.Name);
    }

    /// <summary>
    /// 移除所有外力
    /// </summary>
    public void ClearForce()
    {
        _forceList.Clear();
    }

    public override void PhysicsProcess(float delta)
    {
        if (_basisVelocity == Vector2.Zero && _forceList.Count == 0)
        {
            return;
        }

        //先调用更新
        var externalForces = _forceList.ToArray();
        foreach (var fore in externalForces)
        {
            if (fore.Enable)
                fore.PhysicsProcess(delta);
        }

        //外力总和
        var finallyEf = new Vector2();
        foreach (var fore in externalForces)
        {
            if (fore.Enable)
                finallyEf += fore.Velocity;
        }

        //最终速率
        var finallyVelocity = _basisVelocity + finallyEf;

        if (finallyVelocity != Vector2.Zero)
        {
            //计算移动
            _velocity = ActivityObject.MoveAndSlide(finallyVelocity);

            //调整外力速率
            if (externalForces.Length > 0)
            {
                //x轴外力
                var scaleX = finallyEf.x == 0f ? 0 : Mathf.Abs((_velocity.x - _basisVelocity.x) / finallyEf.x);
                //y轴外力
                var scaleY = finallyEf.y == 0f ? 0 : Mathf.Abs((_velocity.y - _basisVelocity.y) / finallyEf.y);
                for (var i = 0; i < _forceList.Count; i++)
                {
                    var force = _forceList[i];
                    if (force.Enable)
                    {
                        var velocity = force.Velocity;
                        force.Velocity = new Vector2(velocity.x * scaleX, velocity.y * scaleY);
                        if (force.Resistance != 0)
                        {
                            force.Velocity = force.Velocity.MoveToward(Vector2.Zero, force.Resistance * delta);
                        }

                        if (force.AutoDestroy && force.Velocity == Vector2.Zero)
                        {
                            _forceList.RemoveAt(i--);
                            GD.Print("移除外力: " + force.Name);
                        }
                    }
                }
            }
        }
        else
        {
            _velocity = finallyEf;
        }
    }

    public override void DebugDraw()
    {
        var globalRotation = GlobalRotation;
        ActivityObject.DrawLine(Vector2.Zero, BasisVelocity.Rotated(-globalRotation), Colors.Yellow);
        foreach (var force in _forceList)
        {
            ActivityObject.DrawLine(Vector2.Zero, force.Velocity.Rotated(-globalRotation), Colors.YellowGreen);
        }
    }
}