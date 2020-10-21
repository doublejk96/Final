using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    #region Singleton
    private static Player instance;
    public static Player Ins
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<Player>();

                if (null == instance)
                {
                    Debug.LogError("Player Not Found");
                }
            }
            return instance;
        }
    }
    #endregion 

    private PlayerController controller;
    private Animator anim;

    [Header("Hp")]
    public float curHp;
    public float maxHp;

    [Header("etc")]
    public float MoveSpeed;

    void Start()
    {
        controller = GetComponent<PlayerController>();
        anim = GetComponent<Animator>();

        RestPlayer();
    }

    void Update()
    {
        FindEnemy();

        anim.SetFloat("AttackSpeed", GunManager.Ins.attackSpeed);
    }

    void RestPlayer()
    {
        curHp = maxHp;


    }

    void FindEnemy()
    {
        float disToClosestEnemy = Mathf.Infinity;

        Enemy closestEnemy = null;
        Enemy[] enemies = GameObject.FindObjectsOfType<Enemy>();
        foreach (Enemy curEnemy in enemies)
        {
            if (curEnemy.curHp > 0)
            {
                float disToCurEnemy = (curEnemy.transform.position - transform.position).sqrMagnitude;
                if (disToCurEnemy < disToClosestEnemy)
                {
                    disToClosestEnemy = disToCurEnemy;
                    closestEnemy = curEnemy;
                }
            }
        }

        if (controller.isMove == false)
        {
            if (closestEnemy != null)
            {             
                transform.LookAt(closestEnemy.transform);

                controller.Attack();
            }
        }        
        else if (closestEnemy == null)
        {
            return;
        }       
    }    
}
