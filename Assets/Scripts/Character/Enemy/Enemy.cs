using System.Collections;
using System.Collections.Generic;
using System.Runtime.Remoting.Messaging;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : Character
{
    private Transform target;
    private NavMeshAgent agent;

    [Header("AI")]
    public float speed;
    public float stopDis;
    public float retreatDis;

    [Header("Effect")]
    public GameObject hitEffect;
    public GameObject dieEffect;

    void Start()
    {
        Init();

        target = FindObjectOfType<Player>().transform;
        agent = GetComponent<NavMeshAgent>();        
    }

    void Update()
    {      
        shotTime -= Time.deltaTime;

        if (isDie == false)
        {
            transform.LookAt(target.transform);

            ChasePlayer();   
        }      
    }

    void ChasePlayer()
    {
        float dis = Vector3.Distance(transform.position, target.position);
        if (dis > stopDis)
        {
            StartCoroutine(UpdatePath());
        }
        else if (dis <= stopDis && dis > retreatDis)
        {
            anim.SetBool("isMove", false);
            anim.SetBool("backWalk", false);

            transform.position = transform.position;

            Attack();
        }
        else if (dis <= retreatDis)
        {
            anim.SetBool("backWalk", true);

            Vector3 back = Vector3.MoveTowards(transform.position, target.position, -speed * Time.deltaTime);
            transform.position = back;

            Attack();
        }
        
    }

    IEnumerator UpdatePath()
    {
        float refreshRate = 0.25f;

        while (target != null)
        {
            anim.SetBool("isMove", true);

            Vector3 targetPos = new Vector3(target.position.x, 0, target.position.z);
            agent.SetDestination(targetPos);

            yield return new WaitForSeconds(refreshRate);
        }
    }

    public override void OnDamage(float damage)
    {
        base.OnDamage(damage);

        Instantiate(hitEffect, transform.position, Quaternion.identity);
    }

    public override void Die()
    {
        base.Die();

        Instantiate(dieEffect, transform.localPosition, Quaternion.identity);
    }
}
