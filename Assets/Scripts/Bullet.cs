using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{  
    private Rigidbody rigid;
    private Collider bulletCollider;

    [Header("Effect")]
    public GameObject hitEffect;
    public GameObject wallEffect;
    public GameObject boxEffect;
        
    void Start()
    {
        rigid = GetComponent<Rigidbody>();
        bulletCollider = GetComponent<Collider>();

        rigid.AddForce(transform.forward * GunManager.Ins.speed);
    }

    private void OnTriggerEnter(Collider other)
    {
        float damage = GunManager.Ins.damage;

        if (other.gameObject.tag == "Enemy")
        {
            Enemy enemy = other.gameObject.GetComponent<Enemy>();
            enemy.OnDamage(damage);

            Instantiate(hitEffect, transform.position, Quaternion.identity);

            Destroy(gameObject);
        }
        else if (other.gameObject.tag == "Box")
        {
            Instantiate(boxEffect, transform.position, Quaternion.identity);

            Destroy(gameObject);
        }
        else
        {
            Instantiate(wallEffect, transform.position, Quaternion.identity);

            Destroy(gameObject);
        }
    }
}
