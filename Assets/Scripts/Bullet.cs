using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{  
    private Rigidbody rigid;
    private Collider bulletCollider;
        
    void Start()
    {
        float power = GunManager.Ins.bulletPower;

        rigid = GetComponent<Rigidbody>();
        bulletCollider = GetComponent<Collider>();

        rigid.AddForce(transform.forward * power);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject)
        {
            rigid.useGravity = true;

            Destroy(gameObject);
        }
    }
}
