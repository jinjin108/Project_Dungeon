using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private EnemyMove m_Move;

    private void Awake()
    {
        m_Move = GetComponent<EnemyMove>();
    }

    public void Initialize(WaypointPath path)
    {
        m_Move.Initialize(path);
    }
}
