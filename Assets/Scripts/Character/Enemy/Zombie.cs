using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombie : Enemy
{
    [Header("Speed")]
    public float walkSpeed;

    [Header("Distance")]
    public float chaseDis;
    public float attackDis;

    [Header("Effect")]
    public GameObject hitEffect;
    public GameObject dieEffect;

    void Update()
    {
        AnimationSpeed();

        ZombieAi();
    }
    void AnimationSpeed()
    {
        anim.SetFloat("walkSpeed", walkSpeed);
    }

    void ZombieAi()
    {
        anim.SetBool("isWalk", false);

        float dis = Vector3.Distance(transform.position, player.transform.position);

        if (dis <= chaseDis)
        {
            transform.LookAt(player.transform);

            anim.SetBool("isWalk", true);
            // 추격
            agent.SetDestination(player.transform.position);
        }
        else if (dis <= attackDis)
        {
            // 공격
        }
    }

    public override void OnDamage(float damage)
    {
        base.OnDamage(damage);

        Instantiate(hitEffect, transform.position, transform.rotation);
    }

    public override void Die()
    {
        base.Die();

        anim.SetTrigger("isDie");

        Invoke("DieEffect", 1.89f);

        Destroy(gameObject, 1.89f);
    }

    void DieEffect()
    {
        Vector3 effectPos = new Vector3(0, 0.6f, 0);
        Instantiate(dieEffect, transform.position + effectPos, transform.rotation);
    }
}
