using System.Collections;
using UnityEngine;

/// <summary>
/// 유도 총알 마찬가지로 총알을 부모 클래스로 둠.
/// </summary>
public class FollowTargetBullet : Bullet
{
    public ETarget targetType; // 타겟이 뭔지를 타입을 정해준다.

    private GameObject target; // 찾은 타겟 오브젝트를 넣을 변수
    
    private void Awake()
    {
        // 타겟 초기화
        InitTarget();
    }

    /// <summary>
    /// 처음 타깃이 누군인지 설정
    /// </summary>
    private void InitTarget()
    {
        switch (targetType) // 타입이 누군인지 구별
        {
            case ETarget.Player: // 플레이어가 타깃일 때
                var player = FindObjectOfType<PlayerController>(); // var 오브젝트에 PlayerController가 있는 오브젝트를 저장
                target = player != null ? player.gameObject : null; // 삼항 정리로 player가 널이 아니면 player.GameObject 대입 널이면 널값 대입
                if (player == null) // 플레이어가 널이면 
                {
                    Debug.LogWarning("플레이어가 존재하지 않습니다!"); // 디버그로 플레이어가 없다고 발사
                }
                break;

            case ETarget.Enemy: // 적이 타깃일 때 
                var enemy = FindObjectOfType<EnemyController>(); // var 오브젝트에 EnemyController가 있는 오브젝트를 저장
                target = enemy != null ? enemy.gameObject : null; // 삼항 정리로 enemy가 널이 아니면 enemy.GameObject 대입 널이면 널값 대입
                if (enemy == null)
                {
                    Debug.LogWarning("적이 존재하지 않습니다!"); // 디버그로 적이 없다고 발사
                }
                break;
        }

        if (target == null) // 예외 사항 체크
        {
            Debug.Log("타겟이 없음"); 
        }
    }

    public override void AddAbility()
    {
        InitTarget(); // 비활성화 되었다가 다시 활성화 될 경우를 위해 타겟 찾는 함수 대입ㄴ
        StartCoroutine(FollowTarget()); // 따라가는 로직 코루틴 시작
    }

    /// <summary>
    ///  따라가는 함수
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
                        Debug.Log("타겟이 없음. 총알 비활성화 또는 파괴.");
                        ObjectPoolManager.Instance.DeSpawnToPool(gameObject); // 총알 비활성화
                        yield break;
                    }
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

    /// <summary>
    /// 적이 죽었을 때 총알이 새로운 적타깃을 찾는 함수
    /// </summary>
    /// <returns></returns>
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


    // 2D 기준으로 타깃을 바라보는 함수
    void LookAt2D(Transform targetTransform)
    {
        if (targetTransform == null)
            return;

        // 대상 방향 계산
        Vector3 direction = targetTransform.position - transform.position;

        // 각도 계산 Atan2 로 라디안 구하고 Rad2Deg로 각도로 변환
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        // 거기서 90도 빼주기 왜냐하면 기준이 오른쪽은 본 기준
        angle -= 90;

        // Z축 회전만 설정
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }
}
