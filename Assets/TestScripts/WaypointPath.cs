using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointPath : MonoBehaviour
{
    [SerializeField]
    private Transform[] m_Points;

    public int Count
    {
        get { return m_Points.Length; }
    }

    public Transform GetPoint(int index)
    {
        if (index < 0 || index >= m_Points.Length)
            return null;

        return m_Points[index];
    }
}
