using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private int m_MaxHp = 10;

    private int m_CurrentHp;

    public int CurrentHp => m_CurrentHp;
    public int MaxHp => m_MaxHp;

    private void Awake()
    {
        m_CurrentHp = m_MaxHp;
    }

    public void TakeDamage(int amount)
    {
        if (m_CurrentHp <= 0)
            return;

        m_CurrentHp -= amount;
        m_CurrentHp = Mathf.Max(m_CurrentHp, 0);

        Debug.Log($"Player HP : {m_CurrentHp}");

        EventManager.RaisePlayerHpChanged(m_CurrentHp);

        if (m_CurrentHp <= 0)
        {
            EventManager.RaisePlayerDead();
        }
    }
}
