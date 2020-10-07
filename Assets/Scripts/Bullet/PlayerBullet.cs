using System.Collections;
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

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            enemy.OnDamage(damage);

            Destroy(gameObject);
        }                
    }
}
