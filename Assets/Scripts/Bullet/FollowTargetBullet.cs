using System.Collections;
using System.Collections.Generic;
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
                target = FindObjectOfType<PlayerController>().gameObject;
                break;
            case ETarget.Enemy:
                target = FindObjectOfType<EnemyController>().gameObject;
                break;
        }
    }

    public override void AddAbility()
    {
        StartCoroutine(FollowTarget());
    }

    IEnumerator FollowTarget()
    {
        yield return new WaitForSeconds(1);
        while (true)
        {
            LookAt2D(target.transform);
            yield return null;
        }
    }

    void LookAt2D(Transform targetTransform)
    {
        // 대상 방향 계산
        Vector3 direction = targetTransform.position - transform.position;

        // 각도 계산
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        angle -= 90;

        // Z축 회전만 설정
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }
}
