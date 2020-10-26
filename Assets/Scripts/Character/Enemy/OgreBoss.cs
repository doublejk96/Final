using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OgreBoss : Enemy
{
    public Collider coll;

    public float damage;

    [Header("Speed")]
    public float walkSpeed;

    [Header("Distance")]
    public float chaseDis;
    public float attackDis;

    [Header("Effect")]
    public GameObject hitEffect;

    public bool isAttack = false;

    void Update()
    {
        AnimationSpeed();

        OgreeAi();
    }

    void AnimationSpeed()
    {
        anim.SetFloat("walkSpeed", walkSpeed);
    }

    void OgreeAi()
    {
        float dis = Vector3.Distance(transform.position, player.transform.position);

        if (isDie == false)
        {
            if (dis <= chaseDis)
            {
                transform.LookAt(player.transform);

                anim.SetBool("isWalk", true);
                agent.SetDestination(player.transform.position);
            }

            if (dis <= attackDis)
            {
                if (Player.Ins.curHp > 0)
                {
                    if (isAttack == false)
                    {
                        agent.isStopped = true;

                        isAttack = true;
                        anim.SetTrigger("isAttack");
                    }
                }
                else
                {
                    agent.isStopped = true;
                    anim.SetBool("isWalk", false);
                }
            }
        }        
    }

    void StoppedFalse()
    {
        agent.isStopped = false;
        isAttack = false;
    }

    void ShakeCam()
    {
        Player.Ins.cam.ShakeCamera(0.2f, 0.2f);
    }

    void BigShakeCam()
    {
        Player.Ins.cam.ShakeCamera(0.5f, 0.5f);
    }

    public bool PlayerInAttackRange()
    {
        if (player != null)
        {
            Vector3 dir = player.transform.position - transform.position;
            float dis = dir.magnitude;
            if (dis <= attackDis)
            {
                player.OnDamage(damage);
                player.rigid.AddForce(new Vector3(0, 500, 0));
            }
        }
        return false;
    }

    public override void OnDamage(float damage)
    {
        base.OnDamage(damage);

        Instantiate(hitEffect, transform.position, transform.rotation);

        if (curHp == maxHp / 2)
        {
            agent.isStopped = true;
            anim.SetTrigger("isHit");
        }
    }

    public override void Die()
    {
        base.Die();

        agent.isStopped = true;
        anim.SetTrigger("isDie");
        coll.enabled = false;  
    }
}
