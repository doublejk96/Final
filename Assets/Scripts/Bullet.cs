using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{  
    private Rigidbody rigid;
    private Collider bulletCollider;
        
    void Start()
    {
        rigid = GetComponent<Rigidbody>();
        bulletCollider = GetComponent<Collider>();

        rigid.AddForce(transform.forward * GunManager.Ins.speed);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            Enemy enemy = other.gameObject.GetComponent<Enemy>();
            enemy.OnDamage(GunManager.Ins.damage);

            Destroy(gameObject);
        }
    }
}
