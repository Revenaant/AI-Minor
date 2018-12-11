using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : Super_Agressive
{
#if DEBUG
    protected override void Start()
    {
        base.Start();
        button = GameObject.Find("Super_Agressive").GetComponent<Buttons>();
        OnStateEnter += button.onActive;
        OnStateExit += button.onPassive;
    }
#endif

    public override void Enter(AbstractState<TestAgent> prevState)
    {
        base.Enter(prevState);
        _agent.anim.CrossFade("attacks", 0.25f);
    }

    float counter = 45;
    public override void Step()
    {
        base.Step();

        float length = _agent.anim.GetCurrentAnimatorClipInfo(0)[0].clip.length;
        float current = _agent.anim.GetCurrentAnimatorStateInfo(0).length;

        if (current > length)
        {
            _agent.anim.CrossFade("attacks", 0.25f);
        }

        counter--;
        if (counter <= 0)
        {
            counter = 45;
            _agent.anim.SetFloat("attack", (float)System.Math.Round(Random.Range(0.00f, 0.571f), 3));
        }

    }

    public override void Exit(AbstractState<TestAgent> newState)
    {
        base.Exit(newState);
    }
}
