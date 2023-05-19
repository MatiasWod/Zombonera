using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CmdHeal : ICommand
{
    private IDamagable _damageable;
    private int _heal;

    public CmdHeal(IDamagable damagable, int healAmount)
    {
        _damageable = damagable;
        _heal = healAmount;
    }

    public void Execute() => _damageable.Heal(_heal);
}
