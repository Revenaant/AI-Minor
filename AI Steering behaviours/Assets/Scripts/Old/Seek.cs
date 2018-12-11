using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Seek : SteeringBehaviour
{
    [SerializeField] private float _speed = 1;
    private Vector3 _target;

    // Update is called once per frame
    void Update() {
        // Get target direction
        Vector3 dir = MousePos - transform.position;
        //steering.DesiredVelocity = dir.normalized * _speed;
    }

    public Vector3 MousePos
    {
        get
        {
            RaycastHit hit;
            Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit);

            Vector3 mousePos = hit.point;
            mousePos.y = 0;

            return mousePos;
        }
    }
}
