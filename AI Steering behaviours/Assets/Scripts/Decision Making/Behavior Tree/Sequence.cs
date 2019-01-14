using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sequence : Composite
{
    public Sequence(params BNode[] nodes) : base(nodes) { }

    /// <summary>
    /// If any child node returns a failure, the entire node fails. Whence all  
    /// nodes return a success, the node reports a success.
    /// </summary>
    /// <returns></returns>
    public override BNodeStatus Run()
    {
        bool anyChildRunning = false;

        for (int i = 0; i < children.Count; i++)
        {
            // Evaluate the child node
            switch (children[i].Run())
            {
                // If this child failed, the whole sequence fails
                case BNodeStatus.Failure:
                    nodeStatus = BNodeStatus.Failure;
                    return NodeStatus;
                // If this child was successful, continue to the next
                case BNodeStatus.Success:
                    continue;
                // if this child is still running, return this node with Running
                case BNodeStatus.Running:
                    anyChildRunning = true;
                    continue; // RETURN???

                default:
                    nodeStatus = BNodeStatus.Success;
                    return NodeStatus;
            }
        }
        // If we haven't returned yet it means all children failed
        nodeStatus = anyChildRunning ? BNodeStatus.Running : BNodeStatus.Success;
        return NodeStatus;
    }
}
