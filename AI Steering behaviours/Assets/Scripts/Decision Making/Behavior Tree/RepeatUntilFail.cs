using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepeatUntilFail : Decorator
{
    public RepeatUntilFail(BNode node) : base(node) { }

    public override void Init(System.Action action)
    {
        if (action != null) action.Invoke();
    }

    public override BNodeStatus Run()
    {
        if (NodeStatus != BNodeStatus.Failure)
            nodeStatus = RunLogic();
        //do { nodeStatus = RunLogic(); }
        //while (NodeStatus != BNodeStatus.Failure);

        return NodeStatus;
    }

    private BNodeStatus RunLogic()
    {
        // Evaluate the child node
        switch (child.Run())
        {
            // If this child failed, the whole sequence fails
            case BNodeStatus.Failure:
                nodeStatus = BNodeStatus.Failure;
                return NodeStatus;
            // If this child was successful return success as well
            case BNodeStatus.Success:
                nodeStatus = BNodeStatus.Success;
                Reset(); // This should be it's own nodeType
                return NodeStatus;
            // if this child is still running, return this node with Running
            case BNodeStatus.Running:
                nodeStatus = BNodeStatus.Running;
                return NodeStatus;
        }
        nodeStatus = BNodeStatus.Success;
        return NodeStatus;
    }
}
