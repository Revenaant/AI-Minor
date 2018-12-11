using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flee : SteeringBehaviour
{
    [SerializeField] private float _speed = 1;
    private Vector3 _target;

    // Update is called once per frame
    void Update () {
        // Get target direction
        Vector3 dir = transform.position - _target;
        //steering.DesiredVelocity = dir.normalized * _speed;

        //if (dir.sqrMagnitude > Mathf.Pow(threatRadius, 2))
        //{
        //    _velocity = Vector3.zero;
        //    steering = Vector3.zero;
        //}
    }
}
