using System.Collections;
using UnityEngine;

/// <summary>
/// ���� �Ѿ� ���������� �Ѿ��� �θ� Ŭ������ ��.
/// </summary>
public class FollowTargetBullet : Bullet
{
    public ETarget targetType; // Ÿ���� ������ Ÿ���� �����ش�.

    private GameObject target; // ã�� Ÿ�� ������Ʈ�� ���� ����
    
    private void Awake()
    {
        // Ÿ�� �ʱ�ȭ
        InitTarget();
    }

    /// <summary>
    /// ó�� Ÿ���� �������� ����
    /// </summary>
    private void InitTarget()
    {
        switch (targetType) // Ÿ���� �������� ����
        {
            case ETarget.Player: // �÷��̾ Ÿ���� ��
                var player = FindObjectOfType<PlayerController>(); // var ������Ʈ�� PlayerController�� �ִ� ������Ʈ�� ����
                target = player != null ? player.gameObject : null; // ���� ������ player�� ���� �ƴϸ� player.GameObject ���� ���̸� �ΰ� ����
                if (player == null) // �÷��̾ ���̸� 
                {
                    Debug.LogWarning("�÷��̾ �������� �ʽ��ϴ�!"); // ����׷� �÷��̾ ���ٰ� �߻�
                }
                break;

            case ETarget.Enemy: // ���� Ÿ���� �� 
                var enemy = FindObjectOfType<EnemyController>(); // var ������Ʈ�� EnemyController�� �ִ� ������Ʈ�� ����
                target = enemy != null ? enemy.gameObject : null; // ���� ������ enemy�� ���� �ƴϸ� enemy.GameObject ���� ���̸� �ΰ� ����
                if (enemy == null)
                {
                    Debug.LogWarning("���� �������� �ʽ��ϴ�!"); // ����׷� ���� ���ٰ� �߻�
                }
                break;
        }

        if (target == null) // ���� ���� üũ
        {
            Debug.Log("Ÿ���� ����"); 
        }
    }

    public override void AddAbility()
    {
        InitTarget(); // ��Ȱ��ȭ �Ǿ��ٰ� �ٽ� Ȱ��ȭ �� ��츦 ���� Ÿ�� ã�� �Լ� ���Ԥ�
        StartCoroutine(FollowTarget()); // ���󰡴� ���� �ڷ�ƾ ����
    }

    /// <summary>
    ///  ���󰡴� �Լ�
    /// </summary>
    /// <returns></returns>
    IEnumerator FollowTarget()
    {
        yield return new WaitForSeconds(1);

        while (true)
        {
            switch (targetType)
            {
                case ETarget.Player:
                    if (target == null)
                    {
                        Debug.Log("Ÿ���� ����. �Ѿ� ��Ȱ��ȭ �Ǵ� �ı�.");
                        ObjectPoolManager.Instance.DeSpawnToPool(gameObject); // �Ѿ� ��Ȱ��ȭ
                        yield break;
                    }
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

    /// <summary>
    /// ���� �׾��� �� �Ѿ��� ���ο� ��Ÿ���� ã�� �Լ�
    /// </summary>
    /// <returns></returns>
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


    // 2D �������� Ÿ���� �ٶ󺸴� �Լ�
    void LookAt2D(Transform targetTransform)
    {
        if (targetTransform == null)
            return;

        // ��� ���� ���
        Vector3 direction = targetTransform.position - transform.position;

        // ���� ��� Atan2 �� ���� ���ϰ� Rad2Deg�� ������ ��ȯ
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        // �ű⼭ 90�� ���ֱ� �ֳ��ϸ� ������ �������� �� ����
        angle -= 90;

        // Z�� ȸ���� ����
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }
}
