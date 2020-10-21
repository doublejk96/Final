using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    #region Singleton
    private static Enemy instance;
    public static Enemy Ins
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<Enemy>();

                if (null == instance)
                {
                    Debug.LogError("Enemy Not Found");
                }
            }
            return instance;
        }
    }
    #endregion

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
        Destroy(gameObject);
    }
}
