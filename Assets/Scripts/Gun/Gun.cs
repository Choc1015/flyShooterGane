using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour,IUpdatable
{
    // �ѿ� �� �Ѿ� ������ �߰�
    public GameObject BulletPrefab;

    // ���� �θ� 
    private string whoChar;

    private void Awake()
    {
        // �ʱ� ���� �Լ�
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
        // ������Ʈ���� �����̽��ٸ� ������ �� �߻� , �ٵ� �̰� �̺�Ʈ�� ���� ����, ��� üũ�ϴ� �� �ʹ� �Ҿ���
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // �߻� �Լ�
            Shooting();
        }
    }

    void Init()
    {
        // ������ �� üũ
        if (BulletPrefab == null)
        {
            Debug.LogError("�Ѿ� �������� ������� �ʾҽ��ϴ�.");
        }

        whoChar = transform.parent.name;

        Debug.Log(whoChar);
    }
    public void Shooting()
    {
        Debug.Log("�߻�");
        // ������Ʈ Ǯ�� Ŭ�������� Ǯ�� ���� * ���� ���� �Լ� �־�� ��.
        GameObject bullet = ObjectPoolManager.Instance.SpawnFromPool(BulletPrefab.name, transform.position );
        // �Ѿ��� ȸ�� ���� ���� ȸ������ ���߱�
        bullet.transform.rotation = transform.rotation;
    }

}
