using UnityEngine;

public class UpdateManager : Singleton<UpdateManager>
{
    // Update와 FixedUpdate를 위한 이벤트 정의
    public event System.Action OnUpdateEvent;
    public event System.Action OnFixedUpdateEvent;

    // 매 프레임마다 Update 이벤트 호출
    void Update()
    {
        OnUpdateEvent?.Invoke();
    }

    // 물리 프레임마다 FixedUpdate 이벤트 호출
    void FixedUpdate()
    {
        OnFixedUpdateEvent?.Invoke();
    }

    // Update 등록 메서드
    public void Register(IUpdatable updatable)
    {
        OnUpdateEvent += updatable.OnUpdate;
    }

    // Update 등록 해제 메서드
    public void Unregister(IUpdatable updatable)
    {
        OnUpdateEvent -= updatable.OnUpdate;
    }

    // FixedUpdate 등록 메서드
    public void Register(IFixedUpdatable fixedUpdatable)
    {
        OnFixedUpdateEvent += fixedUpdatable.OnFixedUpdate;
    }

    // FixedUpdate 등록 해제 메서드
    public void Unregister(IFixedUpdatable fixedUpdatable)
    {
        OnFixedUpdateEvent -= fixedUpdatable.OnFixedUpdate;
    }
}
