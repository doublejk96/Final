﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : Bullet
{
    private Enemy enemy;

    [Header("Damage")]
    public int damage;

    void Start()
    {
        Init();

        enemy = FindObjectOfType<Enemy>();
    }

    public override void OnCollisionEnter(Collision other)
    {
        base.OnCollisionEnter(other);

        if (other.gameObject.tag == "Enemy")
        {
            enemy.OnDamage(damage);

            Destroy(gameObject);
        }
    }
}
