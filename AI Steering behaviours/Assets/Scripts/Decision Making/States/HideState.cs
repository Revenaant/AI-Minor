using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideState : Super_Scared
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
        _agent.nav.SetDestination(GameObject.Find("HideSpot").transform.position);
    }

    const int n = 1000;
    int time = n;
    public override void Step()
    {
        base.Step();

        // When cover has reached start healing
        if (_agent.nav.remainingDistance < 0.1f)
        {
            _agent.anim.CrossFade("idleBlock", 0.25f);
            time--;
            _agent.TakeDamage(-1f);
        }

        // When enough time has passed go to passive state
        if (time <= 0 || Input.GetKeyDown(KeyCode.F))
        {
            _agent.fsm.ChangeState<Super_Passive>();
            time = n;
        }
    }

    public override void Exit(AbstractState<TestAgent> newState)
    {
        base.Exit(newState);
    }


}
