using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartState : State
{
    public override void Enter() => Debug.Log("���ۿ� ����");
    public override void Execute() => Debug.Log("������");
    public override void Exit() => Debug.Log("���ۿ��� ����");
}