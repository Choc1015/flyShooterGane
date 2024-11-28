using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    // Ű������ �����ð͵�
    private Vector2 playerInput;


    // X,Y�� 1���� -1������ ���� ���Ϳ� �����ؼ� ����
    public Vector2 AxisInput()
    {
        float xInput = Input.GetAxisRaw("Horizontal");
        float yInput = Input.GetAxisRaw("Vertical");

        playerInput = new Vector2(xInput, yInput);

        return playerInput;
    }

}
