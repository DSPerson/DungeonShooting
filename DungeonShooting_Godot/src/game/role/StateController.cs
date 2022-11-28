using System;
using Godot;
using System.Collections.Generic;

/// <summary>
/// 对象状态机控制器
/// </summary>
public class StateController<T, S> : Component where T : ActivityObject where S : Enum
{
    /// <summary>
    /// 当前活跃的状态
    /// </summary>
    public StateBase<T, S> CurrStateBase => _currStateBase;
    private StateBase<T, S> _currStateBase;
    
    /// <summary>
    /// 负责存放状态实例对象
    /// </summary>
    private readonly Dictionary<S, StateBase<T, S>> _states = new Dictionary<S, StateBase<T, S>>();

    /// <summary>
    /// 记录下当前帧是否有改变的状态
    /// </summary>
    private bool _isChangeState;
    
    public override void PhysicsProcess(float delta)
    {
        _isChangeState = false;
        if (CurrStateBase != null)
        {
            CurrStateBase.PhysicsProcess(delta);
            //判断当前帧是否有改变的状态, 如果有, 则重新调用 PhysicsProcess() 方法
            if (_isChangeState)
            {
                PhysicsProcess(delta);
            }
        }
    }

    /// <summary>
    /// 往状态机力注册一个新的状态
    /// </summary>
    public void Register(StateBase<T, S> stateBase)
    {
        if (GetStateInstance(stateBase.StateType) != null)
        {
            GD.PrintErr("当前状态已经在状态机中注册:", stateBase);
            return;
        }
        stateBase.Master = ActivityObject as T;
        stateBase.StateController = this;
        _states.Add(stateBase.StateType, stateBase);
    }

    /// <summary>
    /// 立即切换到下一个指定状态, 并且这一帧会被调用 PhysicsProcess
    /// </summary>
    public void ChangeState(S next, params object[] arg)
    {
        _changeState(false, next, arg);
    }

    /// <summary>
    /// 切换到下一个指定状态, 下一帧才会调用 PhysicsProcess
    /// </summary>
    public void ChangeStateLate(S next, params object[] arg)
    {
        _changeState(true, next, arg);
    }

    /// <summary>
    /// 根据状态类型获取相应的状态对象
    /// </summary>
    private StateBase<T, S> GetStateInstance(S stateType)
    {
        _states.TryGetValue(stateType, out var v);
        return v;
    }

    /// <summary>
    /// 切换状态
    /// </summary>
    private void _changeState(bool late, S next, params object[] arg)
    {
        if (_currStateBase != null && _currStateBase.StateType.Equals(next))
        {
            return;
        }
        var newState = GetStateInstance(next);
        if (newState == null)
        {
            GD.PrintErr("当前状态机未找到相应状态:" + next);
            return;
        }
        if (_currStateBase == null)
        {
            _currStateBase = newState;
            newState.Enter(default, arg);
        }
        else if (_currStateBase.CanChangeState(next))
        {
            _isChangeState = !late;
            var prev = _currStateBase.StateType;
            _currStateBase.Exit(next);
            GD.Print("nextState => " + next);
            _currStateBase = newState;
            _currStateBase.Enter(prev, arg);
        }
    }
}