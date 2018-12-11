using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractState<T> : MonoBehaviour
    where T : AbstractAgent
{

    public System.Action OnStateEnter;
    public System.Action OnStateExit;

    protected TestAgent _agent;
    protected Buttons button;

    public virtual void Enter(AbstractState<T> prevState)
    {
        this.enabled = true;
        if (OnStateEnter != null) OnStateEnter.Invoke();
    }

    public virtual void Step()
    {
        // Update
    }

    public virtual void Exit(AbstractState<T> newState)
    {
        // Debug
        //Tracer.Trace(button.transform.position, newState.button.transform.position);

        if (OnStateExit != null) OnStateExit.Invoke();
        this.enabled = false;
    }
}
