using System.Collections.Generic;
using UnityEngine;

public class UpdateManager : Singleton<UpdateManager>
{
    private List<IUpdatable> updatableObjects = new List<IUpdatable>();
    private List<IFixedUpdatable> fixedUpdatableObjects = new List<IFixedUpdatable>();

    // 매 프레임마다 모든 IUpdatable 객체의 OnUpdate 메서드를 호출
    void Update()
    {
        foreach (var updatable in updatableObjects)
        {
            if (updatable == null)
                continue;

            updatable.OnUpdate();
        }
    }

    // 물리 프레임마다 모든 IFixedUpdatable 객체의 OnFixedUpdate 메서드를 호출
    void FixedUpdate()
    {
        foreach (var fixedUpdatable in fixedUpdatableObjects)
        {
            if (fixedUpdatable == null)
                continue;

            fixedUpdatable.OnFixedUpdate();
        }
    }

    // Update를 등록하는 메서드
    public void Register(IUpdatable updatable)
    {
        if (!updatableObjects.Contains(updatable))
        {
            updatableObjects.Add(updatable);
        }
    }

    // Update를 등록 해제하는 메서드
    public void Unregister(IUpdatable updatable)
    {
        if (updatableObjects.Contains(updatable))
        {
            updatableObjects.Remove(updatable);
        }
    }

    // FixedUpdate를 등록하는 메서드
    public void Register(IFixedUpdatable fixedUpdatable)
    {
        if (!fixedUpdatableObjects.Contains(fixedUpdatable))
        {
            fixedUpdatableObjects.Add(fixedUpdatable);
        }
    }

    // FixedUpdate를 등록 해제하는 메서드
    public void Unregister(IFixedUpdatable fixedUpdatable)
    {
        if (fixedUpdatableObjects.Contains(fixedUpdatable))
        {
            fixedUpdatableObjects.Remove(fixedUpdatable);
        }
    }
}