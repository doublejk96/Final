﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Transform target;

    [Header("HP")]
    public int curHp;
    public int maxHp;

    public virtual void init()
    {
        target = FindObjectOfType<Player>().transform;

        curHp = maxHp;
    }

    void Update()
    {
        LookPlayer();
    }

    void LookPlayer()
    {
        transform.LookAt(target.transform);
    }

    public void OnDamage(int damage)
    {
        curHp -= damage;
        curHp = Mathf.Max(0, curHp);

        if (curHp <= 0)
        {
            Die();
        }
    }

    void Die()
    {

    }

}
