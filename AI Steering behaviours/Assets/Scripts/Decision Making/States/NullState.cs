using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NullState : AbstractState<TestAgent>
{
#if DEBUG
    protected virtual void Start()
    {
        // Debug
        button = GameObject.Find(this.ToString().TrimEnd(')').Substring(9)).GetComponent<Buttons>();
        OnStateEnter += button.onActive;
        OnStateExit += button.onPassive;

        button.onActive();
    }
#endif

    protected virtual void OnEnable()
    {
        _agent = GetComponent<TestAgent>();
        Debug.Assert(_agent != null, this + ": State is not attached to an object with an agent of type " + typeof(TestAgent) + '!');
    }

    public override void Enter(AbstractState<TestAgent> prevState)
    {
        base.Enter(prevState);
        print("Entering : " + this);
    }

    public override void Step()
    {
        base.Step();
    }

    public override void Exit(AbstractState<TestAgent> newState)
    {
        print("Exiting : " + this);
        base.Exit(newState);
    }
}
