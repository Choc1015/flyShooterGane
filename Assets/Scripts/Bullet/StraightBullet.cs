using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StraightBullet : Bullet // �ҷ��� ��� ����
{
    
    
    //  �θ�Ŭ������ Start���� ������ �߰��ɷ�
    public override void AddAbility()
    {
        StartCoroutine(BombBullet());
    }

    // �߰� �ɷ��� �ۼ�
    IEnumerator BombBullet()
    {
        yield return new WaitForSeconds(3f);
        Debug.Log("����!");
        yield return null;
    }




}
