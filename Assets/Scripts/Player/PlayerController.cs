using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.PackageManager;
using UnityEngine;

// 이 스크립트 사용시 무조건 이것들이 사용되어야 함을 명시
[RequireComponent(typeof(StatInfo), typeof(PlayerInput),typeof(PlayerMovement))]
public class PlayerController : MonoBehaviour, IUpdatable
{
    public bool IsAlive { get; private set; } = true;

    // 움직임 클래스
    private PlayerMovement movement;
    // 정보 클래스
    private StatInfo playerInfo;
    // 플레이 INPUT 클래스
    private PlayerInput input;
    // 처음 위치
    private Vector2 InitPosition;
    // 처음 생명력
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
        // 초기 함수
        Init();
    }

    private void Init()
    {
        // 각각의 클래스를 오브젝에서 찾아 넣어줌
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


        if (Input.anyKey) // 아무키를 눌렀을시
        {
            // 움직임 함수와, 스피트 정보, 인풋 클래스를 가져옴
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
