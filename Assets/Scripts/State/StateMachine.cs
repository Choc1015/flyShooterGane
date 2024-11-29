using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine : MonoBehaviour,IUpdatable
{
    private State currentSate;

    private void OnEnable()
    {
        UpdateManager.Instance?.Register(this);
    }

    private void OnDisable()
    {
        UpdateManager.Instance?.Unregister(this);
    }

    public void ChangeState(State newState)
    {
        if(currentSate != null)
        {
            currentSate.Exit();
        }

        currentSate = newState;
        currentSate.Enter();
    }

    public void OnUpdate()
    {
        if (currentSate != null) 
        {
            currentSate.Execute();
        }
    }
}
