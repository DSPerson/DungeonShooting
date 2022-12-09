﻿
using Godot;

/// <summary>
/// 目标在视野范围内, 跟进目标
/// </summary>
public class AiFollowUpState : StateBase<Enemy, AiStateEnum>
{

    /// <summary>
    /// 目标是否在视野内
    /// </summary>
    public bool IsInView;

    //导航目标点刷新计时器
    private float _navigationUpdateTimer = 0;
    private float _navigationInterval = 0.3f;

    //是否存在下一个移动点
    //private bool _hasNextPosition;
    //private Vector2 _nextPosition;

    public AiFollowUpState() : base(AiStateEnum.AiFollowUp)
    {
    }

    public override void Enter(AiStateEnum prev, params object[] args)
    {
        _navigationUpdateTimer = 0;
        IsInView = true;
    }

    public override void PhysicsProcess(float delta)
    {
        var playerPos = Player.Current.GetCenterPosition();

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

        var masterPosition = Master.GlobalPosition;

        //是否在攻击范围内
        var inAttackRange = false;

        var weapon = Master.Holster.ActiveWeapon;
        if (weapon != null)
        {
            inAttackRange = masterPosition.DistanceSquaredTo(playerPos) <= Mathf.Pow(Master.GetWeaponRange(0.7f), 2);
        }

        //枪口指向玩家
        Master.LookTargetPosition(playerPos);
        if (!Master.NavigationAgent2D.IsNavigationFinished())
        {
            //计算移动
            var nextPos = Master.NavigationAgent2D.GetNextLocation();
            Master.AnimatedSprite.Animation = AnimatorNames.Run;
            Master.Velocity = (nextPos - masterPosition - Master.NavigationPoint.Position).Normalized() *
                              Master.MoveSpeed;
            Master.CalcMove(delta);
        }

        //检测玩家是否在视野内
        if (Master.IsInTailAfterViewRange(playerPos))
        {
            IsInView = !Master.TestViewRayCast(playerPos);
            //关闭射线检测
            Master.TestViewRayCastOver();
        }
        else
        {
            IsInView = false;
        }

        if (IsInView)
        {
            if (inAttackRange) //在攻击范围内
            {
                //发起攻击
                Master.EnemyAttack();
                
                //距离够近, 可以切换到环绕模式
                if (Master.GlobalPosition.DistanceSquaredTo(playerPos) <= Mathf.Pow(weapon.Attribute.MinDistance, 2) * 0.7f)
                {
                    ChangeStateLate(AiStateEnum.AiSurround);
                }
            }
        }
        else
        {
            ChangeStateLate(AiStateEnum.AiTailAfter);
        }
    }

    public override void DebugDraw()
    {
        var playerPos = GameApplication.Instance.Room.Player.GetCenterPosition();
        Master.DrawLine(new Vector2(0, -8), Master.ToLocal(playerPos), Colors.Red);
    }
}