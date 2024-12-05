using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ETarget // Enum���� Ÿ�� ����
{
    Player, // �÷��̾ ��ǥ�� �Ѵ�.
    Enemy // ���� ��ǥ�� �Ѵ�.
}

/// <summary>
///  �Ѿ� �߻� ��ũ��Ʈ ��� �Ѿ��� �� ��ũ��Ʈ�� ��ӹ޴´�.
/// </summary>
public abstract class Bullet : MonoBehaviour, IUpdatable 
{
    // �Ѿ� �ӵ�
    [SerializeField] private float bulletSpeed;
    // �Ѿ� ������
    private int bulletDamage = 1;
    // �Ѿ� ���� �ð� ( �ش� �ð� ���� �ڵ� ��Ȱ��ȭ ) 
    private float bulletDurationTime = 5;
    // ���� �� ( �� ������ �Ѿ�� ��Ȱ��ȭ ) 
    const int endX = 11;
    const int endY = 20;

    /// <summary>
    /// Ȱ��ȭ �� �۵��ϴ� �޼���
    /// </summary>
    private void OnEnable()
    {
        UpdateManager.Instance?.Register(this); // ������Ʈ �Ŵ����� ������Ʈ�� ���ٰ� �˸��� ���� ���� 
        AddAbility(); // �߰� �ɷ� �κ� ���� 
        StartCoroutine(ClearBullet()); // �Ѿ� ���� �ð� ���� ���� �Ǵ� ��� �ڷ�ƾ���� ����
    }

    /// <summary>
    ///  ��Ȱ��ȭ �� �۵��ϴ� �޼��� 
    /// </summary>
    private void OnDisable()
    {
        UpdateManager.Instance?.Unregister(this); // ��Ȱ��ȭ �� ������Ʈ �Ŵ������� ���´ٰ� ���� ���� 
    }

    /// <summary>
    /// ������Ʈ �Ŵ������� ������Ʈ�� �� ���� �� ���̱⿡ ��ǻ� ���⿡ ���� ����� ������Ʈ�� ���� ���
    /// </summary>
    public void OnUpdate() 
    {
        // �������� ������ �ڵ����� ������ ��.
        ShootBullet();
        // ���� ������ ���� �� �ڵ� ��Ȱ��ȭ �Լ�
        DespawnOnExit();
    }

    // �ڷ�ƾ���� �߰��ɷ��� Ȯ���� ��
    public virtual void AddAbility() { }

    // �Ѿ� ������ ����
    public void ShootBullet()
    {
        transform.Translate(Vector2.up * bulletSpeed * Time.deltaTime); // �ӵ��� ��ŸŸ�� �׸��� ���� ���� ������ ��
    }

    // Ư�� ������ ������ ����
    public void DespawnOnExit()
    {
        if (!IsInRange(transform.position.x,-endX,endX) || !IsInRange(transform.position.y, -endY, endY)) // ���� x�� y�� �ϳ��� ���� �ٱ��� �� �� ���� ������.
        {
            ObjectPoolManager.Instance.DeSpawnToPool(gameObject); // �׸��� ��Ȱ��ȭ �����մϴ�.
        }
    }

    // ���� �ȿ� �ִ� �� Ȯ���ϴ� �޼���
    public bool IsInRange(float value, float min, float max)
    {
        return value >= min && value <= max; // �ּҰ��� �ִ밪 ���̿� �ִ� �� ������ �Ǻ��ϰ� ����
    }

    //������ ���� �Լ�
    public void SetDamage(int value)
    {
        bulletDamage = value; // �ش� �������� ����
    }

    // ������ �о����
    public int GetDamage() 
    {
        return bulletDamage; // ���� ������ �� ����
    }

    /// <summary>
    /// Ŭ���� �ڷ�ƾ 
    /// </summary>
    /// <returns></returns>
    IEnumerator ClearBullet() 
    {
        
        yield return new WaitForSeconds(bulletDurationTime); // �Ѿ� ���� �ð� ���� ��ٸ���
        ObjectPoolManager.Instance.DeSpawnToPool(gameObject); // �׸��� ���� �ڱ� ������Ʈ�� ����
        yield return null; // ���� ��
    }

}
