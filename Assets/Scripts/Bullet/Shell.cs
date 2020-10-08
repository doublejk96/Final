using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shell : MonoBehaviour
{
    private Rigidbody rigid;

    public float forceMin;
    public float forceMax;

    void Start()
    {
        rigid = GetComponent<Rigidbody>();

        float force = Random.Range(forceMin, forceMax);
        rigid.AddForce(transform.right * force);

        Destroy(gameObject, 10);
    }
}
