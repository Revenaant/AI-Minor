using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestTree : MonoBehaviour
{
    private BNode root;
    private float distanceToEnemy;
    private float health;
    private float enemyHealth;

    public GameObject moved;
    public Transform p1;
    public Transform p2;

    private TestAgentBT agent;

    //// Use this for initialization
    private void Start()
    {
        agent = GetComponent<TestAgentBT>();
        health = 10;

        root = new RepeatUntilFail(
            new QueryGate (() => agent.health > 300, new Sequence(
                new MoveToNode(p1, moved),
                new MoveToNode(p2, moved)
                ))
            );            

        /// Example of how the Orc BT would be constructed.
        //       root =
        //       new Selector(
        //           new QueryGate(distanceToEnemy > 10,
        //               new Sequence(
        //                   new ActionNode(print("Idle")),
        //                   new ActionNode(print("Patrol")),
        //                   new ActionNode(print("Wander"))
        //               )
        //           ),
        //           new QueryGate(health > 20,
        //               new Sequence(
        //                   new RepeatUntil(distanceToEnemy < 1, new Seek()),
        //                   new ActionNode(print("Attack"))
        //               )
        //           ),
        //           new QueryGate(health >= 100,
        //               // Flee
        //               // Hide
        //           )
        //       );
    }

    // Update is called once per frame
    private void Update () {
        //if(root.NodeStatus == BNodeStatus.Running)
        {
            root.Run();
            health -= 0.1f;
            //Debug.Log("Running " + root.NodeStatus);
        }
	}

    private BNodeStatus bprint()
    {
        Debug.Log("Run");
        return BNodeStatus.Success;
    }

    private BNodeStatus bprint2()
    {
        Debug.Log("Start");
        return BNodeStatus.Success;
    }

    private BNodeStatus bprint3()
    {
        Debug.Log("End");
        return BNodeStatus.Success;
    }
}
