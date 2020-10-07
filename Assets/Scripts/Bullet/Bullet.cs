using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Rigidbody rigid;

    public float speed;

    public virtual void Init()
    {
        rigid = GetComponent<Rigidbody>();

        rigid.AddForce(transform.forward * speed);
    }

    void OnCollisionEnter(Collision other)
    {
        rigid.useGravity = true;

        Destroy(gameObject);
    }
}
