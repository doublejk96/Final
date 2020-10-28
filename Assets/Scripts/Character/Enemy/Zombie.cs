using UnityEngine;

public class Zombie : Enemy
{
    public float damage;

    [Header("Speed")]
    public float walkSpeed;

    [Header("Distance")]
    public float chaseDis;
    public float attackDis;

    [Header("Effect")]
    public GameObject hitEffect;
    public GameObject dieEffect;

    public bool isAttack = false;

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
                anim.SetTrigger("isEat");                
            }
        }
    }

    void StoppedFalse()
    {
        agent.isStopped = false;
        isAttack = false;
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
            }
        }
        return false;
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
