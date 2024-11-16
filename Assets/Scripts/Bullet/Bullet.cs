using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Bullet : MonoBehaviour
{
    [SerializeField] float BulletSpeed;

    private void Update()
    {
        shootBullet();
    }

    public void shootBullet()
    {
        transform.Translate(Vector2.up * BulletSpeed * Time.deltaTime);
    }

   
}
