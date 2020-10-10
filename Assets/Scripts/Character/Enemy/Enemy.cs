using System.Collections;
using System.Collections.Generic;
using System.Runtime.Remoting.Messaging;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : Character
{
    private Transform target;

    [Header("Effect")]
    public GameObject hitEffect;
    public GameObject dieEffect;

    void Start()
    {
        Init();

        target = FindObjectOfType<Player>().transform;        
    }

    void Update()
    {
        shotTime -= Time.deltaTime;

        if (isDie == false)
        {
            transform.LookAt(target.transform);

            Attack();
        }      
    }

    public override void OnDamage(float damage)
    {
        base.OnDamage(damage);

        Instantiate(hitEffect, transform.position, Quaternion.identity);
    }

    public override void Die()
    {
        base.Die();

        Instantiate(dieEffect, transform.localPosition, Quaternion.identity);
    }
}
