using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private PlayerController playerCon;
    private Animator anim;
    private CameraOption cam;

    [Header("HP")]
    public float curHp;
    public float maxHp;

    [Header("Bullet")]
    public Transform bulletPrefab;
    public Transform firePos;

    [Header("Shell")]
    public Transform shellPrefab;
    public Transform shellPos;

    [Header("Effect")]
    public Vector3 offset;
    public GameObject fireEffect;    
    public GameObject hitEffect;
    public GameObject hitLight;
    private float effectTime = 0.05f;

    [Header("Fire Rate")]
    public float shotTime;
    public float nextShotTime;  

    void Start()
    {
        playerCon = GetComponent<PlayerController>();
        anim = GetComponent<Animator>();
        cam = GameObject.FindWithTag("MainCamera").GetComponent<CameraOption>();

        curHp = maxHp;
    }

    void Update()
    {
        shotTime -= Time.deltaTime;

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
            return;
        }     
        else if (closestEnemy != null)
        {
            if (playerCon.isMove == false)
            {
                transform.LookAt(closestEnemy.transform);
                Debug.DrawLine(transform.position, closestEnemy.transform.position);

                Attack();
            }
        }
    }

    void Attack()
    {
        if (shotTime <= 0)
        {
            anim.SetTrigger("isFire");

            cam.VibrateTime(0.1f, 0.05f);
            
            Instantiate(bulletPrefab, firePos.position, firePos.rotation);
            Instantiate(shellPrefab, shellPos.position, shellPos.rotation);
            FireEffectOn();
            
            shotTime = nextShotTime;
        }  
    }        

    void FireEffectOn()
    {
        fireEffect.SetActive(true);

        /*
        int index = Random.Range(0, sprites.Length);
        for (int i = 0; i < renderers.Length; i++)
        {
            renderers[i].sprite = sprites[index];
        }
        */

        Invoke("FireEffectOff", effectTime);
    }

    void FireEffectOff()
    {
        fireEffect.SetActive(false);        
    }

    public void OnDamage(float damage)
    {
        curHp -= damage;
        curHp = Mathf.Max(0, curHp);

        cam.VibrateTime(0.1f, 0.1f);

        Vector3 effectPos = transform.position + offset;
        Instantiate(hitEffect, effectPos, Quaternion.identity);

        if (curHp <= 0)
        {
            Die();
        }
    }

    void Die()
    {

    }
}
