using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class QueryGate : Decorator
{
    /// <summary>
    /// Run node if query is true, otherwise exit with fail
    /// </summary>
    /// <param name="node"></param>
    /// <param name="query"></param>
    public QueryGate(Func<bool> query, BNode node) : base(node)
    {
        _query = query;
    }

    private Func<bool> _query;

    public override void Init(System.Action action)
    {
        if (action != null) action.Invoke();
    }

    /// <summary>
    /// Will check the query to be true before running the child node, in which case it runs normally.
    /// If the query is false however, it will return immediately with Failure
    /// </summary>
    /// <returns></returns>
    public override BNodeStatus Run()
    {
        UnityEngine.Debug.Log("Running");
        if (_query() == false) return nodeStatus = BNodeStatus.Failure;


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
