using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EEnemyMove
{
    NotMove, // 움직이지 않는다
    StraightMove, // 일직선 움직임
    FollowPlayer, // 플레이어를 따라간다.
    MoveInZigzag, // 지그재그 움직임
    MoveSideToSide // 좌우 움직임
}

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] EEnemyMove move;
    private Vector3 startPosition;
    
    private void Start()
    {
        startPosition = transform.position;
    }

    public void InitMove(float enemySpeed,GameObject player, float zigzagSpeed, float range)
    {
        switch (move)
        {
            case EEnemyMove.NotMove:
                // 움직이지 않음으로 아무것도 없음.
                break;
            case EEnemyMove.StraightMove:
                StraightMove(enemySpeed);
                break;
            case EEnemyMove.FollowPlayer:
                FollowPlayer(player.transform, enemySpeed);
                break;
            case EEnemyMove.MoveInZigzag:
                MoveZigzag(enemySpeed, zigzagSpeed, range);
                break;
            case EEnemyMove.MoveSideToSide:
                MoveSideToSide(enemySpeed, range);
                break;
        }
    }

    void StraightMove(float speed)
    {
        // 아래로 움직이는 코드
        transform.Translate(Vector2.down * speed * Time.deltaTime);
    }

    void FollowPlayer(Transform playerTransform, float speed)
    {
        // 현재 위치와 목표 위치의 방향 계산
        Vector3 direction = (playerTransform.position - transform.position).normalized;

        // 이동
        transform.position += direction * speed * Time.deltaTime;

        // 플레이어 쳐다보기 
        LookAt2D(playerTransform);
    }

    void MoveZigzag(float speed, float zigzagSpeed, float range)
    {
        // 직선
        StraightMove(speed);
        
        // 지그재그
        MoveSideToSide(zigzagSpeed, range);
    }

    void MoveSideToSide(float zigzagSpeed, float range)
    {
        // 초기 위치 기준으로 좌우로 진동하는 위치 계산
        float offset = Mathf.Sin(Time.time * zigzagSpeed) * range;

        // 새 위치 적용
        transform.position = new Vector3(startPosition.x + offset, transform.position.y, transform.position.z);
    }

    void LookAt2D(Transform targetTransform)
    {
        // 대상 방향 계산
        Vector3 direction = targetTransform.position - transform.position;

        // 각도 계산
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        angle += 90;

        // Z축 회전만 설정
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }
}
