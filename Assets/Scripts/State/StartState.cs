using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartState : State
{
    public override void Enter()
    {
        GameManager.Instance.IsPause = false;
        GameManager.Instance.PopupStarts();
        Time.timeScale = 0f;

    }
    public override void Execute() { }
    public override void Exit() { }
}