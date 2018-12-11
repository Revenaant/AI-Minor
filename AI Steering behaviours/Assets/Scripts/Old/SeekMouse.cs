using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SeekMouse : MonoBehaviour
{
    [SerializeField] private float _mass = 1;
    [SerializeField] private float _maxVelocity = 1;
    [SerializeField] private float _maxSteerForce = 0.25f;

    [Header("Flee Properties")]
    [SerializeField] private float _fleeRadius = 15;

    [Header("Arrive Properties")]
    [SerializeField] private float _arriveRadius = 15;

    [Header("Wander Properties")]
    [SerializeField] private float _angleDelta = 15;        // Erraticness 
    [SerializeField] private float _circleDistance = 5;     
    [SerializeField] private float _circleRadius = 1;

    [Header("Collision Avoidance")]
    [SerializeField] private float _maxSeeAhead = 5;

    [Header("UI Interface")]
    [SerializeField] private Dropdown _drop;

    private Vector3 _position;
    private Vector3 _velocity;
    private Vector3 _desiredVelocity;

    // Wander
    private float _wanderAngle = 0;

    private enum SteerBehaviour { None, Seek, Flee, Arrive, Wander, Pursuit, Evade }
    private SteerBehaviour _currentBehaviour = SteerBehaviour.None;

	// Use this for initialization
	void Start ()
    {
        _position = transform.position;

        // Initialize UI
        PopulateDropdown(_drop, _currentBehaviour);
	}
	
	// Update is called once per frame
	void Update () {
        transform.GetChild(0).localScale = Vector3.one;
        transform.GetChild(0).position = transform.position;

        // Decide the steering vector
        _currentBehaviour = (SteerBehaviour)_drop.value;
        switch (_currentBehaviour)
        {
            case SteerBehaviour.None:
                Stop();
                break;
            case SteerBehaviour.Seek:
                _desiredVelocity = Seek(FindObjectOfType<SteeringManager>().transform.position);
                break;
            case SteerBehaviour.Flee:
                DrawRadius();
                _desiredVelocity = Flee(MousePos, _fleeRadius);
                break;
            case SteerBehaviour.Arrive:
                DrawRadius();
                transform.GetChild(0).position = MousePos;
                _desiredVelocity = Arrive(MousePos, _arriveRadius);
                break;
            case SteerBehaviour.Wander:
                _desiredVelocity = Wander();
                break;
            case SteerBehaviour.Pursuit:
                _desiredVelocity = Pursuit(FindObjectOfType<SteeringManager>());
                break;
            case SteerBehaviour.Evade:
                _desiredVelocity = Evade(FindObjectOfType<SteeringManager>());
                break;

        }

        Move();
        LookWhereYoureGoing();
        DrawVectors();
    }

    private void Move()
    {
        // Add steering to velocity
        Vector3 steering = Vector3.ClampMagnitude(_desiredVelocity - _velocity, _maxSteerForce);
        steering = steering / _mass;

        // Add acceleration to velocity
        _velocity = Vector3.ClampMagnitude(_velocity + steering, _maxVelocity);
        _velocity.y = 0;

        // Add velocity
        _position += _velocity;

        transform.position = _position;
    }

    private void Stop()
    {
        _velocity = Vector3.zero;
        _desiredVelocity = Vector3.zero;
    }

    private void LookWhereYoureGoing()
    {
        transform.rotation = Quaternion.LookRotation(_velocity);
    }

    private Vector3 Seek(Vector3 target)
    {
        // Get target direction
        Vector3 dir = target - _position;
        return dir.normalized * _maxVelocity;
    }

    /// <summary>
    /// Opposite to Seek
    /// </summary>
    /// <param name="target"></param>
    /// <returns></returns>
    private Vector3 Flee(Vector3 target, float threatRadius)
    {
        Vector3 desiredVelocity;

        // Get target direction
        Vector3 dir = _position - target;
        desiredVelocity = dir.normalized * _maxVelocity;

        if (dir.sqrMagnitude > Mathf.Pow(threatRadius, 2))
        {
            _velocity = Vector3.zero;
            desiredVelocity = Vector3.zero;
        }
            
        return desiredVelocity;
    }

    /// <summary>
    /// Arrive is like flee, but it gradually stops when reaching it's destination
    /// The difference between Arrive and Seek is the piece of code that 
    /// considers a radius and makes it stop gradually
    /// </summary>
    /// <returns></returns>
    private Vector3 Arrive(Vector3 target, float slowRadius)
    {
        // Get target direction
        Vector3 dir = target - _position;

        // Gradually stop when near target
        if (dir.sqrMagnitude < Mathf.Pow(slowRadius, 2))
            return dir.normalized * _maxVelocity * (dir.magnitude / slowRadius);
        else
            return dir.normalized * _maxVelocity;
    }

    private Vector3 Wander()
    {
        // Place the center of the circle in front of the entity
        Vector3 circleCenter = _velocity.normalized * _circleDistance;

        // Rotate the wanderAngle slowly by a random number that goes from (-delta, delta)
        _wanderAngle += (UnityEngine.Random.Range(-_angleDelta, _angleDelta));
        #region otherMethod
        //_wanderAngle += (UnityEngine.Random.Range(0.0f, 1.0f) * 2 * _angleDelta) - _angleDelta; // Same thing as above
        #endregion

        // Stablish the displacement the _wanderAngle direction and force with magnitude = radius
        Vector3 displacement = (Quaternion.AngleAxis(_wanderAngle, Vector3.up) * Vector3.one) * _circleRadius;
        displacement.y = 0;

        Vector3 wanderForce = circleCenter + displacement;

        // Debug
        DrawArrow.DrawDebugLine(transform.position, transform.position + wanderForce, Color.green);
        transform.GetChild(0).position = transform.position + (circleCenter * 0.85f);
        transform.GetChild(0).localScale = new Vector3(_circleRadius * 1.5f, 1, _circleRadius * 1.5f);

        return wanderForce;
    }

    /// <summary>
    /// A Seek looking into the future
    /// </summary>
    /// <param name="unit"></param>
    /// <returns></returns>
    private Vector3 Pursuit(SteeringManager unit)
    {
        // Get the distance between units
        Vector3 dist = unit.transform.position - transform.position;

        // Can be fixed number to manually control accuracy. But dynamic is better.
        float estimate = dist.magnitude / _maxVelocity;

        // Look ahead of the other unit
        Vector3 futurePos = unit.transform.position /*+ unit.Velocity*/ * estimate;
        return Seek(futurePos);
    }

    /// <summary>
    /// Same as pursuit, but Flee instead of Seek
    /// </summary>
    /// <param name="unit"></param>
    /// <returns></returns>
    private Vector3 Evade(SteeringManager unit)
    {
        // Get the distance between units
        Vector3 dist = unit.transform.position - transform.position;

        // Can be fixed number to manually control accuracy. But dynamic is better.
        float estimate = dist.magnitude / _maxVelocity;

        // Look ahead of the other unit
        Vector3 futurePos = unit.transform.position /*+ unit.Velocity*/ * estimate;
        return Flee(futurePos, 15);
    }

    //private Vector3 CollisionAvoidance()
    //{
    //    Vector3 ahead = transform.position + _velocity.normalized * _maxSeeAhead;
    //    Vector3 halfHead = ahead * 0.5f;
    //}

    // Line circle collision check
    private bool Intersects(Vector3 ahead, Vector3 halfHead, SphereCollider col)
    {
        bool intersects = (col.center - ahead).magnitude <= col.radius ||
                          (col.center - halfHead).magnitude <= col.radius;
        return intersects;
    }

    #region Utility

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

    private void PopulateDropdown(Dropdown dropdown, Enum e)
    {
        string[] enumNames = Enum.GetNames(e.GetType());
        List<string> names = new List<string>(enumNames);
        dropdown.AddOptions(names);
    }

    private void DrawRadius()
    {
        transform.GetChild(0).localScale = new Vector3(_fleeRadius, 1, _fleeRadius);
    }

    private void DrawVectors()
    {
        float scale = _maxVelocity * 20;

        DrawArrow.DrawDebugLine(transform.position, transform.position +_velocity * scale, Color.blue);
        DrawArrow.DrawDebugLine(transform.position + _velocity * scale, transform.position + _desiredVelocity * scale, Color.magenta);
        DrawArrow.DrawDebugLine(transform.position, transform.position + _desiredVelocity * scale, Color.red);

        DrawArrow.DrawDebugLine(transform.position + _desiredVelocity * scale, MousePos, Color.yellow);
    }

    // Not used, but nice to remember the formulas.
    private void setVectorAngle(Vector3 vec, float value) {
        float len = vec.magnitude;
        vec.x = Mathf.Cos(value) * len;
        vec.z = Mathf.Sin(value) * len;
    }

    #endregion
}
