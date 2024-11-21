using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour,IUpdatable
{
    public GameObject BulletPrefab;

    private void Awake()
    {
        Init();
    }

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

    void Init()
    {
        if(BulletPrefab == null)
        {
            Debug.LogError("총알 프리팹이 적용되지 않았습니다.");
        }
    }
    public void Shooting()
    {
        Debug.Log("발사");

        
        GameObject bullet = ObjectPoolManager.Instance.SpawnFromPool(BulletPrefab.name, gameObject.transform);
        bullet.transform.rotation = transform.rotation;
    }

}
