using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolManager : Singleton<ObjectPoolManager>
{
    [System.Serializable]
    public class Pool // Ǯ�� �����հ� Ǯ���� ����� ������ �ִ�.
    {
        public GameObject prefab;
        public int size;
    }

    public List<Pool> pools; // ����Ʈ�� ���� Ǯ���� �����Ѵ�. 
    private Dictionary<string, Queue<GameObject>> poolDictionary; // Ǯ�� ��ųʸ����� �����Ѵ�. 
    private Dictionary<string, HashSet<GameObject>> activeObjects; // Ȱ��ȭ�� ������Ʈ ����

    private void Awake()
    {
        Initialize();
    }

    /// <summary>
    /// ���� Pool���� ���鼭 �ϳ��� Pool�� ť����Ʈ�� ����� ���� ������Ʈ ����
    /// �׸��� Ǯ ������ ��ŭ ������Ʈ �����Ͽ� ť�ȿ� ���� 
    /// �׸��� �� ť�� ��ųʸ��� ����
    /// </summary>
    private void Initialize()
    {
        poolDictionary = new Dictionary<string, Queue<GameObject>>();
        activeObjects = new Dictionary<string, HashSet<GameObject>>();

        foreach (Pool pool in pools)
        {
            Queue<GameObject> objectPool = new Queue<GameObject>();
            HashSet<GameObject> activeSet = new HashSet<GameObject>();

            GameObject parentObject = new GameObject("@" + pool.prefab.name);

            for (int i = 0; i < pool.size; i++)
            {
                objectPool.Enqueue(CreateNewObject(pool, parentObject.transform));
            }

            poolDictionary.Add(pool.prefab.name, objectPool);
            activeObjects.Add(pool.prefab.name, activeSet);
        }
    }


    /// <summary>
    /// ������Ʈ ���� �Լ� Ǯ�� �θ� ������Ʈ�� �޾Ƽ� �θ� ������Ʈ�� �׳� �� �����̰� �����ؼ� �θ� �������� �����Ѵ��� ��Ȱ��ȭ 
    /// </summary>
    /// <param name="pool"></param>
    /// <param name="parent"></param>
    /// <returns></returns>
    private GameObject CreateNewObject(Pool pool, Transform parent)
    {
        GameObject obj = Instantiate(pool.prefab);
        obj.name = pool.prefab.name;
        obj.transform.SetParent(parent);
        parent.transform.SetParent(transform);
        obj.SetActive(false);
        return obj;
    }

    /// <summary>
    /// ������Ʈ ���� �Լ� �±׿� ��Ҹ� �޴´�.
    /// </summary>
    /// <param name="tag"></param>
    /// <param name="spawnPoint"></param>
    /// <returns></returns>
    public GameObject SpawnFromPool(string tag, Vector3 spawnPoint)
    {
        if (!poolDictionary.ContainsKey(tag))
        {
            Debug.LogWarning("Pool with tag " + tag + " doesn't exist.");
            return null;
        }

        if (poolDictionary[tag].Count == 0)
        {
            Pool pool = pools.Find(p => p.prefab.name == tag);
            if (pool != null)
            {
                poolDictionary[tag].Enqueue(CreateNewObject(pool, GameObject.Find("@" + pool.prefab.name).transform));
            }
        }

        GameObject objectToSpawn = poolDictionary[tag].Dequeue();
        objectToSpawn.SetActive(true);
        objectToSpawn.transform.position = spawnPoint;

        activeObjects[tag].Add(objectToSpawn); // Ȱ��ȭ�� ������Ʈ�� ���
        return objectToSpawn;
    }


    // ���� �Լ� �������� ������ ��𼱰� �̰ɷ� ������ ��Ȱ��ȭ�� �������
    public void DeSpawnToPool(GameObject obj)
    {
        obj.transform.position = Vector3.zero;

        string tag = obj.name;
        if (poolDictionary.ContainsKey(tag))
        {
            poolDictionary[tag].Enqueue(obj);
            activeObjects[tag].Remove(obj); // Ȱ��ȭ�� ������Ʈ���� ����
            obj.SetActive(false);
        }
        else
        {
            Debug.LogWarning("No pool exists for object: " + tag);
            Destroy(obj);
        }
    }


    /// <summary>
    /// ��� Ȱ��ȭ�� �������� �����Ͽ� Ǯ�� ��ȯ
    /// </summary>
    public void DespawnAll()
    {
        foreach (var pair in activeObjects)
        {
            string tag = pair.Key;
            HashSet<GameObject> activeSet = pair.Value;

            // Ȱ��ȭ�� ��� ������Ʈ�� ����
            foreach (var obj in new List<GameObject>(activeSet)) // ����Ʈ�� ���� �� ó��
            {
                DeSpawnToPool(obj);
            }
        }

        Debug.Log("��� Ȱ��ȭ�� ������Ʈ�� �����Ǿ����ϴ�.");
    }



}
