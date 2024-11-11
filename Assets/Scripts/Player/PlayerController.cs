using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.PackageManager;
using UnityEngine;

[RequireComponent(typeof(PlayerInfo), typeof(PlayerInput),typeof(PlayerMovement))]
public class PlayerController : MonoBehaviour, IUpdatable
{
    private PlayerMovement _movement;
    private PlayerInfo _info;
    private PlayerInput _input;
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
        Init();
    }

    private void Init()
    {
        _movement = GetComponent<PlayerMovement>();
        _info = GetComponent<PlayerInfo>();
        _input = GetComponent<PlayerInput>();
    }

    public void OnUpdate()
    {
        
        if (Input.anyKey)
        {
            _movement.TransMove(_info.speed, _input.AxisInput());
        }
    }

   
        
}
