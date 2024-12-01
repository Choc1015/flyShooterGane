using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartState : State
{
    public override void Enter()
    {
        Debug.Log("시작에 진입");
        GameManager.Instance.IsPause = false;
        GameManager.Instance.PopupStarts();
        Time.timeScale = 0f;
        
    }
    public override void Execute()
    {
        Debug.Log("시작중");
    }
    public override void Exit() => Debug.Log("시작에서 나감");
}