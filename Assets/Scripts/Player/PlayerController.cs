using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.PackageManager;
using UnityEngine;

// �� ��ũ��Ʈ ���� ������ �̰͵��� ���Ǿ�� ���� ���
[RequireComponent(typeof(StatInfo), typeof(PlayerInput),typeof(PlayerMovement))]
public class PlayerController : MonoBehaviour, IUpdatable
{
    // ������ Ŭ����
    private PlayerMovement movement;
    // ���� Ŭ����
    private StatInfo playerInfo;
    // �÷��� INPUT Ŭ����
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
        // �ʱ� �Լ�
        Init();
    }

    private void Init()
    {
        // ������ Ŭ������ ���������� ã�� �־���
        movement = GetComponent<PlayerMovement>();
        playerInfo = GetComponent<StatInfo>();
        input = GetComponent<PlayerInput>();
    }

    public void OnUpdate()
    {
        
        if (Input.anyKey) // �ƹ�Ű�� ��������
        {
            // ������ �Լ���, ����Ʈ ����, ��ǲ Ŭ������ ������
            movement.TransMove(playerInfo.speed, input.AxisInput());
        }
    }

   
        
}
