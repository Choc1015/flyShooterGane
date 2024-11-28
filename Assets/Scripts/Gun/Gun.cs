using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour,IUpdatable
{
    // 총에 들어가 총알 프리팹 추가
    public GameObject BulletPrefab;

    // 총의 부모 
    private string whoChar;

    private void Awake()
    {
        // 초기 생성 함수
        Init();
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
        // 업데이트에서 스페이스바를 눌렀을 시 발사 , 근데 이거 이벤트로 수정 예정, 계속 체크하는 거 너무 불안정
        if (Input.GetKeyDown(KeyCode.Space))
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

        whoChar = transform.parent.name;

        Debug.Log(whoChar);
    }
    public void Shooting()
    {
        Debug.Log("발사");
        // 오브젝트 풀링 클래스에서 풀로 생성 * 추후 디스폰 함수 넣어야 함.
        GameObject bullet = ObjectPoolManager.Instance.SpawnFromPool(BulletPrefab.name, transform.position );
        // 총알의 회전 값을 총의 회전값에 맞추기
        bullet.transform.rotation = transform.rotation;
    }

}
