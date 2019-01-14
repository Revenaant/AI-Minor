using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inverter : Decorator
{
    public Inverter(BNode node) : base(node) { }

    /// <summary>
    /// Reports a success if the child fails and a failure
    /// if the child succeeds.Running will report as running
    /// </summary>
    /// <returns></returns>
    public override BNodeStatus Run()
    {
        // Evaluate the child node
        switch (child.Run())
        {
            // If this child failed, the whole sequence fails
            case BNodeStatus.Failure:
                nodeStatus = BNodeStatus.Success;
                return NodeStatus;
            // If this child was successful, continue to the next
            case BNodeStatus.Success:
                nodeStatus = BNodeStatus.Failure;
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
