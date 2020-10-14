using System.Collections;
using System.Collections.Generic;
using System.Runtime.Remoting.Messaging;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : Character
{
    [Header("Effect")]
    public GameObject hitEffect;
    public GameObject dieEffect;

    public override void OnDamage(float damage)
    {
        base.OnDamage(damage);

        Instantiate(hitEffect, transform.position, Quaternion.identity);
    }
}
