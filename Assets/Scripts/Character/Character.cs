using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public event System.Action OnDie;

    protected Animator anim;

    [Header("HP")]
    public float curHp;
    public float maxHp;    

    [Header("Attack Speed")]
    public float attackTime;
    public float nextAttackTime;    

    protected bool isDie = false;

    public void Init()
    {
        anim = GetComponent<Animator>();

        curHp = maxHp;
    }

    public virtual void Update()
    {
        attackTime -= Time.deltaTime;
    }

    public virtual void Attack()
    {/*
        if (0 <= attackTime && this is Player)
        {
            Instantiate(bulletPrefab, firePos.position, firePos.rotation);
            Instantiate(shellPrefab, shellPos.position, shellPos.rotation);
            FireEffectOn();
        }*/
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

        if (gameObject.tag == "Player")
        {
            
        }

        if(gameObject.tag == "Enemy")
        {
            if (OnDie != null)
            {
                OnDie();
            }

            Enemy enemy = GetComponent<Enemy>();
            GameManager.Instance.enemyList.Remove(enemy);

            Destroy(gameObject);
        }
    }
}
