using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SteeringManager))]
public abstract class SteeringBehaviour : MonoBehaviour
{
    protected SteeringManager steering;

    private void Awake()
    {
        steering = GetComponent<SteeringManager>();
        if (steering == null) steering = gameObject.AddComponent<SteeringManager>();
    }
}
