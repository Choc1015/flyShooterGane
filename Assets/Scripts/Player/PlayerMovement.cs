using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
   // 움직임 함수
    public void TransMove(float speed, Vector2 dir)
    {
        // 인풋에서 키값을 받으면 정규화
        dir = dir.normalized;
        //자기 오브젝트를 특정 방향(dir)과 스피드를 곱하고 델타값을 곱하만큼으로 변경
        gameObject.transform.Translate(dir * speed * Time.deltaTime);
    }
    
}
