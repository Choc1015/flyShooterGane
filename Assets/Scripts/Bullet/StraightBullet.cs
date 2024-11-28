using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StraightBullet : Bullet // 불렛을 상속 받음
{
    
    
    //  부모클래에서 Start에서 시작할 추가능력
    public override void AddAbility()
    {
        StartCoroutine(BombBullet());
    }

    // 추가 능력을 작성
    IEnumerator BombBullet()
    {
        yield return new WaitForSeconds(3f);
        Debug.Log("폭발!");
        yield return null;
    }




}
