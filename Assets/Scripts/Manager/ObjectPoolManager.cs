using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolManager : Singleton<ObjectPoolManager>
{
    [System.Serializable] // 클래스를 직렬화 시키는 법, 이전 정보를 저장해줌
    public class Pool // 풀링할 오브젝트
    {
        public string tag; // 태그 저장
        public GameObject prefab; // 풀링할 오브젝트
        public int size; // 어느정도 풀링할 건지
    }

    public List<Pool> pools; // 풀링할 객체들
    public Dictionary<string, Queue<GameObject>> poolDictionary; // 풀링 딕셔너리로 이름에 따라 풀링 딕셔러니에 보관

    private void Awake()
    {
        Initialize(); // 초기 값
    }

    private void Initialize()
    {
        poolDictionary = new Dictionary<string, Queue<GameObject>>(); // 객체 생성

        foreach (Pool pool in pools) // 많은 풀링 돌리기
        {
            Queue<GameObject> objectPool = new Queue<GameObject>(); // 큐 객체 생성
            GameObject fileObject = new GameObject("@" + pool.tag); // 풀링된 오브젝트를 깔끔하게 보이게 하기 위해 빈오브젝트에서 관리
            for (int i = 0; i < pool.size; i++) 
            {
                GameObject obj = Instantiate(pool.prefab); // 풀링된 오브젝트 생성
                obj.transform.SetParent(fileObject.transform); // 생성해서 폴더(빈옵젝) 안에 저장
                obj.SetActive(false); // 비활성화 시켜놓기
                objectPool.Enqueue(obj); // 큐에 데이터 저장
            }

            poolDictionary.Add(pool.tag, objectPool); // 태그와 옵젝을 딕셔너리에 저장

        }
    }

    public GameObject SpawnFromPool(string tag, GameObject spawnPoint) // 비활성화로 풀링된 오브젝트 활성화, 이름을 받아서 실행
    {
        if (!poolDictionary.ContainsKey(tag)) // 이름이 딕셔너리에 없으면 오류 구문 실행
        {
            Debug.LogWarning("Pool with tag " + tag + " doesn't exist.");
            return null;
        }
        if (poolDictionary[tag] == null) // 이름은 있는데 안에 풀링 오브젝트가 없을시 리턴ㅋ
        {
            return null;
        }
        GameObject objectToSpawn = poolDictionary[tag].Dequeue(); // 앞에서 널 체크 이후 있기에 큐에서 꺼내 쓰기
        objectToSpawn.SetActive(true); // 꺼내고 활성화
        objectToSpawn.transform.position = spawnPoint.transform.position;
        poolDictionary[tag].Enqueue(objectToSpawn); // 활성화후 정보를 다시 큐에 저장

        return objectToSpawn; // 그리고 그 옵젝을 리턴
    }
}