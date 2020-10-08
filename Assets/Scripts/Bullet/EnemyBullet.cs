using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : Bullet
{
    private Player player;

    [Header("Damage")]
    public int damage;

    void Start()
    {
        Init();

        player = FindObjectOfType<Player>();
    }

    public override void OnCollisionEnter(Collision other)
    {
        base.OnCollisionEnter(other);

        if (other.gameObject.tag == "Player")
        {
            player.OnDamage(damage);

            Destroy(gameObject);
        }
    }
}
