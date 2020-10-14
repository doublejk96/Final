using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Rifle : Enemy
{
    [Header("Sight")]
    public float moveRange;
    public float attackRange;
    public float retreatRange;
        
    protected NavMeshAgent agent;
    protected Player player;

    void Start()
    {
        Init();

        agent = GetComponent<NavMeshAgent>();
        player = FindObjectOfType<Player>();
    }

    // Update is called once per frame
    public override void Update()
    {
        base.Update();

        if(isDie != true)
        {
            transform.LookAt(player.transform);

            Move();
        }        
    }

    void Move()
    {
        agent.isStopped = true;
        anim.SetBool("Aiming", false);
        anim.SetBool("isMove", false);
        anim.SetBool("isBackMove", false);

        float dis = Vector3.Distance(player.transform.position, transform.position);
        if (dis <= moveRange)
        {
            anim.SetBool("isMove", true);
            anim.SetBool("isBackmove", false);

            agent.isStopped = false;

            agent.SetDestination(player.transform.position);
        }

        if (dis <= attackRange)
        {
            anim.SetBool("Aiming", true);
            anim.SetBool("isMove", false);
            anim.SetBool("isBackmove", false);

            Attack();
        }

        if (dis <= retreatRange)
        {
            anim.SetBool("isBackmove", true);
            anim.SetBool("Aiming", false);

            transform.position = Vector3.MoveTowards(transform.position, player.transform.position, -1.5f * Time.deltaTime);
        }
    }

    public override void Attack()
    {
        base.Attack();

        if (attackTime <= 0)
        {
            anim.SetTrigger("isAttack");

            Instantiate(bulletPrefab, firePos.position, firePos.rotation);
            Instantiate(shellPrefab, shellPos.position, shellPos.rotation);

            FireEffectOn();

            attackTime = nextAttackTime;
        }
    }
    void FireEffectOn()
    {
        muzzleFire.SetActive(true);

        Invoke("FireEffectOff", 0.05f);
    }

    void FireEffectOff()
    {
        muzzleFire.SetActive(false);
    }

    public override void Die()
    {
        base.Die();

        anim.SetTrigger("enemyDie");

        agent.isStopped = true;

        Invoke("DieEffect", 0.89f);
    }

    void DieEffect()
    {
        Instantiate(dieEffect, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
