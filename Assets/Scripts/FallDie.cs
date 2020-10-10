using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallDie : MonoBehaviour
{
    private Player player;
    private Enemy enemy;

    void Start()
    {
        player = FindObjectOfType<Player>();
        enemy = FindObjectOfType<Enemy>();
    }

    private void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.tag == "Player")
        {
            player.Die();
        }

        if (other.gameObject.tag == "Enemy")
        {
            enemy.FallDie();
        }
    }
}
