using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hunteroid : Boid
{
    private void Start()
    {
        Boid.AllBoids.Remove(this);
    }

    protected override void Update()
    {
        _steering.CollisionAvoidance();
        _steering.WallAvoidance();
        _steering.Wander();
        //_steering.Pursue(FindObjectOfType<Preyoid>());

        transform.GetChild(0).localScale = Vector3.one;
        transform.GetChild(0).position = transform.position;

        base.Update();
    }
}
