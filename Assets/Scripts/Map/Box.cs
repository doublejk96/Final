using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : MonoBehaviour
{
    public GameObject brokenEffect;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Bullet")
        {
            Instantiate(brokenEffect, transform.position, Quaternion.identity);

            Destroy(gameObject);            
        }
    }
}
