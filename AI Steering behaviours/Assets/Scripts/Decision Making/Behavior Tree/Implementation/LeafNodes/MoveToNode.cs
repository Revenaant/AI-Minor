using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveToNode : BNode
{
    public GameObject self { get; set; }
    public Transform newPos { get; set; }

    public MoveToNode(Transform pos, GameObject body)
    {
        newPos = pos;
        self = body;
    }

    public override void Init(Action action)
    {
        if (action != null) action.Invoke();
    }

    public override BNodeStatus Run()
    {
        if (Input.GetKeyDown(KeyCode.T)) return BNodeStatus.Failure;

        if (isAtDestination()) return BNodeStatus.Success;
        else move();

        return BNodeStatus.Running;
    }

    private void move()
    {
        Vector3 current = self.transform.position;
        Vector3 dest = newPos.position;
        Vector3 dir = dest - current;

        self.transform.Translate(dir.normalized * 0.1f);
    }

    private bool isAtDestination()
    {
        return Vector3.Distance(self.transform.position, newPos.position) < 2f; 
    }
}
