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

        Destroy(gameObject, 10);
    }

    public virtual void OnCollisionEnter(Collision other)
    {
        rigid.useGravity = true;
        rigid.AddForce(transform.forward * - speed / 10);

        Destroy(gameObject, 0.6f);
    }
}
