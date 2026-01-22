using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonManager : MonoBehaviour
{
    private static DungeonManager m_Instance;
    public static DungeonManager Instance => m_Instance;

    [SerializeField]
    private Player m_Player;

    private void Awake()
    {
        if (m_Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        m_Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void OnEnable()
    {
        EventManager.OnEnemyArrived += HandleEnemyArrived;
        EventManager.OnPlayerDead += HandlePlayerDead;
    }

    private void OnDisable()
    {
        EventManager.OnEnemyArrived -= HandleEnemyArrived;
        EventManager.OnPlayerDead -= HandlePlayerDead;
    }

    private void HandleEnemyArrived(Enemy enemy)
    {
        m_Player.TakeDamage(1);
    }

    private void HandlePlayerDead()
    {
        Debug.Log("Game Over");
        // 이후:
        // - 웨이브 정지
        // - UI 표시
        // - 재시작 처리
    }
}

