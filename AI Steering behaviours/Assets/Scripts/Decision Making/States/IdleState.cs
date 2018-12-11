﻿using System.Collections;
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

    public override void Step()
    {
        base.Step();

        // If it flees "far enough" look for hiding
        if (_agent.nav.remainingDistance < 1)
            _agent.fsm.ChangeState<FleeState>();
    }

    public override void Exit(AbstractState<TestAgent> newState)
    {
        base.Exit(newState);
    }
}
