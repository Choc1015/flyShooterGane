using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayingState : State
{
    public override void Enter() => Debug.Log("플레이에 진입");
    public override void Execute() => Debug.Log("플레이중");
    public override void Exit() => Debug.Log("플레이에서 나감");
}
