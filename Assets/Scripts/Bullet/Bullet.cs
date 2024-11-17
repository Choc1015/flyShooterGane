using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Bullet : MonoBehaviour, IUpdatable
{
    [SerializeField] float BulletSpeed;

    protected void OnEnable()
    {
        UpdateManager.Instance?.Register(this);
    }

    protected void OnDisable()
    {
        UpdateManager.Instance?.Unregister(this);
    }

    public void shootBullet()
    {
        transform.Translate(Vector2.up * BulletSpeed * Time.deltaTime);
    }

    public void OnUpdate()
    {
        shootBullet();
    }
}
