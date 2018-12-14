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

    int time = 0;
    public override void Step()
    {
        base.Step();

        // When cover has reached start healing
        if (_agent.nav.remainingDistance < 0.1f)
        {
            _agent.anim.CrossFade("idleBlock", 0.25f);
            _agent.TakeDamage(-1f);
        }

        time++;

        // When enough time has passed go to passive state
        if (time > 150 && _agent.health >= _agent.maxHealth || Input.GetKeyDown(KeyCode.F))
        {
            _agent.fsm.ChangeState<Super_Passive>();
            time = 0;
        }
    }

    public override void Exit(AbstractState<TestAgent> newState)
    {
        base.Exit(newState);
    }


}
