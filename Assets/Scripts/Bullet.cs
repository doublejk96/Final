using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{  
    private Rigidbody rigid;
    private Collider col;

    [Header("Effect")]
    public GameObject wallEffect;
    public GameObject boxEffect;
        
    void Start()
    {
        rigid = GetComponent<Rigidbody>();
        col = GetComponent<Collider>();

        rigid.AddForce(transform.forward * GunController.Ins.speed);
    }

    private void OnTriggerEnter(Collider other)
    {
        float damage = GunController.Ins.damage;

        if (other.gameObject.tag == "Enemy")
        {
            Enemy enemy = other.gameObject.GetComponent<Enemy>();
            enemy.OnDamage(damage);

            Destroy(gameObject);
        }
        else if (other.gameObject.tag == "Box")
        {
            Instantiate(boxEffect, transform.position, Quaternion.identity);

            Destroy(gameObject);
        }
        else if (other.gameObject.tag == "Obstacle")
        {
            Instantiate(wallEffect, transform.position, Quaternion.identity);

            Destroy(gameObject);
        }
    }
}
