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
    public Dictionary<string, Queue<GameObject>> poolDictionary; // Ǯ�� ��ųʸ����� �����Ѵ�. 

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

        foreach (Pool pool in pools)
        {
            Queue<GameObject> objectPool = new Queue<GameObject>();
            GameObject parentObject = new GameObject("@" + pool.prefab.name);

            for (int i = 0; i < pool.size; i++)
            {
                objectPool.Enqueue(CreateNewObject(pool, parentObject.transform));
            }

            poolDictionary.Add(pool.prefab.name, objectPool);
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
        if (!poolDictionary.ContainsKey(tag)) // �ش� �̸��� Ǯ�� ������ �ִ��� üũ �뵵
        {
            Debug.LogWarning("Pool with tag " + tag + " doesn't exist.");
            return null;
        }

        if (poolDictionary[tag].Count == 0) // ���� �ش� Ǯ�� �����ŭ�� ť�� �� �̾ƽ�ٸ� Ǯ����� �ø��鼭 ����
        {
            Pool pool = pools.Find(p => p.prefab.name == tag); // find�� ����Ʈ���� �ش� Ǯ�� ã�Ƴ��� ������ Null�� ������ �̹� ���� Null üũ�� ��
            if (pool != null)
            {
                poolDictionary[tag].Enqueue(CreateNewObject(pool, GameObject.Find("@" + pool.prefab.name).transform));
            }
        }

        GameObject objectToSpawn = poolDictionary[tag].Dequeue();
        objectToSpawn.SetActive(true);
        objectToSpawn.transform.position = spawnPoint;

        return objectToSpawn;
    }

    // ���� �Լ� �������� ������ ��𼱰� �̰ɷ� ������ ��Ȱ��ȭ�� �������
    public void DeSpawnToPool(GameObject obj)
    {
        obj.transform.position = Vector3.zero; // ��ġ�� �ʱ�ȭ
        
        
        string tag = obj.name; // ������Ʈ �̸� TAG ����
        if (poolDictionary.ContainsKey(tag)) // Ǯ ��ųʸ��� ������ 
        {
            poolDictionary[tag].Enqueue(obj); // ������Ʈ �ٽ� ����
            obj.SetActive(false); // ��Ȱ��ȭ 
        }
        else
        {
            // �߸��� ������Ʈ�� ��� �α׸� ����� ����
            Debug.LogWarning("No pool exists for object: " + tag);
            Destroy(obj);
        }

       
    }
}
