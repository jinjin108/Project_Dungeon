using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    [SerializeField]
    private float m_MoveSpeed = 3f;

    private WaypointPath m_Path;
    private int m_CurrentIndex;
    private Transform m_Target;

    // DungeonManager에서 반드시 호출
    public void Initialize(WaypointPath path)
    {
        m_Path = path;
        m_CurrentIndex = 0;
        m_Target = m_Path.GetPoint(m_CurrentIndex);
    }

    private void Update()
    {
        if (m_Target == null)
            return;

        Vector3 dir = m_Target.position - transform.position;
        transform.position += dir.normalized * m_MoveSpeed * Time.deltaTime;

        if (dir.sqrMagnitude < 0.05f)
        {
            MoveNext();
        }
    }

    private void MoveNext()
    {
        m_CurrentIndex++;
        m_Target = m_Path.GetPoint(m_CurrentIndex);

        if (m_Target == null)
        {
            DungeonManager.Instance.OnEnemyReachedGoal();
            Destroy(gameObject);
        }
    }
}
