using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Bullet : MonoBehaviour, IUpdatable
{
    // �Ѿ� �ӵ�
    [SerializeField] private float bulletSpeed;
    // �Ѿ� ������
    private int bulletDamage = 1;
    
    // ���� ��
    const int endX = 6;
    const int endY = 10;

    public void OnEnable()
    {
        UpdateManager.Instance?.Register(this);
    }

    protected void OnDisable()
    {
        UpdateManager.Instance?.Unregister(this);
    }
    private void Start()
    {


        // �ڷ�ƾ���� �߰��ɷ��� Ȯ���� ��
        AddAbility(); 
    }

    public void OnUpdate()
    {
        // �������� ������ �ڵ����� ������ ��.
        ShootBullet();
        DespawnOnExit();
    }

    // �ڷ�ƾ���� �߰��ɷ��� Ȯ���� ��
    public virtual void AddAbility()
    {

    }

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

}
