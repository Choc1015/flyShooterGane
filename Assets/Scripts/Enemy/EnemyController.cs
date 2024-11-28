using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(StatInfo))]
public class EnemyController : MonoBehaviour
{
    // Àû Á¤º¸
    private StatInfo enemyInfo;

    private void Awake()
    {
        enemyInfo = GetComponent<StatInfo>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
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
