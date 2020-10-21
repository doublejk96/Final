using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Hp")]
    public float curHp;
    public float maxHp;

    public void Start()
    {
        ResetEnemy();
    }

    void ResetEnemy()
    {
        curHp = maxHp;
    }

    public void OnDamage(float damage)
    {
        curHp--;
        curHp = Mathf.Max(0, curHp);

        if (curHp <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        SpawnManager spawn = transform.parent.GetComponent<SpawnManager>();
        spawn.enemyList.Remove(this);

        Destroy(gameObject);        
    }
}
