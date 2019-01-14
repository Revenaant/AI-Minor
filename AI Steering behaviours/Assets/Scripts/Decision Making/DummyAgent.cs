using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DummyAgent : AbstractAgent
{
    [HideInInspector] public HFSM<DummyAgent> fsm;
    public Collider axe;

    // Use this for initialization
    protected virtual void Start()
    {
        // Creates an HFSM and sets the starting state
        //fsm = new HFSM<DummyAgent>(this);
        ////fsm.ChangeState<DummyState>();

        Animator anim = GetComponent<Animator>();
        anim.Play("2HitComboB");
        axe.enabled = false;
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        // Updates the state
        //fsm.Step();
    }

    public void hit()
    {
        axe.enabled = true;
        StartCoroutine(WaitOneFrame(() => axe.enabled = false));
    }

    private IEnumerator WaitOneFrame(System.Action action)
    {
        yield return new WaitForSeconds(0.1f);
        action.Invoke();
    }
}

public class DummyState : AbstractState<DummyAgent>
{

}
