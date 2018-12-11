using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tracer : MonoBehaviour
{
    public static void Trace(Vector3 start, Vector3 end)
    {
        Debug.DrawLine(start, end, Color.red, 2);
    }
}
