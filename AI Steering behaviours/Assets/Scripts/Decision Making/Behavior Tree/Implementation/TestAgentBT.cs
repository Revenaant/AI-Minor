﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TestAgentBT : AbstractAgent
{
    public GameObject target;

    private OrcBehavior _brain;
    private Animator _anim;
    private NavMeshAgent _nav;

    [Range(0, 600)]
    public float health;
    public float maxHealth;

    private UIFillBar _fillbar;
    public UnityEngine.UI.Text _text;

    // Use this for initialization
    private void Start()
    {
        // Behavior Tree
        _brain = GetComponent<OrcBehavior>();
        //_brain.ConstructTree();

        // Anim
        _anim = GetComponent<Animator>();

        // Nav
        _nav = GetComponent<NavMeshAgent>();

        // Health bar
        _fillbar = GetComponent<UIFillBar>();
        maxHealth = health;
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        // Updates the state
        //fsm.Step();

        if (Input.GetKeyDown(KeyCode.O)) TakeDamage(100);
        if (Input.GetKeyDown(KeyCode.I)) TakeDamage(-100);
        _brain.Run();

        //if (Input.GetKeyDown(KeyCode.Alpha1)) fsm.ChangeState<Super_Passive>();
        //if (Input.GetKeyDown(KeyCode.Alpha2)) fsm.ChangeState<Super_Agressive>();
        //if (Input.GetKeyDown(KeyCode.Alpha3)) fsm.ChangeState<Super_Scared>();

        //if (Input.GetKeyDown(KeyCode.Alpha4)) fsm.ChangeState<IdleState>();
        //if (Input.GetKeyDown(KeyCode.Alpha5)) fsm.ChangeState<PatrolState>();
        //if (Input.GetKeyDown(KeyCode.Alpha6)) fsm.ChangeState<WanderState>();

        //if (Input.GetKeyDown(KeyCode.Alpha7)) fsm.ChangeState<SeekState>();
        //if (Input.GetKeyDown(KeyCode.Alpha8)) fsm.ChangeState<AttackState>();

        //if (Input.GetKeyDown(KeyCode.Alpha9)) fsm.ChangeState<FleeState>();
        //if (Input.GetKeyDown(KeyCode.Alpha0)) fsm.ChangeState<HideState>();

        //if (Input.GetKeyDown(KeyCode.Tab)) fsm.ChangeState<NullState>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Weapon"))
        {
            TakeDamage(100);
        }
    }

    /// <summary>
    /// Takes health away from the agent by the specified ammount, clamping health to minimum
    /// </summary>
    /// <param name="damage"></param>
    public virtual void TakeDamage(float damage)
    {
        health = Mathf.Clamp(health - damage, 0, maxHealth);
        StartCoroutine(_fillbar.lerpBar(health / maxHealth));
        _text.text = health + " / " + maxHealth;

        if (health == 0)
        {
            _anim.Play("death");
            //fsm.ChangeState<NullState>();
            //if (OnDeath != null) OnDeath.Invoke(this);
        }
    }

    // This function is called by animator events
    public void hit() { }

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
