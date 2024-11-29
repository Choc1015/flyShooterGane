using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolManager : Singleton<ObjectPoolManager>
{
    [System.Serializable]
    public class Pool // 풀은 프리팹과 풀링될 사이즈를 가지고 있다.
    {
        public GameObject prefab;
        public int size;
    }

    public List<Pool> pools; // 리스트에 여러 풀들을 관리한다. 
    public Dictionary<string, Queue<GameObject>> poolDictionary; // 풀을 딕셔너리에서 생성한다. 

    private void Awake()
    {
        Initialize();
    }

    /// <summary>
    /// 여러 Pool들을 돌면서 하나의 Pool을 큐리스트로 만들고 게임 오브젝트 생성
    /// 그리고 풀 사이즈 만큼 오브젝트 생성하여 큐안에 저장 
    /// 그리고 그 큐를 딕셔너리에 저장
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
    /// 오브젝트 생성 함수 풀과 부모 오브젝트를 받아서 부모 오브젝트는 그냥 빈 폴더이고 생성해서 부모 오브젝에 정리한다음 비활성화 
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
    /// 오브젝트 스폰 함수 태그와 장소를 받는다.
    /// </summary>
    /// <param name="tag"></param>
    /// <param name="spawnPoint"></param>
    /// <returns></returns>
    public GameObject SpawnFromPool(string tag, Vector3 spawnPoint)
    {
        if (!poolDictionary.ContainsKey(tag)) // 해당 이름의 풀을 가지고 있는지 체크 용도
        {
            Debug.LogWarning("Pool with tag " + tag + " doesn't exist.");
            return null;
        }

        if (poolDictionary[tag].Count == 0) // 만약 해당 풀의 사이즈만큼의 큐를 다 뽑아썼다면 풀사이즈를 늘리면서 생성
        {
            Pool pool = pools.Find(p => p.prefab.name == tag); // find로 리스트에서 해당 풀을 찾아내기 없으면 Null을 뱉지만 이미 전에 Null 체크를 함
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

    // 디스폰 함수 리스폰을 했으니 어디선가 이걸로 프리팹 비활성화를 해줘야함
    public void DeSpawnToPool(GameObject obj)
    {
        obj.transform.position = Vector3.zero; // 위치값 초기화
        
        
        string tag = obj.name; // 오브젝트 이름 TAG 저장
        if (poolDictionary.ContainsKey(tag)) // 풀 딕셔너리에 있으면 
        {
            poolDictionary[tag].Enqueue(obj); // 오브젝트 다시 저장
            obj.SetActive(false); // 비활성화 
        }
        else
        {
            // 잘못된 오브젝트일 경우 로그를 남기고 삭제
            Debug.LogWarning("No pool exists for object: " + tag);
            Destroy(obj);
        }

       
    }
}
