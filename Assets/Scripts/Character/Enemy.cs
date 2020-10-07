using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Transform target;

    [Header("HP")]
    public int curHp;
    public int maxHp;

    [Header("Effect")]
    public GameObject hitEffect;

    public virtual void init()
    {
        target = FindObjectOfType<Player>().transform;

        curHp = maxHp;
    }

    void Update()
    {
        LookPlayer();
    }

    void LookPlayer()
    {
        transform.LookAt(target.transform);
    }

    public void OnDamage(int damage)
    {
        curHp -= damage;
        curHp = Mathf.Max(0, curHp);

        Instantiate(hitEffect, transform.position, Quaternion.identity);
        Destroy(hitEffect, 1);

        if (curHp <= 0)
        {
            Die();
        }
    }

    void Die()
    {

    }

}
