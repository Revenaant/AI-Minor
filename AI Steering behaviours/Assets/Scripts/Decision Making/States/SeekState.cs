using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeekState : Super_Agressive
{
#if DEBUG
    protected override void Start()
    {
        base.Start();
        button = GameObject.Find("Super_Agressive").GetComponent<Buttons>();
        OnStateEnter += button.onActive;
        OnStateExit += button.onPassive;

        button.onActive();
    }
#endif

    public override void Enter(AbstractState<TestAgent> prevState)
    {
        base.Enter(prevState);
        _agent.nav.isStopped = false;
        _agent.nav.SetDestination(_agent.TargetValid);
        _agent.anim.CrossFade("run", 0.25f);
    }

    public override void Step()
    {
        base.Step();

        // If it flees "far enough" look for hiding
        if (_agent.nav.remainingDistance < 0.6f)
        {
            _agent.fsm.ChangeState<AttackState>();
            _agent.nav.isStopped = true;
        }
    }

    public override void Exit(AbstractState<TestAgent> newState)
    {
        base.Exit(newState);
    }
}
