using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 폭탄 총알입니다. 총알 스크립트의 기본 기능을 상속받습니다. 
/// </summary>
public class BombBullet : Bullet
{
    [SerializeField] GameObject BulletPrefab; //총알 프리팹 
    [SerializeField] int spreadBulletCount = 10; // 총알이 터지는 개수
    [SerializeField] float bombDelay = 1f; // 터지기까지 걸리는 시간

    public override void AddAbility() // 추가 능력
    {
        StartCoroutine(Bomb()); // 코루틴 발생
    }

    /// <summary>
    /// 폭발 기능
    /// </summary>
    /// <returns></returns>
    IEnumerator Bomb() 
    {
        float spreadAngle = 360 / spreadBulletCount; // 원을 기준으로 터지는 개수에 따른 각도 계산 예) 10개면 36도에 하나씩 터짐

        yield return new WaitForSeconds(bombDelay); // 해당 딜레이 만큼 기다리고 실행
        
        for (int i = 0; i < spreadBulletCount; i++) // for문 돌려서 개수 만큼 반복
        {
            // 오브젝트 풀링 클래스에서 풀로 생성
            GameObject bullet = ObjectPoolManager.Instance.SpawnFromPool(BulletPrefab.name, transform.position); 
            // 총알의 회전 값을 총의 회전값에 맞추기
            bullet.transform.rotation = Quaternion.Euler(Quaternion.identity.x, Quaternion.identity.y, spreadAngle * i);
        }
        gameObject.SetActive(false); // 원래 총알은 비활성화 그래야지 깔끔한 원이 만들어짐 중점 오브젝를 없앤다고 보면 됨.
        yield return null; // 끝났으니 널 리턴
    }

}
