using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeekNode : BNode
{
    private TestAgentBT _agent;
    private Transform _targetValid;

    public SeekNode(TestAgentBT agent, Transform targetPosition)
    {
        _agent = agent;
        _targetValid = targetPosition;
    }

    public override void Init(System.Action action)
    {
        if (action != null) action.Invoke();
    }

    protected override void Enter()
    {
        base.Enter();
        _agent.nav.isStopped = false;
        _agent.anim.CrossFade("run", 0.25f);

        #region debug
        //Debug.Log(_agent.transform.position + " to " + _targetValid.position + "SAY" + nodeStatus);
        #endregion
    }

    public override BNodeStatus Run()
    {
        if (!HasEntered) Enter();
        
        _agent.nav.SetDestination(_targetValid.position);

        #region Debug
        Debug.Log(_agent.transform.position + " to " + _targetValid.position + nodeStatus);
        //t--;
        //if(t <= 0)
        //{
        //    _agent.nav.isStopped = true;
        //    return nodeStatus = BNodeStatus.Success;
        //}
        #endregion

        if (_agent.nav.remainingDistance < 0.6f)
        {
            Debug.Log("Reached");
            _agent.nav.isStopped = true;
            return nodeStatus = BNodeStatus.Running;
            
        }

        Debug.Log(_agent.transform.position + " to " + _targetValid.position);
        //if(nav.hasPath issues) return nodeStatus = BNodeStatus.Failure;

        return nodeStatus = BNodeStatus.Running;
    }
}
