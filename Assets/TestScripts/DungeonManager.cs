using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonManager : MonoBehaviour
{
    private static DungeonManager instance;
    public static DungeonManager Instance => instance;

    [SerializeField]
    private WaypointPath m_WaypointPath;

    [SerializeField]
    private GameObject m_EnemyPrefab;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
    }

    private void Start()
    {
        SpawnEnemy();
    }

    public void SpawnEnemy()
    {
        GameObject enemyObj = Instantiate(m_EnemyPrefab);
        Enemy enemy = enemyObj.GetComponent<Enemy>();
        enemy.Initialize(m_WaypointPath);
    }

    public void OnEnemyReachedGoal()
    {
        Debug.Log("Enemy reached goal");
    }
}
