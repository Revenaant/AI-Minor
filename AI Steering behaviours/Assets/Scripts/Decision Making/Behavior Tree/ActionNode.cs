using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionNode : BNode
{
    /// <summary>
    /// Method signature for the action
    /// </summary>
    /// <returns></returns>
    public delegate BNodeStatus ActionNodeDelegate();

    /// <summary>
    /// The delegate that is called to evaluate this node
    /// </summary>
    private ActionNodeDelegate action;

    /// <summary>
    /// Because this node contains no logic itself, the logic must
    /// be passed in the form of a delegate. As the signature states,
    /// the action needs to return a BNodeStatus enum
    /// </summary>
    /// <param name="action"></param>
    public ActionNode(ActionNodeDelegate action)
    {
        this.action = action;
    }

    public override void Init(System.Action action)
    {
        if (action != null) action.Invoke();
    }

    public override BNodeStatus Run()
    {
        switch (action())
        {
            case BNodeStatus.Success:
                return nodeStatus = BNodeStatus.Success;
            case BNodeStatus.Failure:
                return nodeStatus = BNodeStatus.Failure;
            case BNodeStatus.Running:
                return nodeStatus = BNodeStatus.Running;
            default:
                return nodeStatus = BNodeStatus.Failure;
        }
    }
}
