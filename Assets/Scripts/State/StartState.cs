using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartState : State
{
    public override void Enter()
    {
        Debug.Log("���ۿ� ����");
        GameManager.Instance.IsPause = false;
        GameManager.Instance.PopupStarts();
        Time.timeScale = 0f;
        
    }
    public override void Execute()
    {
        Debug.Log("������");
    }
    public override void Exit() => Debug.Log("���ۿ��� ����");
}