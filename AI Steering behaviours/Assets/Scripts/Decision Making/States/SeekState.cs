using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeekState : Super_Agressive
{
#if DEBUG
    protected override void Awake()
    {
        base.Awake();
        button = GameObject.Find("Super_Agressive").GetComponent<Buttons>();
        OnStateEnter += button.onActive;
        OnStateExit += button.onPassive;
    }
#endif

    // Use this for initialization
    protected override void Start()
    {
        base.Start();
    }

    protected override void OnEnable() { }

    public override void Enter(AbstractState<TestAgent> prevState)
    {
        base.Enter(prevState);
        _agent.nav.SetDestination(_agent.TargetValid);
        _agent.anim.Play("run");
    }

    public override void Step()
    {
        base.Step();

        // If it flees "far enough" look for hiding
        if (_agent.nav.remainingDistance < 0.1f)
            _agent.fsm.ChangeState<AttackState>();
    }

    public override void Exit(AbstractState<TestAgent> newState)
    {
        base.Exit(newState);
    }
}
