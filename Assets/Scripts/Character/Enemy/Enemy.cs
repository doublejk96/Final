using System.Collections;
using System.Collections.Generic;
using System.Runtime.Remoting.Messaging;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public event System.Action OnDie;

    private Animator anim;

    private Transform target;

    [Header("HP")]
    public float curHp;
    public float maxHp;

    [Header("Bullet")]
    public Transform bulletPrefab;
    public Transform firePos;

    [Header("Shell")]
    public Transform shellPrefab;
    public Transform shellPos;

    [Header("Fire Rate")]
    public float shotTime;
    public float nextShotTime;

    [Header("Effect")]
    public GameObject fireEffect;
    public GameObject hitEffect;
    public GameObject dieEffect;
    private float effectTime = 0.05f;

    private bool die;

    void Start()
    {
        anim = GetComponent<Animator>();
        target = FindObjectOfType<Player>().transform;

        curHp = maxHp;
    }

    void Update()
    {
        shotTime -= Time.deltaTime;

        if (die == false)
        {
            LookPlayer();
            Attack();
        }
    }

    void LookPlayer()
    {
        transform.LookAt(target.transform);
    }

    void Attack()
    {
        if (shotTime <= 0)
        {            
            anim.SetTrigger("isFire");

            Instantiate(bulletPrefab, firePos.position, firePos.rotation);
            Instantiate(shellPrefab, shellPos.position, shellPos.rotation);
            Activate();

            shotTime = nextShotTime;
        }
    }

    void Activate()
    {
        fireEffect.SetActive(true);

        /*
        int index = Random.Range(0, sprites.Length);
        for (int i = 0; i < renderers.Length; i++)
        {
            renderers[i].sprite = sprites[index];
        }
        */

        Invoke("Deactivate", effectTime);
    }

    void Deactivate()
    {
        fireEffect.SetActive(false);
    }

    public void OnDamage(float damage)
    {
        curHp -= damage;
        curHp = Mathf.Max(0, curHp);
                
        SlowTime();

        if (curHp <= 0)
        {
            Die();
        }
    }

    void SlowTime()
    {
        Time.timeScale = 0.1f;

        Instantiate(hitEffect, transform.position, Quaternion.identity);

        Invoke("TimeReturn", 0.011f);
    }

    void TimeReturn()
    {
        Time.timeScale = 1;
    }

    public void Die()
    {
        if (OnDie != null)
        {
            OnDie();
        }

        die = true;

        anim.SetTrigger("isDie");  

        Invoke("DieEffect", 0.89f);
        Destroy(gameObject, 0.89f);

        GameManager.Instance.enemyList.Remove(this);
    }

    public void FallDie()
    {
        if (OnDie != null)
        {
            OnDie();
        }

        die = true;

        DieEffect();
        Destroy(gameObject);
    }

    void DieEffect()
    {
        Instantiate(dieEffect, transform.localPosition, Quaternion.identity);
    }

}
