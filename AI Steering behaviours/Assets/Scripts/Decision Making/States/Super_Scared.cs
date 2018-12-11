using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Super_Scared : AbstractState<TestAgent>
{
#if DEBUG
    protected virtual void Start()
    {
        // Debug
        button = GameObject.Find(this.ToString().TrimEnd(')').Substring(9)).GetComponent<Buttons>();
        OnStateEnter += button.onActive;
        OnStateExit += button.onPassive;

        _agent = GetComponent<TestAgent>();
        Debug.Assert(_agent != null, this + ": State is not attached to an object with an agent of type " + typeof(TestAgent) + '!');
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

        //if (_agent.fsm.GetCurrentState().ToString() == "ORC_PBR (Super_Scared)")
            //_agent.fsm.ChangeState<FleeState>();
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
