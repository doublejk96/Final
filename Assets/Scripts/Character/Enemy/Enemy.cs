using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Script")]
    public Animator anim;
    public Player player;

    [Header("Hp")]
    public float curHp;
    public float maxHp;

    public void Start()
    {
        anim = GetComponent<Animator>();
        player = FindObjectOfType<Player>();

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
       // SpawnManager spawn = transform.parent.GetComponent<SpawnManager>();
       // spawn.enemyList.Remove(this);
    }
}
