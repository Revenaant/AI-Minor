using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TestAgent : AbstractAgent
{
    public GameObject target;

    [HideInInspector] public HFSM<TestAgent> fsm;
    private Animator _anim;
    private NavMeshAgent _nav;

    [Range(0, 600)]
    public float health;
    private float _maxHealth;

    private UIFillBar _fillbar;
    public UnityEngine.UI.Text _text;

    // Use this for initialization
    private void Start () {
        // Creates an HFSM and sets the starting state
        fsm = new HFSM<TestAgent>(this);
        fsm.ChangeState<Super_Passive>();

        // Anim
        _anim = GetComponent<Animator>();
        _anim.Play("walk");

        // Nav
        _nav = GetComponent<NavMeshAgent>();
        _nav.SetDestination(TargetValid);

        _fillbar = GetComponent<UIFillBar>();
        _maxHealth = health;
    }
	
	// Update is called once per frame
	protected virtual void Update () {
        // Updates the state
        fsm.Step();

        if (Input.GetKeyDown(KeyCode.O)) fsm.ChangeState<FleeState>();// TakeDamage(100);
        if (Input.GetKeyDown(KeyCode.I)) fsm.ChangeState<HideState>();// TakeDamage(-100);
    }

    /// <summary>
    /// Takes health away from the agent by the specified ammount, clamping health to minimum
    /// </summary>
    /// <param name="damage"></param>
    public virtual void TakeDamage(float damage)
    {
        health = Mathf.Clamp(health - damage, 0, _maxHealth);
        StartCoroutine(_fillbar.lerpBar(health / _maxHealth));
        _text.text = health + " / " + _maxHealth;

        if (health == 0)
        {
            _anim.Play("death");
            //if (OnDeath != null) OnDeath.Invoke(this);
        }
    }

    public Animator anim
    {
        get { return _anim; }
        set { _anim = value; }
    }

    public NavMeshAgent nav
    {
        get { return _nav; }
        set { _nav = value; }
    }

    public Vector3 TargetValid
    {
        get
        {
            Vector3 pos = target.transform.position;
            Vector3 dir = transform.position - pos;

            return pos + dir * 0.15f;
        }
    }

    public float TargetDistance
    {
        get
        {
            Vector3 dist = target.transform.position - transform.position;
            return dist.magnitude;
        }
    }
}
