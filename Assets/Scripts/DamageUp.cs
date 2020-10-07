using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageUp : Item
{
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Player")
        {
            Player player = FindObjectOfType<Player>();

            player.maxHp += 1;

            player.nextShotTime -= 0.1f;

            Destroy(gameObject);
        }
    }
}
