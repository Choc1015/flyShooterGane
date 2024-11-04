using System.Collections.Generic;
using UnityEngine;

public class UpdateManager : Singleton<UpdateManager>
{
    private List<IUpdatable> updatableObjects = new List<IUpdatable>();
    private List<IFixedUpdatable> fixedUpdatableObjects = new List<IFixedUpdatable>();

    // �� �����Ӹ��� ��� IUpdatable ��ü�� OnUpdate �޼��带 ȣ��
    void Update()
    {
        foreach (var updatable in updatableObjects)
        {
            if (updatable == null)
                continue;

            updatable.OnUpdate();
        }
    }

    // ���� �����Ӹ��� ��� IFixedUpdatable ��ü�� OnFixedUpdate �޼��带 ȣ��
    void FixedUpdate()
    {
        foreach (var fixedUpdatable in fixedUpdatableObjects)
        {
            if (fixedUpdatable == null)
                continue;

            fixedUpdatable.OnFixedUpdate();
        }
    }

    // Update�� ����ϴ� �޼���
    public void Register(IUpdatable updatable)
    {
        if (!updatableObjects.Contains(updatable))
        {
            updatableObjects.Add(updatable);
        }
    }

    // Update�� ��� �����ϴ� �޼���
    public void Unregister(IUpdatable updatable)
    {
        if (updatableObjects.Contains(updatable))
        {
            updatableObjects.Remove(updatable);
        }
    }

    // FixedUpdate�� ����ϴ� �޼���
    public void Register(IFixedUpdatable fixedUpdatable)
    {
        if (!fixedUpdatableObjects.Contains(fixedUpdatable))
        {
            fixedUpdatableObjects.Add(fixedUpdatable);
        }
    }

    // FixedUpdate�� ��� �����ϴ� �޼���
    public void Unregister(IFixedUpdatable fixedUpdatable)
    {
        if (fixedUpdatableObjects.Contains(fixedUpdatable))
        {
            fixedUpdatableObjects.Remove(fixedUpdatable);
        }
    }
}