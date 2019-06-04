using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sequence : Composite
{
    public Sequence(params BNode[] nodes) : base(nodes) { }

    public override void Init(System.Action action)
    {
        if (action != null) action.Invoke();
    }

    private BNode currentChild;
    private int iterator = 0;

    /// <summary>
    /// If any child node returns a failure, the entire node fails. Whence all  
    /// nodes return a success, the node reports a success.
    /// </summary>
    /// <returns></returns>
    public override BNodeStatus Run()
    {
        if (NodeStatus == BNodeStatus.Running) nodeStatus = RunLogic();
        return NodeStatus;
    }

    private BNodeStatus RunLogic()
    {
        bool anyChildRunning = false;

        if (children.Count <= 0) return BNodeStatus.Failure;
        currentChild = children[iterator];

        // Evaluate the child node
        switch (currentChild.Run())
        {
            // If this child failed, the whole sequence fails
            case BNodeStatus.Failure:
                return nodeStatus = BNodeStatus.Failure;
            // If we reach the final child, return with Success, else return Running and go to the next
            case BNodeStatus.Success:

                if (++iterator == children.Count) return nodeStatus = BNodeStatus.Success;
                return nodeStatus = BNodeStatus.Running;
            // if this child is still running, return this node with Running
            case BNodeStatus.Running:
                anyChildRunning = true;
                break; // Break to go to the next frame
        }

        nodeStatus = anyChildRunning ? BNodeStatus.Running : BNodeStatus.Success;
        return nodeStatus;

        #region parallel Node
        //    // This would be better for a parallel Node
        //    for (int i = 0; i < children.Count; i++)
        //    {
        //        // Evaluate the child node
        //        switch (children[i].Run())
        //        {
        //            // If this child failed, the whole sequence fails
        //            case BNodeStatus.Failure:
        //                nodeStatus = BNodeStatus.Failure;
        //                return NodeStatus;
        //            // If this child was successful, continue to the next
        //            case BNodeStatus.Success:
        //                continue;
        //            // if this child is still running, return this node with Running
        //            case BNodeStatus.Running:
        //                anyChildRunning = true;
        //                continue; // Continue does a good Parallel Node behavior

        //            default:
        //                nodeStatus = BNodeStatus.Success;
        //                return NodeStatus;
        //        }
        //        break;
        //    }
        //    // If we haven't returned yet it means all children failed
        //    nodeStatus = anyChildRunning ? BNodeStatus.Running : BNodeStatus.Success;
        //    return NodeStatus;
        //}
        #endregion
    }

    public override void Reset()
    {
        base.Reset();
        iterator = 0;
    }
}
