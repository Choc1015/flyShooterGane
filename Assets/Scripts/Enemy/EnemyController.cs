using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(EnemyInfo), typeof(EnemyMovement))]
public class EnemyController : MonoBehaviour,IUpdatable
{
    public bool IsAlive { get; private set; } = true;

    // �� ����
    private EnemyInfo enemyInfo;
    private EnemyMovement movement;
    private GameObject target;

    // ���� ��
    private const int endX = 20;
    private const int endY = 20;

    private void Start()
    {
        enemyInfo = GetComponent<EnemyInfo>();
        movement = GetComponent<EnemyMovement>();
        target = FindObjectOfType<PlayerController>()?.gameObject;
    }

    private void OnEnable()
    {
        UpdateManager.Instance?.Register(this);
        IsAlive = true;
        target = FindObjectOfType<PlayerController>().gameObject;
    }

    private void OnDisable()
    {
        IsAlive = false;
        UpdateManager.Instance?.Unregister(this);
    }

    public void OnUpdate()
    {
        if (GameManager.Instance.IsPause == false || !IsAlive)
            return;

        if(target == null)
            target = FindObjectOfType<PlayerController>()?.gameObject;

        movement?.InitMove(enemyInfo.speed, target, enemyInfo.zigzagSpeed, enemyInfo.range);
        DespawnOnExit();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("PlayerBullet"))
        {
            OnDamaged(collision.gameObject.GetComponent<Bullet>().GetDamage());
            ObjectPoolManager.Instance.DeSpawnToPool(collision.gameObject);
        }
    }

    private void OnDamaged(int DamageValue)
    {
        enemyInfo.health -= DamageValue;

        Die();

    }

    private void Die()
    {
        if (enemyInfo.health == 0)
        {
            IsAlive = false;
            GameManager.Instance.EnemyCount--;
            ObjectPoolManager.Instance.DeSpawnToPool(gameObject);
        }
    }


    // Ư�� ������ ������ ����
    public void DespawnOnExit()
    {
        if (!IsInRange(transform.position.x, -endX, endX) || !IsInRange(transform.position.y, -endY, endY))
        {
            GameManager.Instance.EnemyCount--;
            ObjectPoolManager.Instance.DeSpawnToPool(gameObject);
        }
    }

    // ���� �ȿ� �ִ� �� Ȯ���ϴ� �޼���
    public bool IsInRange(float value, float min, float max)
    {
        return value >= min && value <= max;
    }
}
