using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolState : Super_Passive
{
    public Transform waypoints;
    private Transform currentWaypoint;

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

    public override void Enter(AbstractState<TestAgent> prevState)
    {
        base.Enter(prevState);
        _agent.anim.CrossFade("walk", 0.25f);


        if (currentWaypoint == null)
            currentWaypoint = GetNextWaypoint();

        if (currentWaypoint != null)
            _agent.nav.SetDestination(currentWaypoint.position);
    }

    const int n = 600;
    int time = n;
    public override void Step()
    {
        base.Step();

        if (currentWaypoint == null)
        {
            currentWaypoint = GetNextWaypoint();

            if (currentWaypoint == null)
                return;

        }

        if (Vector3.Distance(_agent.transform.position, currentWaypoint.position) < 3f)
        {
            currentWaypoint = null;
        }

        if (currentWaypoint != null)
            _agent.nav.SetDestination(currentWaypoint.position);

        time--;
        // When enough time has passed go to passive state
        if (time <= 0 || Input.GetKeyDown(KeyCode.F))
        {
            _agent.fsm.ChangeState<WanderState>();
            time = n;
        }
    }

    public override void Exit(AbstractState<TestAgent> newState)
    {
        base.Exit(newState);
    }

    public Transform GetNextWaypoint()
    {
        //Transform previousInList = null;
        // Loops through all the child objects
        if (waypoints.childCount <= 0) return waypoints.transform;
        return waypoints.GetChild(Random.Range(0, waypoints.childCount));
    }
}
