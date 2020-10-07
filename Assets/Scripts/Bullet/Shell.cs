using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shell : MonoBehaviour
{
    private Rigidbody rigid;

    public float force;

    void Start()
    {
        rigid = GetComponent<Rigidbody>();

        rigid.AddForce(transform.right * force);

        Destroy(gameObject, 10);
    }
}
