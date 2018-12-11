using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : Super_Passive
{
#if DEBUG
    protected override void Start()
    {
        base.Start();
        button = GameObject.Find("Super_Passive").GetComponent<Buttons>();
        OnStateEnter += button.onActive;
        OnStateExit += button.onPassive;
    }
#endif

    public override void Enter(AbstractState<TestAgent> prevState)
    {
        base.Enter(prevState);
        _agent.anim.CrossFade("idleNormal", 0.25f);
    }

    const int n = 300;
    int time = n;
    public override void Step()
    {
        base.Step();

        // When enough time has passed go to passive state
        if (time <= 0 || Input.GetKeyDown(KeyCode.F))
        {
            _agent.fsm.ChangeState<PatrolState>();
            time = n;
        }
    }

    public override void Exit(AbstractState<TestAgent> newState)
    {
        base.Exit(newState);
    }
}
