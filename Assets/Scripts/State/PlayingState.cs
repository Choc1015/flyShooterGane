using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayingState : State
{
    public override void Enter() => Debug.Log("�÷��̿� ����");
    public override void Execute() => Debug.Log("�÷�����");
    public override void Exit() => Debug.Log("�÷��̿��� ����");
}
