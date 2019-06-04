using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Selector : Composite
{
    public Selector(params BNode[] nodes) : base(nodes) { }

    public override void Init(System.Action action)
    {
        if (action != null) action.Invoke();
    }

    private BNode currentChild;
    private int iterator = 0;

    /// <summary>
    /// If any child reports a success If any of the children reports a success, the selector will 
    /// immediately report a success upwards.If all children fail,
    /// it will report a failure instead.
    /// </summary>
    /// <returns></returns>
    public override BNodeStatus Run()
    {
        if (NodeStatus == BNodeStatus.Running) nodeStatus = RunLogic();
        return NodeStatus;
    }

    private BNodeStatus RunLogic()
    {
        lock (Locker)
        {
            if (children.Count <= 0) return BNodeStatus.Failure;
            currentChild = children[iterator];

            BNodeStatus test;
            // Evaluate the child node
            switch (test = currentChild.Run())
            {
                // If this child failed, evaluate the next
                case BNodeStatus.Failure:
                    //children[iterator].Button.SetColor(Color.red);
                    UnityEngine.Debug.Log("Steps " + iterator + "/" + (children.Count - 1) + " result " + test);
                    if (++iterator == children.Count) return nodeStatus = BNodeStatus.Failure;
                    return nodeStatus = BNodeStatus.Running;
                // If this child was successful, return this node with Success
                case BNodeStatus.Success:
                    UnityEngine.Debug.Log("Steps " + iterator + "/" + (children.Count - 1) + " result " + test);
                    //children[iterator].Button.SetColor(Color.green);
                    return nodeStatus = BNodeStatus.Success;
                // if this child is still running, return this node with Running
                case BNodeStatus.Running:
                    UnityEngine.Debug.Log("Steps " + iterator + "/" + (children.Count - 1) + " result " + test);
                    //children[iterator].Button.SetColor(Color.yellow);
                    return nodeStatus = BNodeStatus.Running;
            }
            // If we haven't returned yet it means all children failed
            nodeStatus = BNodeStatus.Failure;
            return NodeStatus;
        }
    }

    public override void Reset()
    {
        base.Reset();
        iterator = 0;
    }
}
