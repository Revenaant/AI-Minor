using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public abstract class BNode
{
    public BNode () { }

    protected BNode parent;
    /// <summary>
    /// Node to return the Run status to
    /// </summary>
    public BNode Parent {
        get { return parent ?? this; }
        set { parent = value; }
    }

    protected static readonly object Locker = new object();

    protected BNodeStatus nodeStatus;
    /// <summary>
    /// The current status of this node: Success = 0, Failure = 1 or Running = 2
    /// </summary>
    public BNodeStatus NodeStatus {
        get { return nodeStatus; }
    }

    public BNodeStatus? LastStatus { get; set; }

    /// <summary>
    /// Delegate that returns the state of the node
    /// </summary>
    /// <returns></returns>
    public delegate BNodeStatus NodeReturn();

    /// <summary>
    /// Implementing classes use this method to evaluate the desired set of conditions  
    /// </summary>
    /// <returns></returns>
    public abstract BNodeStatus Run();

    public Buttons Button;
    public BNode button(Buttons b)
    {
        Button = b;
        return this;
    }
}
