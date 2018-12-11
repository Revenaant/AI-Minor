using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Super_Passive : AbstractState<TestAgent>
{
#if DEBUG
    protected virtual void Awake()
    {
        // Debug
        button = GameObject.Find(this.ToString().TrimEnd(')').Substring(9)).GetComponent<Buttons>();
        OnStateEnter += button.onActive;
        OnStateExit += button.onPassive;
    }
#endif

    // Use this for initialization
    protected virtual void Start()
    {
#if DEBUG
        // Debug
        button.onActive();
#endif
    }

    protected virtual void OnEnable()
    {
        _agent = GetComponent<TestAgent>();
        Debug.Assert(_agent != null, this + ": State is not attached to an object with an agent of type " + typeof(TestAgent) + '!');
    }

    public override void Enter(AbstractState<TestAgent> prevState)
    {
        base.Enter(prevState);
        print("Entering : " + this);

        if (_agent.fsm.GetCurrentState() is Super_Passive && _agent.fsm.GetCurrentState() == this)
            _agent.fsm.ChangeState<IdleState>();
    }

    public override void Step()
    {
        base.Step();

        if (_agent.TargetDistance < 5)
            _agent.fsm.ChangeState<Super_Agressive>();

        if (_agent.health <= 250)
            _agent.fsm.ChangeState<Super_Scared>();
    }

    public override void Exit(AbstractState<TestAgent> newState)
    {
        print("Exiting : " + this);
        base.Exit(newState);
    }
}
