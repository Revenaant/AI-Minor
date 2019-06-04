using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackNode : BNode
{
    private TestAgentBT _agent;

    public AttackNode(TestAgentBT agent, object target)
    {
        _agent = agent;
    }

    public override void Init(System.Action action)
    {
        if (action != null) action.Invoke();
    }

    protected override void Enter()
    {
        base.Enter();
        _agent.anim.CrossFade("attacks", 0.25f);
    }

    // Very arbitrary magic number
    float counter = 45;
    public override BNodeStatus Run()
    {
        if (!HasEntered) Enter();

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

        return nodeStatus = BNodeStatus.Running;
    }

}
