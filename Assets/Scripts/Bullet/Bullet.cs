using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ETarget // Enum으로 타겟 설정
{
    Player, // 플레이어를 목표로 한다.
    Enemy // 적을 목표로 한다.
}

/// <summary>
///  총알 추상 스크립트 모든 총알은 이 스크립트를 상속받는다.
/// </summary>
public abstract class Bullet : MonoBehaviour, IUpdatable 
{
    // 총알 속도
    [SerializeField] private float bulletSpeed;
    // 총알 데미지
    private int bulletDamage = 1;
    // 총알 지속 시간 ( 해당 시간 이후 자동 비활성화 ) 
    private float bulletDurationTime = 5;
    // 범위 값 ( 이 범위를 넘어가면 비활성화 ) 
    const int endX = 11;
    const int endY = 20;

    /// <summary>
    /// 활성화 시 작동하는 메서드
    /// </summary>
    private void OnEnable()
    {
        UpdateManager.Instance?.Register(this); // 업데이트 매니저에 업데이트를 쓴다고 알리기 위해 가입 
        AddAbility(); // 추가 능력 부분 실행 
        StartCoroutine(ClearBullet()); // 총알 지속 시간 이후 삭제 되는 기능 코루틴으로 시작
    }

    /// <summary>
    ///  비활성화 시 작동하는 메서드 
    /// </summary>
    private void OnDisable()
    {
        UpdateManager.Instance?.Unregister(this); // 비활성화 시 업데이트 매니저에서 나온다고 가입 해제 
    }

    /// <summary>
    /// 업데이트 매니저에서 업데이트를 한 번에 쓸 것이기에 사실상 여기에 쓰는 기능은 업데이트와 같은 기능
    /// </summary>
    public void OnUpdate() 
    {
        // 프리팹이 생성시 자동으로 앞으로 감.
        ShootBullet();
        // 범위 밖으로 나갈 시 자동 비활성화 함수
        DespawnOnExit();
    }

    // 코루틴으로 추가능력을 확장할 시
    public virtual void AddAbility() { }

    // 총알 움직임 로직
    public void ShootBullet()
    {
        transform.Translate(Vector2.up * bulletSpeed * Time.deltaTime); // 속도와 델타타입 그리고 업을 곱해 앞으로 감
    }

    // 특정 범위를 나갈시 디스폰
    public void DespawnOnExit()
    {
        if (!IsInRange(transform.position.x,-endX,endX) || !IsInRange(transform.position.y, -endY, endY)) // 만약 x값 y값 하나라도 범위 바깥일 시 불 값을 내보냄.
        {
            ObjectPoolManager.Instance.DeSpawnToPool(gameObject); // 그리고 비활성화 디스폰합니다.
        }
    }

    // 범위 안에 있는 지 확인하는 메서드
    public bool IsInRange(float value, float min, float max)
    {
        return value >= min && value <= max; // 최소값과 최대값 사이에 있는 참 거짓을 판별하고 리턴
    }

    //데미지 변경 함수
    public void SetDamage(int value)
    {
        bulletDamage = value; // 해당 데미지로 저장
    }

    // 데미지 읽어오기
    public int GetDamage() 
    {
        return bulletDamage; // 현재 데미지 값 배출
    }

    /// <summary>
    /// 클리어 코루틴 
    /// </summary>
    /// <returns></returns>
    IEnumerator ClearBullet() 
    {
        
        yield return new WaitForSeconds(bulletDurationTime); // 총알 지속 시간 동안 기다리기
        ObjectPoolManager.Instance.DeSpawnToPool(gameObject); // 그리고 현재 자기 오브젝트를 디스폰
        yield return null; // 리턴 널
    }

}
