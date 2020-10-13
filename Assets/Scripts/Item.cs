using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    private Player player;

    void Start()
    {
        player = FindObjectOfType<Player>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (player.curHp < player.maxHp)
            {
                player.curHp += 1;

                player.curHp = Mathf.Max(0, player.maxHp);

                Destroy(gameObject);
            }                       
        }
    }
}
