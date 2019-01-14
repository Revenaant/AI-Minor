using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Composite : BNode
{
    public Composite(params BNode[] nodes)
    {
        children = new List<BNode>(nodes);
        for(int i = 0; i < children.Count; i++)
        {
            if (children[i] != null)
            {
                children[i].Parent = this;
            }
        }
    }

    protected List<BNode> children = new List<BNode>();
    /// <summary>
    /// All the child nodes in this node
    /// </summary>
    public List<BNode> Children
    {
        get { return children; }
    }

    public void AddChild(BNode child)
    {
        if(child != null)
        {
            child.Parent = this;
            children.Add(child);
        }
    }

    public void InsertChild(int index, BNode child)
    {
        if (child != null)
        {
            child.Parent = this;
            children.Insert(index, child);
        }
    }
}
