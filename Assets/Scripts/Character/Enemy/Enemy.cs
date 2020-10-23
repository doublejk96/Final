using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{  
    [Header("Script")]
    public Animator anim;
    public Player player;
    public NavMeshAgent agent;

    [Header("Hp")]
    public float curHp;
    public float maxHp;

    public void Start()
    {
        anim = GetComponent<Animator>();
        player = FindObjectOfType<Player>();
        agent = GetComponent<NavMeshAgent>();

        curHp = maxHp;
    }

    public virtual void OnDamage(float damage)
    {
        curHp--;
        curHp = Mathf.Max(0, curHp);

        if (curHp <= 0)
        {
            Die();
        }
    }

    public virtual void Die()
    {
        //agent.isStopped = true;

       // SpawnManager spawn = transform.parent.GetComponent<SpawnManager>();
       // spawn.enemyList.Remove(this);
    }
}
