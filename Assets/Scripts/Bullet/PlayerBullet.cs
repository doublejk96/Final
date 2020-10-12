﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : Bullet
{
    [Header("Damage")]
    public int damage;

    void Start()
    {
        Init();
    }

    public override void OnCollisionEnter(Collision other)
    {
        base.OnCollisionEnter(other);

        if (other.gameObject.tag == "Enemy")
        {
            Enemy enemy = other.gameObject.GetComponent<Enemy>();
            enemy.OnDamage(damage);

            Destroy(gameObject);
        }
    }
}
