using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class Obstacle : MonoBehaviour
{
    public static List<Collider> AllObstacles = new List<Collider>();

    private void Start()
    {
        AllObstacles.Add(GetComponent<Collider>());
    }
}
