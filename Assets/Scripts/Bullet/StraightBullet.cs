using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StraightBullet : Bullet
{
    private new void OnEnable()
    {
        base.OnEnable();
        StartCoroutine(BombBullet());
    }

    IEnumerator BombBullet()
    {
        yield return new WaitForSeconds(3f);
        Debug.Log("����!");
        yield return null;
    }

}
