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

    [Header("Bullet")]
    public Transform bulletPrefab;
    public Transform firePos;

    [Header("Shell")]
    public Transform shellPrefab;
    public Transform shellPos;

    [Header("Attack Speed")]
    public float attackTime;
    public float nextAttackTime;

    [Header("Effect")]
    public GameObject muzzleFire;

    protected bool isDie = false;

    public void Init()
    {
        anim = GetComponent<Animator>();

        curHp = maxHp;
    }

    public virtual void Update()
    {
        attackTime -= Time.deltaTime;

        attackTime = Mathf.Max(0, attackTime);
    }

    public virtual void Attack()
    {

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
