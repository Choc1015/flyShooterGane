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
                    Debug.LogWarning("�÷��̾ �������� �ʽ��ϴ�!");
                }
                break;

            case ETarget.Enemy:
                var enemy = FindObjectOfType<EnemyController>();
                target = enemy != null ? enemy.gameObject : null;
                if (enemy == null)
                {
                    Debug.LogWarning("���� �������� �ʽ��ϴ�!");
                }
                break;
        }

        if (target == null)
        {
            Debug.Log("Ÿ���� ã�� �� �����ϴ�!");
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
                        // ���ο� Ÿ�� �˻�
                        target = FindNewTarget();

                        if (target == null)
                        {
                            Debug.Log("Ÿ���� ����. �Ѿ� ��Ȱ��ȭ �Ǵ� �ı�.");
                            ObjectPoolManager.Instance.DeSpawnToPool(gameObject); // �Ѿ� ��Ȱ��ȭ
                            yield break;
                        }
                    }
                    break;
            }
            // Ÿ���� ��ȿ�� ��� ȸ�� �� �̵� ó��
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
            if (enemy.IsAlive) // ����ִ� ���� ���
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

        // ��� ���� ���
        Vector3 direction = targetTransform.position - transform.position;

        // ���� ���
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        angle -= 90;

        // Z�� ȸ���� ����
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }
}
