using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : Super_Agressive
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
        //_agent.anim.Play("Attacks");
    }

    public override void Step()
    {
        base.Step();

        if (_agent.anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 1)
        {
            //_agent.anim.Play("Attacks");
            //_agent.anim.SetFloat("Blend", Random.Range(0, 0.571f));
        }
    }

    public override void Exit(AbstractState<TestAgent> newState)
    {
        base.Exit(newState);
    }
}
