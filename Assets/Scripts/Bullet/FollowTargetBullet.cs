using System.Collections;
using UnityEngine;

public class FollowTargetBullet : Bullet
{
    private GameObject target;
    public ETarget targetType;

    private void Awake()
    {
        InitTarget();
    }

    private void InitTarget()
    {
        switch (targetType)
        {
            case ETarget.Player:
                var player = FindObjectOfType<PlayerController>();
                target = player != null ? player.gameObject : null;
                if (player == null)
                {
                    Debug.LogWarning("플레이어가 존재하지 않습니다!");
                }
                break;

            case ETarget.Enemy:
                var enemy = FindObjectOfType<EnemyController>();
                target = enemy != null ? enemy.gameObject : null;
                if (enemy == null)
                {
                    Debug.LogWarning("적이 존재하지 않습니다!");
                }
                break;
        }

        if (target == null)
        {
            Debug.Log("타겟을 찾을 수 없습니다!");
        }
    }

    public override void AddAbility()
    {
        InitTarget();
        StartCoroutine(FollowTarget());
    }

    IEnumerator FollowTarget()
    {
        yield return new WaitForSeconds(1);

        while (true)
        {
            switch (targetType)
            {
                case ETarget.Player:
                    if (target == null)
                        yield break;
                    break;
                case ETarget.Enemy:
                    if (target == null || !target.GetComponent<EnemyController>().IsAlive)
                    {
                        // 새로운 타겟 검색
                        target = FindNewTarget();

                        if (target == null)
                        {
                            Debug.Log("타겟이 없음. 총알 비활성화 또는 파괴.");
                            ObjectPoolManager.Instance.DeSpawnToPool(gameObject); // 총알 비활성화
                            yield break;
                        }
                    }
                    break;
            }
            // 타겟이 유효한 경우 회전 및 이동 처리
            LookAt2D(target.transform);
            yield return null;
        }
    }

    private GameObject FindNewTarget()
    {
        var enemies = FindObjectsOfType<EnemyController>();
        float closestDistance = Mathf.Infinity;
        GameObject newTarget = null;

        foreach (var enemy in enemies)
        {
            if (enemy.IsAlive) // 살아있는 적만 고려
            {
                float distance = Vector3.Distance(transform.position, enemy.transform.position);
                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    newTarget = enemy.gameObject;
                }
            }
        }

        return newTarget;
    }


    void LookAt2D(Transform targetTransform)
    {
        if (targetTransform == null)
            return;

        // 대상 방향 계산
        Vector3 direction = targetTransform.position - transform.position;

        // 각도 계산
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        angle -= 90;

        // Z축 회전만 설정
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }
}
