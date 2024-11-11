using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolManager : Singleton<ObjectPoolManager>
{
    [System.Serializable] // Ŭ������ ����ȭ ��Ű�� ��, ���� ������ ��������
    public class Pool // Ǯ���� ������Ʈ
    {
        public string tag; // �±� ����
        public GameObject prefab; // Ǯ���� ������Ʈ
        public int size; // ������� Ǯ���� ����
    }

    public List<Pool> pools; // Ǯ���� ��ü��
    public Dictionary<string, Queue<GameObject>> poolDictionary; // Ǯ�� ��ųʸ��� �̸��� ���� Ǯ�� ��ŷ��Ͽ� ����

    private void Awake()
    {
        Initialize(); // �ʱ� ��
    }

    private void Initialize()
    {
        poolDictionary = new Dictionary<string, Queue<GameObject>>(); // ��ü ����

        foreach (Pool pool in pools) // ���� Ǯ�� ������
        {
            Queue<GameObject> objectPool = new Queue<GameObject>(); // ť ��ü ����
            GameObject fileObject = new GameObject("@" + pool.tag); // Ǯ���� ������Ʈ�� ����ϰ� ���̰� �ϱ� ���� �������Ʈ���� ����
            for (int i = 0; i < pool.size; i++) 
            {
                GameObject obj = Instantiate(pool.prefab); // Ǯ���� ������Ʈ ����
                obj.transform.SetParent(fileObject.transform); // �����ؼ� ����(�����) �ȿ� ����
                obj.SetActive(false); // ��Ȱ��ȭ ���ѳ���
                objectPool.Enqueue(obj); // ť�� ������ ����
            }

            poolDictionary.Add(pool.tag, objectPool); // �±׿� ������ ��ųʸ��� ����

        }
    }

    public GameObject SpawnFromPool(string tag, GameObject spawnPoint) // ��Ȱ��ȭ�� Ǯ���� ������Ʈ Ȱ��ȭ, �̸��� �޾Ƽ� ����
    {
        if (!poolDictionary.ContainsKey(tag)) // �̸��� ��ųʸ��� ������ ���� ���� ����
        {
            Debug.LogWarning("Pool with tag " + tag + " doesn't exist.");
            return null;
        }
        if (poolDictionary[tag] == null) // �̸��� �ִµ� �ȿ� Ǯ�� ������Ʈ�� ������ ���Ϥ�
        {
            return null;
        }
        GameObject objectToSpawn = poolDictionary[tag].Dequeue(); // �տ��� �� üũ ���� �ֱ⿡ ť���� ���� ����
        objectToSpawn.SetActive(true); // ������ Ȱ��ȭ
        objectToSpawn.transform.position = spawnPoint.transform.position;
        poolDictionary[tag].Enqueue(objectToSpawn); // Ȱ��ȭ�� ������ �ٽ� ť�� ����

        return objectToSpawn; // �׸��� �� ������ ����
    }
}