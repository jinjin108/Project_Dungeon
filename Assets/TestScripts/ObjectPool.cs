using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PoolSettings
{
    public GameObject prefab;
    public int prewarmCount;
}

public class ObjectPool : MonoBehaviour
{
    private static ObjectPool m_Instance;
    public static ObjectPool Instance => m_Instance;

    [SerializeField]
    private PoolSettings[] m_PoolSettings;

    [SerializeField]
    private Transform m_Enemys;
    private Dictionary<GameObject, Queue<GameObject>> m_PoolDictionary = new();
    private Dictionary<GameObject, GameObject> m_InstanceToPrefab = new();

    private void Awake()
    {
        if (m_Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        m_Instance = this;
        DontDestroyOnLoad(gameObject);

        InitializePools();
    }

    private void InitializePools()
    {
        if (m_PoolSettings == null)
            return;

        foreach (PoolSettings settings in m_PoolSettings)
        {
            if (settings.prefab == null || settings.prewarmCount <= 0)
                continue;

            Prewarm(settings.prefab, settings.prewarmCount);
        }
    }

    private void Prewarm(GameObject prefab, int count)
    {
        if (!m_PoolDictionary.ContainsKey(prefab))
            m_PoolDictionary[prefab] = new Queue<GameObject>();

        for (int i = 0; i < count; i++)
        {
            GameObject instance = Instantiate(prefab, m_Enemys);
            instance.SetActive(false);

            m_InstanceToPrefab[instance] = prefab;
            m_PoolDictionary[prefab].Enqueue(instance);
        }
    }

    public T Get<T>(T prefab) where T : Component
    {
        GameObject prefabObj = prefab.gameObject;

        if (!m_PoolDictionary.ContainsKey(prefabObj))
            m_PoolDictionary[prefabObj] = new Queue<GameObject>();

        GameObject instance;

        if (m_PoolDictionary[prefabObj].Count > 0)
        {
            instance = m_PoolDictionary[prefabObj].Dequeue();
        }
        else
        {
            instance = Instantiate(prefabObj, m_Enemys);
            m_InstanceToPrefab[instance] = prefabObj;
        }

        instance.transform.SetParent(m_Enemys);
        instance.SetActive(true);

        return instance.GetComponent<T>();
    }

    public void Return(GameObject instance)
    {
        if (instance == null)
            return;

        if (!m_InstanceToPrefab.TryGetValue(instance, out GameObject prefab))
        {
            Destroy(instance);
            return;
        }

        instance.SetActive(false);
        instance.transform.SetParent(m_Enemys);

        m_PoolDictionary[prefab].Enqueue(instance);
    }

    public void Return<T>(T component) where T : Component
    {
        Return(component.gameObject);
    }
}