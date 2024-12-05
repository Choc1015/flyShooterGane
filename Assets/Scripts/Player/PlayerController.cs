using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.PackageManager;
using UnityEngine;

// �� ��ũ��Ʈ ���� ������ �̰͵��� ���Ǿ�� ���� ���
[RequireComponent(typeof(StatInfo), typeof(PlayerInput),typeof(PlayerMovement))]
public class PlayerController : MonoBehaviour, IUpdatable
{
    public bool IsAlive { get; private set; } = true;

    // ������ Ŭ����
    private PlayerMovement movement;
    // ���� Ŭ����
    private StatInfo playerInfo;
    // �÷��� INPUT Ŭ����
    private PlayerInput input;
    // ó�� ��ġ
    private Vector2 InitPosition;
    // ó�� �����
    private int initHealth; 
    private void OnEnable()
    {
        UpdateManager.Instance?.Register(this);
        IsAlive = true;
        Init();
    }

    private void OnDisable()
    {
        IsAlive = false;
        transform.position = InitPosition;
        playerInfo.health = initHealth;
        UpdateManager.Instance?.Unregister(this);
    }

    private void Awake()
    {
        // �ʱ� �Լ�
        Init();
    }

    private void Init()
    {
        // ������ Ŭ������ ���������� ã�� �־���
        movement = GetComponent<PlayerMovement>();
        playerInfo = GetComponent<StatInfo>();
        input = GetComponent<PlayerInput>();
        InitPosition = transform.position;
        initHealth = playerInfo.health;
        GameManager.Instance.playerHp = playerInfo.health;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("EnemyBullet"))
        {
            OnDamaged(collision.gameObject.GetComponent<Bullet>().GetDamage());
            GameManager.Instance.playerHp = playerInfo.health;
            ObjectPoolManager.Instance.DeSpawnToPool(collision.gameObject);
        }
    }

    public void OnUpdate()
    {
        if (GameManager.Instance.IsPause == false)
            return;


        if (Input.anyKey) // �ƹ�Ű�� ��������
        {
            // ������ �Լ���, ����Ʈ ����, ��ǲ Ŭ������ ������
            movement.TransMove(playerInfo.speed, input.AxisInput());
        }
    }

    private void OnDamaged(int DamageValue)
    {
        playerInfo.health -= DamageValue;

        Die();

    }

    private void Die()
    {
        if (playerInfo.health == 0)
        {
            IsAlive = false;
        }
    }

}
