using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrcBehavior : MonoBehaviour
{
    private TestAgentBT _agent;
    private BNode _root;

    private void Start()
    {
        _agent = GetComponent<TestAgentBT>();
        ConstructTree();
    }

    /// <summary>
    /// Constructs a behavior tree
    /// </summary>
    public void ConstructTree()
    {
        //_root = new SeekNode(_agent, _agent.target.transform);

        _root =
        new Selector(
            new QueryGate(() => _agent.TargetDistance < 100,
                new Sequence(
                    new SeekNode(_agent, _agent.target.transform),
                    new AttackNode(_agent, null)
                    )
                )
        //),
        //new QueryGate(() => _agent.health > 200,
        //    new Sequence(
        //        new ActionNode(print("Idle")),
        //        new ActionNode(print("Patrol")),
        //        new ActionNode(print("Wander"))
        //    )
        //),
        //new QueryGate(() => _agent.health >= 100,
        //           // Flee
        //           // Hide
        //)
        );
    }

    public void Run()
    {
        _root.Run();
    }

    public void Stop()
    {
        throw new System.NotImplementedException("I should do this sometime");
    }
}
