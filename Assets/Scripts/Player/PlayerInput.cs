using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    // 키값으로 가져올것들
    private Vector2 playerInput;


    // X,Y의 1에서 -1사이의 값을 벡터에 저장해서 리턴
    public Vector2 AxisInput()
    {
        float xInput = Input.GetAxisRaw("Horizontal");
        float yInput = Input.GetAxisRaw("Vertical");

        playerInput = new Vector2(xInput, yInput);

        return playerInput;
    }

}
