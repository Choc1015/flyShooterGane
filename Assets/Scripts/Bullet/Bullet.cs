using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Bullet : MonoBehaviour, IUpdatable
{
    // 총알 속도
    [SerializeField] private float bulletSpeed;
    // 총알 데미지
    private int bulletDamage = 1;
    
    // 범위 값
    const int endX = 6;
    const int endY = 10;

    public void OnEnable()
    {
        UpdateManager.Instance?.Register(this);
    }

    protected void OnDisable()
    {
        UpdateManager.Instance?.Unregister(this);
    }
    private void Start()
    {


        // 코루틴으로 추가능력을 확장할 시
        AddAbility(); 
    }

    public void OnUpdate()
    {
        // 프리팹이 생성시 자동으로 앞으로 감.
        ShootBullet();
        DespawnOnExit();
    }

    // 코루틴으로 추가능력을 확장할 시
    public virtual void AddAbility()
    {

    }

    // 총알 움직임 로직
    public void ShootBullet()
    {
        transform.Translate(Vector2.up * bulletSpeed * Time.deltaTime);
    }

    // 특정 범위를 나갈시 디스폰
    public void DespawnOnExit()
    {
        if (!IsInRange(transform.position.x,-endX,endX) || !IsInRange(transform.position.y, -endY, endY))
        {
            ObjectPoolManager.Instance.DeSpawnToPool(gameObject);
        }
    }

    // 범위 안에 있는 지 확인하는 메서드
    public bool IsInRange(float value, float min, float max)
    {
        return value >= min && value <= max;
    }

    // 자식 클래스에서 데미지 변경 가능
    public void SetDamage(int value)
    {
        bulletDamage = value;
    }

    // 데미지 읽어오기
    public int GetDamage()
    {
        return bulletDamage;
    }

}
