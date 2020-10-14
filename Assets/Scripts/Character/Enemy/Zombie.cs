﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Zombie : Enemy
{
    [Header("ATK")]
    public float atk;

    [Header("Sight")]
    public float moveRange = 3f;
    public float attackRange = 0.5f;

    [Header("Effect")]
    public GameObject hitEffect;
    public GameObject dieEffect;

    protected NavMeshAgent agent;
    protected Player player;

    void Start()
    {
        Init();

        agent = GetComponent<NavMeshAgent>();
        player = FindObjectOfType<Player>();
    }

    public override void Update()
    {
        base.Update();

        transform.LookAt(player.transform);

        Move();
    }

    void Move()
    {
        agent.isStopped = true;
        anim.SetBool("isMove", false);        

        float dis = Vector3.Distance(player.transform.position, transform.position);
        if (dis <= moveRange)
        {
            anim.SetBool("isMove", true);

            agent.isStopped = false;

            agent.SetDestination(player.transform.position);
        }

        if (dis <= attackRange)
        {
            if (attackTime <= 0)
            {
                agent.isStopped = true;

                anim.SetTrigger("isAttack");

                attackTime = nextAttackTime;
            }                        
        }
    }

    public override void Attack()
    {
        base.Attack();

        bool inRange = RangeInPlayer(attackRange);        

        if (inRange == true)
        {
            player.OnDamage(atk);
        }

        agent.isStopped = false;
    }

    bool RangeInPlayer(float range)
    {
        if (player != null)
        {            
            float dis = Vector3.Distance(player.transform.position, transform.position);
            if (dis <= range)
            {
                return true;
            }
        }
        return false;
    }

    public override void OnDamage(float damage)
    {
        base.OnDamage(damage);

        Instantiate(hitEffect, transform.position, Quaternion.identity);
    }

    public override void Die()
    {
        base.Die();

        Instantiate(dieEffect, transform.position, Quaternion.identity);
    }
}
