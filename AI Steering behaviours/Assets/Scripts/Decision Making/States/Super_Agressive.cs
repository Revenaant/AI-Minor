using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Super_Agressive : AbstractState<TestAgent> {

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
        _agent = GetComponent<TestAgent>();
        Debug.Assert(_agent != null, this + ": State is not attached to an object with an agent of type " + typeof(TestAgent) + '!');
    }

    protected virtual void OnEnable()
    {
        // if (_agent.fsm.GetCurrentState() is Super_Agressive && _agent.fsm.GetCurrentState() == this)
        {
            _agent.anim.Play("idleCombat");
            StartCoroutine(Approach());
        }
    }

    public override void Enter(AbstractState<TestAgent> prevState)
    {
        base.Enter(prevState);
        print("Entering : " + this);
    }

    public override void Step()
    {
        base.Step();

        // If too hurt, from any state, go to Flee
        if (_agent.health <= 250) _agent.fsm.ChangeState<FleeState>();
    }

    public override void Exit(AbstractState<TestAgent> newState)
    {
        print("Entering : " + this);
        base.Exit(newState);
    }

    // When entering the state, default to seek
    private IEnumerator Approach()
    {
        yield return new WaitForSeconds(2.5f);
        _agent.fsm.ChangeState<SeekState>();
    }
}
