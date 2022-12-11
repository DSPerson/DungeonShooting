﻿
using Godot;

/// <summary>
/// 距离目标足够近, 在目标附近随机移动, 并开火
/// </summary>
public class AiSurroundState : StateBase<Enemy, AiStateEnum>
{
    /// <summary>
    /// 目标是否在视野内
    /// </summary>
    public bool IsInView = true;

    //是否移动结束
    private bool _isMoveOver;

    //移动停顿计时器
    private float _pauseTimer;

    //下一个移动点
    private Vector2 _nextPosition;

    public AiSurroundState() : base(AiStateEnum.AiSurround)
    {
    }

    public override void Enter(AiStateEnum prev, params object[] args)
    {
        IsInView = true;
        _isMoveOver = true;
        _pauseTimer = 0;
    }

    public override void PhysicsProcess(float delta)
    {
        //先检查弹药是否打光
        if (Master.IsAllWeaponTotalAmmoEmpty())
        {
            //再寻找是否有可用的武器
            if (Master.CheckUsableWeaponInUnclaimed())
            {
                //切换到寻找武器状态
                ChangeStateLate(AiStateEnum.AiFindAmmo);
                return;
            }
        }
        
        var playerPos = Player.Current.GetCenterPosition();
        var weapon = Master.Holster.ActiveWeapon;

        //枪口指向玩家
        Master.LookTargetPosition(playerPos);

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
            if (_pauseTimer >= 0)
            {
                Master.AnimatedSprite.Animation = AnimatorNames.Idle;
                _pauseTimer -= delta;
            }
            else if (_isMoveOver) //移动已经完成
            {
                RunOver(playerPos);
                _isMoveOver = false;
            }
            else
            {
                //计算移动
                var nextPos = Master.NavigationAgent2D.GetNextLocation();
                Master.AnimatedSprite.Animation = AnimatorNames.Run;
                Master.Velocity = (nextPos - Master.GlobalPosition - Master.NavigationPoint.Position).Normalized() *
                                  Master.MoveSpeed;
                Master.CalcMove(delta);

                if (Master.NavigationAgent2D.IsNavigationFinished()) //到达终点
                {
                    _pauseTimer = Utils.RandRange(0f, 0.5f);
                    _isMoveOver = true;
                }
                else
                {
                    var lastSlideCollision = Master.GetLastSlideCollision();
                    if (lastSlideCollision != null && lastSlideCollision.Collider is Role) //碰到其他角色
                    {
                        _pauseTimer = Utils.RandRange(0f, 0.3f);
                        _isMoveOver = true;
                    }
                }

                if (weapon != null)
                {
                    var position = Master.GlobalPosition;
                    if (position.DistanceSquaredTo(playerPos) > Mathf.Pow(Master.GetWeaponRange(0.7f), 2)) //玩家离开正常射击范围
                    {
                        ChangeStateLate(AiStateEnum.AiFollowUp);
                    }
                    else
                    {
                        //发起攻击
                        Master.EnemyAttack();
                    }
                }
            }
        }
        else //目标离开视野
        {
            ChangeStateLate(AiStateEnum.AiTailAfter);
        }
    }

    private void RunOver(Vector2 targetPos)
    {
        var distance = (int)(Master.Holster.ActiveWeapon.Attribute.MinDistance * 0.7f);
        _nextPosition = new Vector2(
            targetPos.x + Utils.RandRangeInt(-distance, distance),
            targetPos.y + Utils.RandRangeInt(-distance, distance)
        );
        Master.NavigationAgent2D.SetTargetLocation(_nextPosition);
    }

    public override void DebugDraw()
    {
        Master.DrawLine(new Vector2(0, -8), Master.ToLocal(_nextPosition), Colors.White);
    }
}