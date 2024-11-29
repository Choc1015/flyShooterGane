using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverState : State
{
    public override void Enter() => Debug.Log("종료에 진입");
    public override void Execute() => Debug.Log("종료중");
    public override void Exit() => Debug.Log("종료에서 나감");
}
