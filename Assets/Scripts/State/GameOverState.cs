using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverState : State
{
    public override void Enter() => Debug.Log("���ῡ ����");
    public override void Execute() => Debug.Log("������");
    public override void Exit() => Debug.Log("���ῡ�� ����");
}
