using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WanderState : Super_Passive
{
#if DEBUG
    protected override void Start()
    {
        base.Start();
        button = GameObject.Find("Super_Passive").GetComponent<Buttons>();
        OnStateEnter += button.onActive;
        OnStateExit += button.onPassive;

        button.onActive();
    }
#endif

    [SerializeField] private float _wanderStepDistance = 0.5f;

    public override void Enter(AbstractState<TestAgent> prevState)
    {
        base.Enter(prevState);
        _agent.anim.CrossFade("walk", 0.25f);
        _agent.nav.SetDestination(recalcWander());
    }

    public override void Step()
    {
        base.Step();

        if (!_agent.nav.pathPending && _agent.nav.remainingDistance < 0.1f)
        {
            _agent.nav.SetDestination(recalcWander());
        }
    }

    public override void Exit(AbstractState<TestAgent> newState)
    {
        base.Exit(newState);
    }

    private Vector3 recalcWander()
    {
        Vector3 targetDir = Quaternion.AngleAxis(Random.Range(-60f, 60f), transform.up) * transform.forward;
        targetDir = targetDir.normalized * _wanderStepDistance + transform.position;
        NavMeshHit hit;

        if (NavMesh.Raycast(transform.position, targetDir, out hit, NavMesh.AllAreas))
        {
            targetDir = Quaternion.AngleAxis(Random.Range(120f, 240f), transform.up) * transform.forward;
            targetDir = targetDir.normalized * _wanderStepDistance + transform.position;
        }
        return targetDir;
    }
}
