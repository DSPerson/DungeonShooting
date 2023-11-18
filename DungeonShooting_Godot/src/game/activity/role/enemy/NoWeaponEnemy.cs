﻿
using Godot;

/// <summary>
/// 没有武器的敌人
/// </summary>
[Tool]
public partial class NoWeaponEnemy : Enemy
{
    public override void Attack()
    {
        Debug.Log("attack...");
    }
}