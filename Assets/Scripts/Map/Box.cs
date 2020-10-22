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

            int i = Random.Range(0, itemPrefab.Count);
            Instantiate(itemPrefab[i], transform.position, transform.rotation);

            Destroy(gameObject);
        }
    }
}
