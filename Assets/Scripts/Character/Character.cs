﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
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

    [Header("Fire Rate")]
    public float shotTime;
    public float nextShotTime;

    [Header("Effect")]
    public GameObject muzzleFire;

    protected bool isDie;

    public void Init()
    {
        anim = GetComponent<Animator>();

        curHp = maxHp;
    }

    public virtual void Attack()
    {
        if (shotTime <= 0)
        {
            anim.SetTrigger("isFire");

            Instantiate(bulletPrefab, firePos.position, firePos.rotation);
            Instantiate(shellPrefab, shellPos.position, shellPos.rotation);
            FireEffectOn();

            shotTime = nextShotTime;
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

        anim.SetTrigger("isDie");

        if (gameObject.tag == "Player")
        {
            
        }

        if(gameObject.tag == "Enemy")
        {          
            Enemy enemy = GetComponent<Enemy>();
            GameManager.Instance.enemyList.Remove(enemy);

            Destroy(gameObject);
        }
    }
}
