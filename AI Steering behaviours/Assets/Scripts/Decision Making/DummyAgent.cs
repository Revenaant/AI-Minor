using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DummyAgent : AbstractAgent
{
    [HideInInspector] public HFSM<DummyAgent> fsm;

    // Use this for initialization
    protected virtual void Start()
    {
        // Creates an HFSM and sets the starting state
        fsm = new HFSM<DummyAgent>(this);
        fsm.ChangeState<DummyState>();
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        // Updates the state
        fsm.Step();
    }
}

public class DummyState : AbstractState<DummyAgent>
{

}
