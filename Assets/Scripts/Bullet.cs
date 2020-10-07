using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Rigidbody rigid;

    public float speed;

    void Start()
    {
        rigid = GetComponent<Rigidbody>();

        rigid.AddForce(transform.forward * speed);
    }

    public virtual void OnCollisionEnter(Collision other)
    {
        rigid.useGravity = true;

        Destroy(gameObject);
    }
}
