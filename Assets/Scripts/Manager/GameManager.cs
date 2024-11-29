using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>, IUpdatable
{
    private StateMachine stateMachine;

    private void OnEnable()
    {
        UpdateManager.Instance?.Register(this);
    }

    private void OnDisable()
    {
        UpdateManager.Instance?.Unregister(this);
    }

    private void Start()
    {
        stateMachine = GetComponent<StateMachine>();

        // 초기 상태 설정 
        stateMachine.ChangeState(new StartState());
    }

    public void OnUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            stateMachine.ChangeState(new PlayingState());
        }
    }

}
