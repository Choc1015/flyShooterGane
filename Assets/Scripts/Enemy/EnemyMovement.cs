using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EEnemyMove
{
    NotMove, // �������� �ʴ´�
    StraightMove, // ������ ������
    FollowPlayer, // �÷��̾ ���󰣴�.
    MoveInZigzag, // ������� ������
    MoveSideToSide // �¿� ������
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
                // �������� �������� �ƹ��͵� ����.
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
        // �Ʒ��� �����̴� �ڵ�
        transform.Translate(Vector2.down * speed * Time.deltaTime);
    }

    void FollowPlayer(Transform playerTransform, float speed)
    {
        // ���� ��ġ�� ��ǥ ��ġ�� ���� ���
        Vector3 direction = (playerTransform.position - transform.position).normalized;

        // �̵�
        transform.position += direction * speed * Time.deltaTime;

        // �÷��̾� �Ĵٺ��� 
        LookAt2D(playerTransform);
    }

    void MoveZigzag(float speed, float zigzagSpeed, float range)
    {
        // ����
        StraightMove(speed);
        
        // �������
        MoveSideToSide(zigzagSpeed, range);
    }

    void MoveSideToSide(float zigzagSpeed, float range)
    {
        // �ʱ� ��ġ �������� �¿�� �����ϴ� ��ġ ���
        float offset = Mathf.Sin(Time.time * zigzagSpeed) * range;

        // �� ��ġ ����
        transform.position = new Vector3(startPosition.x + offset, transform.position.y, transform.position.z);
    }

    void LookAt2D(Transform targetTransform)
    {
        // ��� ���� ���
        Vector3 direction = targetTransform.position - transform.position;

        // ���� ���
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        angle += 90;

        // Z�� ȸ���� ����
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }
}
