using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombie : Enemy
{  
    [Header("Effect")]
    public GameObject hitEffect;
    public GameObject dieEffect;

    void Update()
    {
        transform.LookAt(player.transform);
    }

    public override void OnDamage(float damage)
    {
        base.OnDamage(damage);

        Instantiate(hitEffect, transform.position, transform.rotation);
    }

    public override void Die()
    {
        base.Die();

        anim.SetTrigger("isDie");

        Invoke("DieEffect", 1.89f);

        Destroy(gameObject, 1.89f);
    }

    void DieEffect()
    {
        Vector3 effectPos = new Vector3(0, 0.6f, 0);
        Instantiate(dieEffect, transform.position + effectPos, transform.rotation);
    }
}
