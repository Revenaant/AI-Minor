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
    }
#endif

    Vector3 pos;

    public override void Enter(AbstractState<TestAgent> prevState)
    {
        base.Enter(prevState);
        _agent.anim.CrossFade("walk", 0.25f);
        pos = RandomNavSphere(_agent.transform.position, 3, 0);
    }

    public override void Step()
    {
        base.Step();

        if (Vector3.Distance(_agent.transform.position, pos) < 3f)
        {
            _agent.nav.SetDestination(pos);
            pos = RandomNavSphere(_agent.transform.position, 3, 0);
        }
    }

    public override void Exit(AbstractState<TestAgent> newState)
    {
        base.Exit(newState);
    }

    public static Vector3 RandomNavSphere(Vector3 origin, float distance, int layermask)
    {
        Vector3 randomDirection = UnityEngine.Random.insideUnitSphere * distance;

        randomDirection += origin;

        NavMeshHit navHit;

        NavMesh.SamplePosition(randomDirection, out navHit, distance, layermask);

        return navHit.position;
    }
}
