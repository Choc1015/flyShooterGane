using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
   // ������ �Լ�
    public void TransMove(float speed, Vector2 dir)
    {
        // ��ǲ���� Ű���� ������ ����ȭ
        dir = dir.normalized;
        //�ڱ� ������Ʈ�� Ư�� ����(dir)�� ���ǵ带 ���ϰ� ��Ÿ���� ���ϸ�ŭ���� ����
        gameObject.transform.Translate(dir * speed * Time.deltaTime);
    }
    
}
