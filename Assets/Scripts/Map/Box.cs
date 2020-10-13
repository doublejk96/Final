using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : MonoBehaviour
{
    public GameObject brokenEffect;

    public List<GameObject> itemPrefab;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Bullet")
        {
            Instantiate(brokenEffect, transform.position, Quaternion.identity);

            ItemDrop();

            Destroy(gameObject);            
        }
    }

    void ItemDrop()
    {
        int r = Random.Range(0, 10);
        int i = Random.Range(0, itemPrefab.Count);

        if (r <= 3)
        {
            Instantiate(itemPrefab[i], transform.position, Quaternion.identity);
        }        
    }
}
