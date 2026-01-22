using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    private Enemy m_EnemyPrefab;

    [SerializeField]
    private WaypointPath m_Path;

    [SerializeField]
    private Transform m_Enemys;

    [SerializeField]
    private float m_SpawnInterval = 2f;

    private float m_Timer;

    private void Update()
    {
        m_Timer += Time.deltaTime;

        if (m_Timer >= m_SpawnInterval)
        {
            SpawnEnemy();
            m_Timer = 0f;
        }
    }

    private void SpawnEnemy()
    {
        Enemy enemy = ObjectPool.Instance.Get(m_EnemyPrefab);

        enemy.transform.SetParent(m_Enemys);
        enemy.Initialize(m_Path);
    }
}
