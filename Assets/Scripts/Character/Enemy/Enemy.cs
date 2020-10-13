using System.Collections;
using System.Collections.Generic;
using System.Runtime.Remoting.Messaging;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : Character
{
    [Header("ATK")]
    public float atk;

    [Header("Sight")]
    public float moveRange = 5f;
    public float attackRange = 0.5f;

    protected NavMeshAgent agent;
    protected Transform target;

    void Start()
    {
        Init();

        agent = GetComponent<NavMeshAgent>();
        target = FindObjectOfType<Player>().transform;
    }

    void Update()
    {
        Move();
    }

    void Move()
    {
        anim.SetBool("isMove", false);

        float dis = Vector3.Distance(target.position, transform.position);

        if (dis <= moveRange && dis > attackRange)
        {
            transform.LookAt(target);

            anim.SetBool("isMove", true);

            agent.SetDestination(target.position);
        }
        else if (dis <= attackRange)
        {
            anim.SetTrigger("isAttack");
        }
    }
}
