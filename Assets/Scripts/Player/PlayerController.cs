using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.PackageManager;
using UnityEngine;

// 이 스크립트 사용시 무조건 이것들이 사용되어야 함을 명시
[RequireComponent(typeof(StatInfo), typeof(PlayerInput),typeof(PlayerMovement))]
public class PlayerController : MonoBehaviour, IUpdatable
{
    // 움직임 클래스
    private PlayerMovement movement;
    // 정보 클래스
    private StatInfo playerInfo;
    // 플레이 INPUT 클래스
    private PlayerInput input;
    private void OnEnable()
    {
        UpdateManager.Instance?.Register(this);
    }

    private void OnDisable()
    {
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
    }

    public void OnUpdate()
    {
        
        if (Input.anyKey) // 아무키를 눌렀을시
        {
            // 움직임 함수와, 스피트 정보, 인풋 클래스를 가져옴
            movement.TransMove(playerInfo.speed, input.AxisInput());
        }
    }

   
        
}
