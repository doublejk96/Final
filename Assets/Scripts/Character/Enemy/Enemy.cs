using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    [Header("Script")]
    protected Animator anim;
    protected Player player;
    protected NavMeshAgent agent;
    private Rigidbody rigid;

    [Header("Hp")]
    public float curHp;
    public float maxHp;

    protected bool isDie = false;

    public void Start()
    {
        anim = GetComponent<Animator>();
        player = FindObjectOfType<Player>();
        agent = GetComponent<NavMeshAgent>();
        rigid = GetComponent<Rigidbody>();

        curHp = maxHp;
    }

    public virtual void OnDamage(float damage)
    {
        curHp -= damage;
        curHp = Mathf.Max(0, curHp);

        if (curHp <= 0)
        {
            Die();
        }
    }

    public virtual void Die()
    {
        isDie = true;

        SpawnManager spawn = transform.parent.GetComponent<SpawnManager>();
        spawn.enemyList.Remove(this);
    }
}
