using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AvoidanceBox : MonoBehaviour
{
    private List<Collider> colliders = new List<Collider>();
    private BoxCollider self;

    private void Start()
    {
        self = GetComponent<BoxCollider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Obstacle>() != null && !colliders.Contains(other)) { colliders.Add(other); }
    }

    private void OnTriggerExit(Collider other)
    {
        colliders.Remove(other);
        if(other.GetComponent<Obstacle>() != null) other.GetComponent<Renderer>().material.color = Color.white;
    }

    public BoxCollider Self { get { return self; } }
    public List<Collider> GetColliders() { return colliders; }
}
