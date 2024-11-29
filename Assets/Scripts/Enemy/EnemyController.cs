using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(EnemyInfo), typeof(EnemyMovement))]
public class EnemyController : MonoBehaviour,IUpdatable
{
    // Àû Á¤º¸
    private EnemyInfo enemyInfo;
    private EnemyMovement movement;
    private GameObject target;

    private void Awake()
    {
        enemyInfo = GetComponent<EnemyInfo>();
        movement = GetComponent<EnemyMovement>();
        target = FindObjectOfType<PlayerController>().gameObject;
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
        movement.InitMove(enemyInfo.speed, target, enemyInfo.zigzagSpeed, enemyInfo.range);
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

        if (enemyInfo.health == 0)
        {
            Destroy(gameObject);
        }

    }

   
}
