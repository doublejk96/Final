using System.Collections;
using System.Collections.Generic;
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

    [Header("Fire Rate")]
    public float shotTime;
    public float nextShotTime;

    [Header("Effect")]
    public GameObject hitEffect;
    public GameObject dieEffect;

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

            shotTime = nextShotTime;
        }
    }

    public void OnDamage(float damage)
    {
        curHp -= damage;
        curHp = Mathf.Max(0, curHp);

        Instantiate(hitEffect, transform.position, Quaternion.identity);

        SlowTime();

        if (curHp <= 0)
        {
            Die();
        }
    }

    void SlowTime()
    {
        Time.timeScale = 0.1f;

        Invoke("Deactivate", 0.011f);
    }

    void Deactivate()
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

    void DieEffect()
    {
        Instantiate(dieEffect, transform.localPosition, Quaternion.identity);
    }

}
