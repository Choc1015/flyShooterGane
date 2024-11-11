using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour,IUpdatable
{
    public GameObject Bullet;

    private void OnEnable()
    {
        UpdateManager.Instance?.Register(this);
    }

    private void OnDisable()
    {
        UpdateManager.Instance?.Unregister(this);
    }

    public void OnUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Shooting();
        }
    }

    public void Shooting()
    {
        Debug.Log("�߻�");

        ObjectPoolManager.Instance.SpawnFromPool(Bullet.name,this.gameObject);

    }

}
