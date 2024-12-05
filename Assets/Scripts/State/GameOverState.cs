using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverState : State
{
    public override void Enter()
    {
        GameManager.Instance.PopupGameOver();
        Time.timeScale = 0f;
    }
    public override void Execute() { }
    public override void Exit() { GameManager.Instance.Init(); }
}
