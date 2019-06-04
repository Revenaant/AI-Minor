using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class RepeatUntil : Decorator
{
    public RepeatUntil(float time, BNode node) : base(node)
    {
        Timeout = TimeSpan.FromSeconds(time);
    }

    public RepeatUntil(bool query, BNode node) : base(node)
    {
        QueryResult = query;
        Timeout = TimeSpan.FromSeconds(float.MaxValue);
    }

    /// <summary>
    /// The current timeout for this node
    /// </summary>
    public TimeSpan Timeout { get; set; }
    private DateTime _end;

    /// <summary>
    /// The condition to check for exiting the loop
    /// </summary>
    private bool? QueryResult { get; set; }

    public override void Init(System.Action action)
    {
        if (action != null) action.Invoke();
    }

    public override BNodeStatus Run()
    {
        _end = DateTime.Now + Timeout;

        if (QueryResult.HasValue)
        {
            while (QueryResult == true) return RunLogic();

            nodeStatus = BNodeStatus.Failure;
            return NodeStatus;
        }
        else
        {
            while (DateTime.Now < _end) return RunLogic();

            nodeStatus = BNodeStatus.Failure;
            return NodeStatus;
        }
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
