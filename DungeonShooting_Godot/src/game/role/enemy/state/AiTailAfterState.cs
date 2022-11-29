﻿
using Godot;

/// <summary>
/// AI 发现玩家
/// </summary>
public class AiTailAfterState : StateBase<Enemy, AIStateEnum>
{
    //导航目标点刷新计时器
    private float _navigationUpdateTimer = 0;
    private float _navigationInterval = 0.3f;

    private bool _isInView = true;
    
    private float _viewTimer = 0;
    
    public AiTailAfterState() : base(AIStateEnum.AITailAfter)
    {
    }

    public override void Enter(AIStateEnum prev, params object[] args)
    {
        _isInView = true;
        _navigationUpdateTimer = 0;
    }

    public override void PhysicsProcess(float delta)
    {
        if (Master.NavigationAgent2D.IsNavigationFinished())
        {
            return;
        }
        var masterPos = Master.GlobalPosition;
        var playerPos = GameApplication.Instance.Room.Player.GlobalPosition;
        
        //更新玩家位置
        if (_navigationUpdateTimer <= 0)
        {
            //每隔一段时间秒更改目标位置
            _navigationUpdateTimer = _navigationInterval;
            if (Master.NavigationAgent2D.GetTargetLocation() != playerPos)
            {
                Master.NavigationAgent2D.SetTargetLocation(playerPos);
            }
        }
        else
        {
            _navigationUpdateTimer -= delta;
        }
        
        //计算移动
        var nextPos = Master.NavigationAgent2D.GetNextLocation();
        Master.LookTargetPosition(playerPos);
        Master.AnimatedSprite.Animation = AnimatorNames.Run;
        Master.Velocity = (nextPos - Master.GlobalPosition - Master.NavigationPoint.Position).Normalized() * Master.MoveSpeed;
        Master.CalcMove(delta);

        //检测玩家是否在视野内, 此时视野可穿墙, 直接检测距离即可
        _isInView = masterPos.DistanceSquaredTo(playerPos) <= Master.TailAfterViewRange * Master.TailAfterViewRange;
        if (_isInView)
        {
            _viewTimer = 0;
        }
        else //超出视野
        {
            if (_viewTimer > 10) //10秒
            {
                ChangeStateLate(AIStateEnum.AIProbe);
            }
            else
            {
                _viewTimer += delta;
            }
        }
    }

    public override void DebugDraw()
    {
        var playerPos = GameApplication.Instance.Room.Player.GlobalPosition;
        if (_isInView)
        {
            Master.DrawLine(Vector2.Zero, Master.ToLocal(playerPos), Colors.Red);
        }
        else
        {
            Master.DrawLine(Vector2.Zero, Master.ToLocal(playerPos), Colors.Blue);
        }
    }
}