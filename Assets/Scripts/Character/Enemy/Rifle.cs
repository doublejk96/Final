using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Rifle : Enemy
{
    [Header("Sight")]
    public float moveRange = 5f;
    public float attackRange = 3f;

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

    // Update is called once per frame
    public override void Update()
    {
        base.Update();

        transform.LookAt(player.transform);

        Move();
    }

    void Move()
    {
        agent.isStopped = true;
        anim.SetBool("Aiming", false);
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
            anim.SetBool("Aiming", true);
            anim.SetBool("isMove", false);

            Attack();
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
}
