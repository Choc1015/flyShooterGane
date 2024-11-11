using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
   
    public void TransMove(float speed, Vector2 dir)
    {
        dir = dir.normalized;
        gameObject.transform.Translate(dir * speed * Time.deltaTime);
    }
    
}
