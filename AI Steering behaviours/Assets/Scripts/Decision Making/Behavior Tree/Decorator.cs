using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Decorator : BNode {

    public Decorator(BNode node)
    {
        if(node != null)
        {
            child = node;
            node.Parent = this;
        }
    }

    protected BNode child;
    /// <summary>
    /// All the child nodes in this node
    /// </summary>
    public BNode Child
    {
        get { return child; }
    }
}
