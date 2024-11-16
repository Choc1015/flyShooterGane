using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StraightBullet : Bullet
{
    private void Start()
    {
        StartCoroutine(BombBullet());
    }

    IEnumerator BombBullet()
    {
        yield return new WaitForSeconds(3f);
        Debug.Log("Æø¹ß!");
        yield return null;
    }

}
