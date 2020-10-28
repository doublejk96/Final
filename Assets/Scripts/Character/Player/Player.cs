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
    private Renderer render;

    public Rigidbody rigid;
    public CameraSetting cam;

    [Header("Hp")]
    public float curHp;
    public float maxHp;

    [Header("Speed")]
    public float MoveSpeed;
    public float attackSpeed;
    public float reloadSpeed;

    [Header("Effect")]
    public Vector3 effectOffset;
    public GameObject hitEffect;

    void Start()
    {
        anim = GetComponent<Animator>();
        render = GetComponentInChildren<Renderer>();
        rigid = GetComponent<Rigidbody>();

        cam = FindObjectOfType<CameraSetting>();
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
        anim.SetFloat("attackSpeed", attackSpeed);
        anim.SetFloat("reloadSpeed", reloadSpeed);
    }

    public void OnDamage(float damage)
    {
        curHp -= damage;
        curHp = Mathf.Max(0, curHp);

        cam.ShakeCamera(0.15f, 0.5f);
        Instantiate(hitEffect, transform.position + effectOffset, transform.rotation);

        if (curHp <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        anim.SetTrigger("isDie");

        Invoke("GameoverScene", 2f);
    }

    void GameoverScene()
    {
        UI_Manager.Ins.Show(UI_ID.GAMEOVER, true);
    }
}
