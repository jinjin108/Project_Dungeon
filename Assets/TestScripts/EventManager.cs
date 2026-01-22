using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager
{
    // Enemy
    public static Action<Enemy> OnEnemyArrived;

    // Player
    public static Action<int> OnPlayerHpChanged;
    public static Action OnPlayerDead;

    public static void RaiseEnemyArrived(Enemy enemy)
    {
        OnEnemyArrived?.Invoke(enemy);
    }

    public static void RaisePlayerHpChanged(int currentHp)
    {
        OnPlayerHpChanged?.Invoke(currentHp);
    }

    public static void RaisePlayerDead()
    {
        OnPlayerDead?.Invoke();
    }
}
