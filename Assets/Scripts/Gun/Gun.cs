using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EWeaponOwner
{
    Player,
    Enemy
}

public class Gun : MonoBehaviour, IUpdatable
{
    // �ѿ� �� �Ѿ� ������ �߰�
    public GameObject BulletPrefab;
    // �� ����
    public EWeaponOwner Owner;

    public float DelayShootAi = 1f;

    private void Awake()
    {
        // �ʱ� ���� �Լ�
        Init();
    }
    private void Start()
    {
        if (Owner == EWeaponOwner.Enemy)
        {
            StartCoroutine(AiEnemyShooting());
        }
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
        if (Input.GetKeyDown(KeyCode.Space) && Owner == EWeaponOwner.Player)
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

    }

    IEnumerator AiEnemyShooting()
    {
        while (true) 
        {
            yield return new WaitForSeconds(DelayShootAi);
            Shooting();
        };
    }

    public void Shooting()
    {
        Debug.Log("�߻�");
        // ������Ʈ Ǯ�� Ŭ�������� Ǯ�� ���� * ���� ���� �Լ� �־�� ��.
        GameObject bullet = ObjectPoolManager.Instance.SpawnFromPool(BulletPrefab.name, transform.position);
        // �Ѿ��� ȸ�� ���� ���� ȸ������ ���߱�
        bullet.transform.rotation = transform.rotation;
    }

}
