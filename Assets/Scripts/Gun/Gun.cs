using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour,IUpdatable
{
    public GameObject BulletPrefab;

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

        
        GameObject bullet = ObjectPoolManager.Instance.SpawnFromPool(BulletPrefab.name, gameObject);
        bullet.transform.rotation = transform.rotation;
    }

}
