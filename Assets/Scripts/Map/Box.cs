using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : MonoBehaviour
{
    public GameObject destoryEffect;

    public List<GameObject> itemPrefab;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Bullet")
        {
            Instantiate(destoryEffect, transform.position, Quaternion.identity);

            int r = Random.Range(0, 100);
            int i = Random.Range(0, itemPrefab.Count);
            if (r <= 30)
            {
                Instantiate(itemPrefab[i], transform.position, transform.rotation);
            }
            

            Destroy(gameObject);
        }
    }
}
