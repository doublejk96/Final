using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Rigidbody rigid;
    private Collider Collider;

    public float speed;

    public virtual void Init()
    {
        rigid = GetComponent<Rigidbody>();
        Collider = GetComponent<Collider>();

        rigid.AddForce(transform.forward * speed);

        Destroy(gameObject, 10);
    }

    public virtual void OnTriggerEnter(Collider other)
    {
        rigid.useGravity = true;        

        if (other.gameObject.tag == "Box")
        {
            Destroy(gameObject);
        }
        else if (other.gameObject.tag == "Obstacle")
        {
            Collider.isTrigger = false;

            rigid.AddForce(transform.forward * -speed);
        }
    }
}
