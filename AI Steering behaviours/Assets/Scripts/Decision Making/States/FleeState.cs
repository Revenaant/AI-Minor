using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FleeState : Super_Scared
{
#if DEBUG
    protected override void Start()
    {
        base.Start();
        button = GameObject.Find("Super_Scared").GetComponent<Buttons>();
        OnStateEnter += button.onActive;
        OnStateExit += button.onPassive;
    }
#endif

    public override void Enter(AbstractState<TestAgent> prevState)
    {
        base.Enter(prevState);
        _agent.nav.SetDestination(GameObject.Find("FleeSpot" + Random.Range(1, 4)).transform.position);
        _agent.anim.Play("run");
    }

    public override void Step()
    {
        base.Step();

        // If it flees "far enough" look for hiding
        if (_agent.nav.remainingDistance < 0.1f)
            _agent.fsm.ChangeState<HideState>();
    }

    public override void Exit(AbstractState<TestAgent> newState)
    {
        base.Exit(newState);
    }
}
