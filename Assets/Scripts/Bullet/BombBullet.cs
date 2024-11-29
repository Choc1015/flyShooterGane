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
            // ������Ʈ Ǯ�� Ŭ�������� Ǯ�� ���� * ���� ���� �Լ� �־�� ��.
            GameObject bullet = ObjectPoolManager.Instance.SpawnFromPool(BulletPrefab.name, transform.position);
            // �Ѿ��� ȸ�� ���� ���� ȸ������ ���߱�
            bullet.transform.rotation = Quaternion.Euler(Quaternion.identity.x, Quaternion.identity.y, spreadAngle * i);
        }
        gameObject.SetActive(false);
        yield return null;
    }

}
