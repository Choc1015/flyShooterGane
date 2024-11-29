using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EWeaponOwner
{
    Player,
    Enemy
}

public class Gun : MonoBehaviour, IUpdatable
{
    // 총에 들어가 총알 프리팹 추가
    public GameObject BulletPrefab;
    // 총 주인
    public EWeaponOwner Owner;

    public float DelayShootAi = 1f;

    private void Awake()
    {
        // 초기 생성 함수
        Init();
    }
    private void Start()
    {
        if (Owner == EWeaponOwner.Enemy)
        {
            StartCoroutine(AiEnemyShooting());
        }
    }

    private void OnEnable()
    {
        UpdateManager.Instance?.Register(this);
    }

    private void OnDisable()
    {
        UpdateManager.Instance?.Unregister(this);
    }

    public void OnUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Space) && Owner == EWeaponOwner.Player)
        {
            // 발사 함수
            Shooting();
        }

    }

    void Init()
    {
        // 프리팹 널 체크
        if (BulletPrefab == null)
        {
            Debug.LogError("총알 프리팹이 적용되지 않았습니다.");
        }

    }

    IEnumerator AiEnemyShooting()
    {
        while (true) 
        {
            yield return new WaitForSeconds(DelayShootAi);
            Shooting();
        };
    }

    public void Shooting()
    {
        Debug.Log("발사");
        // 오브젝트 풀링 클래스에서 풀로 생성 * 추후 디스폰 함수 넣어야 함.
        GameObject bullet = ObjectPoolManager.Instance.SpawnFromPool(BulletPrefab.name, transform.position);
        // 총알의 회전 값을 총의 회전값에 맞추기
        bullet.transform.rotation = transform.rotation;
    }

}
