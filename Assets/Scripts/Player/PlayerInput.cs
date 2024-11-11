using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{

    private Vector2 playerInput;

    public Vector2 AxisInput()
    {
        float xInput = Input.GetAxisRaw("Horizontal");
        float yInput = Input.GetAxisRaw("Vertical");
            
        playerInput = new Vector2(xInput, yInput);

        return playerInput;
    }
}
