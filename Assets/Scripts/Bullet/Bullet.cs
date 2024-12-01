using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ETarget
{
    Player,
    Enemy
}

public abstract class Bullet : MonoBehaviour, IUpdatable
{
    // �Ѿ� �ӵ�
    [SerializeField] private float bulletSpeed;
    // �Ѿ� ������
    private int bulletDamage = 1;
    private float bulletDurationTime = 5;
    // ���� ��
    const int endX = 11;
    const int endY = 20;

    private void OnEnable()
    {
        UpdateManager.Instance?.Register(this);
        AddAbility();
        StartCoroutine(ClearBullet());
    }

    private void OnDisable()
    {
        UpdateManager.Instance?.Unregister(this);
    }

    public void OnUpdate()
    {
        // �������� ������ �ڵ����� ������ ��.
        ShootBullet();
        DespawnOnExit();
    }

    // �ڷ�ƾ���� �߰��ɷ��� Ȯ���� ��
    public virtual void AddAbility() { }
    // �Ѿ� ������ ����
    public void ShootBullet()
    {
        transform.Translate(Vector2.up * bulletSpeed * Time.deltaTime);
    }

    // Ư�� ������ ������ ����
    public void DespawnOnExit()
    {
        if (!IsInRange(transform.position.x,-endX,endX) || !IsInRange(transform.position.y, -endY, endY))
        {
            ObjectPoolManager.Instance.DeSpawnToPool(gameObject);
        }
    }

    // ���� �ȿ� �ִ� �� Ȯ���ϴ� �޼���
    public bool IsInRange(float value, float min, float max)
    {
        return value >= min && value <= max;
    }

    // �ڽ� Ŭ�������� ������ ���� ����
    public void SetDamage(int value)
    {
        bulletDamage = value;
    }

    // ������ �о����
    public int GetDamage()
    {
        return bulletDamage;
    }

    IEnumerator ClearBullet()
    {
        
        yield return new WaitForSeconds(bulletDurationTime);
        ObjectPoolManager.Instance.DeSpawnToPool(gameObject);
        yield return null;
    }

}
