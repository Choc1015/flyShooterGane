using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ��ź �Ѿ��Դϴ�. �Ѿ� ��ũ��Ʈ�� �⺻ ����� ��ӹ޽��ϴ�. 
/// </summary>
public class BombBullet : Bullet
{
    [SerializeField] GameObject BulletPrefab; //�Ѿ� ������ 
    [SerializeField] int spreadBulletCount = 10; // �Ѿ��� ������ ����
    [SerializeField] float bombDelay = 1f; // ��������� �ɸ��� �ð�

    public override void AddAbility() // �߰� �ɷ�
    {
        StartCoroutine(Bomb()); // �ڷ�ƾ �߻�
    }

    /// <summary>
    /// ���� ���
    /// </summary>
    /// <returns></returns>
    IEnumerator Bomb() 
    {
        float spreadAngle = 360 / spreadBulletCount; // ���� �������� ������ ������ ���� ���� ��� ��) 10���� 36���� �ϳ��� ����

        yield return new WaitForSeconds(bombDelay); // �ش� ������ ��ŭ ��ٸ��� ����
        
        for (int i = 0; i < spreadBulletCount; i++) // for�� ������ ���� ��ŭ �ݺ�
        {
            // ������Ʈ Ǯ�� Ŭ�������� Ǯ�� ����
            GameObject bullet = ObjectPoolManager.Instance.SpawnFromPool(BulletPrefab.name, transform.position); 
            // �Ѿ��� ȸ�� ���� ���� ȸ������ ���߱�
            bullet.transform.rotation = Quaternion.Euler(Quaternion.identity.x, Quaternion.identity.y, spreadAngle * i);
        }
        gameObject.SetActive(false); // ���� �Ѿ��� ��Ȱ��ȭ �׷����� ����� ���� ������� ���� �������� ���شٰ� ���� ��.
        yield return null; // �������� �� ����
    }

}
