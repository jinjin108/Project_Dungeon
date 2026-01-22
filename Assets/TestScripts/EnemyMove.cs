using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    [SerializeField]
    private float m_MoveSpeed = 3f;

    private WaypointPath m_Path;
    private int m_CurrentIndex;
    private bool m_IsMoving;

    public void SetPath(WaypointPath path)
    {
        m_Path = path;
        m_CurrentIndex = 0;
        m_IsMoving = true;

        transform.position = m_Path.GetPoint(0).position;
    }

    private void Update()
    {
        if (!m_IsMoving || m_Path == null)
            return;

        MoveToWaypoint();
    }

    private void MoveToWaypoint()
    {
        Transform target = m_Path.GetPoint(m_CurrentIndex);
        if (target == null)
            return;

        Vector3 dir = target.position - transform.position;
        float distance = dir.magnitude;

        if (distance < 0.1f)
        {
            m_CurrentIndex++;

            if (m_CurrentIndex >= m_Path.Count)
            {
                m_IsMoving = false;
                EnemyArrived();
            }

            return;
        }

        transform.position += dir.normalized * m_MoveSpeed * Time.deltaTime;
    }

    private void EnemyArrived()
    {
        Enemy enemy = GetComponent<Enemy>();

        EventManager.RaiseEnemyArrived(enemy);
        ObjectPool.Instance.Return(this);
    }
}