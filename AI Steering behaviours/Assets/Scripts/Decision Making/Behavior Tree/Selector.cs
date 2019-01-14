using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Selector : Composite
{
    public Selector(params BNode[] nodes) : base(nodes) { }

    /// <summary>
    /// If any child reports a success If any of the children reports a success, the selector will 
    /// immediately report a success upwards.If all children fail,
    /// it will report a failure instead.
    /// </summary>
    /// <returns></returns>
    public override BNodeStatus Run()
    {
        lock (Locker)
        {
            for (int i = 0; i < children.Count; i++)
            {
                
                // Evaluate the child node
                switch (children[i].Run())
                {
                    // If this child failed, evaluate the next
                    case BNodeStatus.Failure:
                        children[i].Button.SetColor(Color.red);
                        continue;
                    // If this child was successful, return this node with Success
                    case BNodeStatus.Success:
                        nodeStatus = BNodeStatus.Success;
                        children[i].Button.SetColor(Color.green);
                        return nodeStatus;
                    // if this child is still running, return this node with Running
                    case BNodeStatus.Running:
                        nodeStatus = BNodeStatus.Running;
                        children[i].Button.SetColor(Color.yellow);
                        return nodeStatus;

                    default:
                        continue;
                }
            }
            // If we haven't returned yet it means all children failed
            nodeStatus = BNodeStatus.Failure;
            return NodeStatus;
        }
    }
}
