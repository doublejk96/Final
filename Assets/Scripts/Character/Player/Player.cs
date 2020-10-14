using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using UnityEngine;

public class Player : Character
{
    private PlayerController playerCon;    
    private CameraOption cam;    

    [Header("Effect")]
    public Vector3 offset;       
    public GameObject hitEffect;

    void Start()
    {
        playerCon = GetComponent<PlayerController>();
        
        cam = GameObject.FindWithTag("MainCamera").GetComponent<CameraOption>();

        Init();
    }

    public override void Update()
    {
        base.Update();

        FindEnemy();
    }

    void FindEnemy()
    {
        float disToClosestEnemy = Mathf.Infinity;
        
        Enemy closestEnemy = null;
        Enemy[] enemies = GameObject.FindObjectsOfType<Enemy>();
        foreach (Enemy currentEnemy in enemies)
        {
            if (currentEnemy.curHp > 0)
            {
                float disToEnemy = (currentEnemy.transform.position - transform.position).sqrMagnitude;
                if (disToEnemy < disToClosestEnemy)
                {
                    disToClosestEnemy = disToEnemy;
                    closestEnemy = currentEnemy;
                }
            }
        }

        if (closestEnemy == null)
        {
            anim.SetBool("Aiming", false);
            return;
        }     
        else if (closestEnemy != null)
        {
            if (playerCon.isMove == false)
            {
                transform.LookAt(closestEnemy.transform);
                anim.SetBool("Aiming", true);
                Debug.DrawLine(transform.position, closestEnemy.transform.position);

                Attack();
            }
        }
    }

    public override void Attack()
    {
        base.Attack();

        if (attackTime <= 0)
        {
            anim.SetTrigger("isAttack");

            Instantiate(bulletPrefab, firePos.position, firePos.rotation);
            Instantiate(shellPrefab, shellPos.position, shellPos.rotation);

            FireEffectOn();

            attackTime = nextAttackTime;
        }
    }

    void FireEffectOn()
    {
        muzzleFire.SetActive(true);

        Invoke("FireEffectOff", 0.05f);
    }

    void FireEffectOff()
    {
        muzzleFire.SetActive(false);
    }

    public override void OnDamage(float damage)
    {
        base.OnDamage(damage);

        cam.VibrateTime(0.1f, 0.1f);

        Vector3 effectPos = transform.position + offset;
        Instantiate(hitEffect, effectPos, Quaternion.identity);
    }
}
