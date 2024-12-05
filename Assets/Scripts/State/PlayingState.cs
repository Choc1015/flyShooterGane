using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;

public class PlayingState : State
{
    public override void Enter()
    {
        Time.timeScale = 1f;
        GameManager.Instance.IsPause = true;
        GameManager.Instance.SpawnEnemy();
    }
    public override void Execute() 
    {
        GameManager.Instance.UpdateGameState();
    }
    public override void Exit()
    {
        GameManager.Instance.Stage++;
    }
}
