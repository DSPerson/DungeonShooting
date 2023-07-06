﻿
using Godot;

/// <summary>
/// 提升伤害buff, 子弹伤害提升20%
/// </summary>
[Tool]
public partial class BuffPropProp0005 : BuffProp
{
    protected override void OnPickUp(Role master)
    {
        master.RoleState.CalcDamageEvent += CalcDamage;
    }

    protected override void OnRemove(Role master)
    {
        master.RoleState.CalcDamageEvent -= CalcDamage;
    }

    private void CalcDamage(int originDamage, RefValue<int> refValue)
    {
        refValue.Value += Mathf.CeilToInt(originDamage * 0.2f);
    }
}