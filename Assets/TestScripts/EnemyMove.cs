using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    [SerializeField]
    private WaypointPath m_Path;

    [SerializeField]
    private float m_MoveSpeed = 3f;

    private int m_CurrentIndex = 0;

    private Transform m_CurrentTarget;

    private void Start()
    {
        InitializePath();
    }

    private void Update()
    {
        if (m_CurrentTarget == null)
            return;

        if (IsArrivedAtTarget())
        {
            SetNextTarget();
            return;
        }

        MoveToTarget();
    }

    private void InitializePath()
    {
        if (m_Path == null || m_Path.Count == 0)
            return;

        m_CurrentIndex = 0;
        m_CurrentTarget = m_Path.GetPoint(m_CurrentIndex);
        transform.position = m_CurrentTarget.position;
        SetNextTarget();
    }

    private void MoveToTarget()
    {
        Vector3 direction = m_CurrentTarget.position - transform.position;
        direction.y = 0f;

        transform.position += direction.normalized * m_MoveSpeed * Time.deltaTime;
    }

    private bool IsArrivedAtTarget()
    {
        float sqrDistance =
            (m_CurrentTarget.position - transform.position).sqrMagnitude;

        return sqrDistance < 0.01f;
    }

    private void SetNextTarget()
    {
        m_CurrentIndex++;

        if (m_CurrentIndex >= m_Path.Count)
        {
            ReachGoal();
            return;
        }

        m_CurrentTarget = m_Path.GetPoint(m_CurrentIndex);
    }
    private void ReachGoal()
    {
        Debug.Log("Enemy reached goal");
        Destroy(gameObject);
    }
}
