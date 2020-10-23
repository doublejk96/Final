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
    
    private Animator anim;

    [Header("Hp")]
    public float curHp;
    public float maxHp;

    [Header("Speed")]
    public float MoveSpeed;
    public float attackSpeed;
    public float reloadSpeed;    

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        AnimationSpeed();
    }

    public void RestPlayer()
    {
        curHp = maxHp;
    }

    void AnimationSpeed()
    {
        anim.SetFloat("AttackSpeed", attackSpeed);
        anim.SetFloat("ReloadSpeed", reloadSpeed);
    }

       
}
