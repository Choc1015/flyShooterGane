using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour,IInput
{
    public GameObject Bullet;

    public void GetInput()
    {
        if (Input.GetKeyUp(KeyCode.Space))
        {

        }
    }

    public void Shooting()
    {
        
            Instantiate(Bullet); //������Ʈ Ǯ���� ����
        
    }
}
