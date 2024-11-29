using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombBullet : Bullet
{
    [SerializeField] GameObject BulletPrefab;
    [SerializeField] int spreadBulletCount = 10;
    [SerializeField] float bombDelay = 1f;

    public override void AddAbility()
    {
        StartCoroutine(Bomb());
       
    }

    IEnumerator Bomb()
    {
        float spreadAngle = 360 / spreadBulletCount;

        yield return new WaitForSeconds(bombDelay);
        
        for (int i = 0; i < spreadBulletCount; i++)
        {
            // 오브젝트 풀링 클래스에서 풀로 생성 * 추후 디스폰 함수 넣어야 함.
            GameObject bullet = ObjectPoolManager.Instance.SpawnFromPool(BulletPrefab.name, transform.position);
            // 총알의 회전 값을 총의 회전값에 맞추기
            bullet.transform.rotation = Quaternion.Euler(Quaternion.identity.x, Quaternion.identity.y, spreadAngle * i);
        }
        gameObject.SetActive(false);
        yield return null;
    }

}
