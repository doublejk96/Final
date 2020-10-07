using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private PlayerController playerCon;
    private Animator anim;

    [Header("HP")]
    public int curHp;
    public int maxHp;

    [Header("Ammo")]
    public int curAmmo;
    public int maxAmmo;

    [Header("Reload")]
    public Transform mag;
    public Transform magPos;
    public float reloadTime;

    [Header("Bullet")]
    public Transform bullet;
    public Transform firePos;

    [Header("Shell")]
    public Transform shell;
    public Transform shellPos;

    [Header("Effect")]
    public GameObject fireEffect;
    public Sprite[] sprites;
    public SpriteRenderer[] renderers;
    public float effectTime;

    [Header("Fire Rate")]
    public float shotTime;
    public float nextShotTime;  

    void Start()
    {
        playerCon = GetComponent<PlayerController>();
        anim = GetComponent<Animator>();

        curHp = maxHp;
        curAmmo = maxAmmo;
    }

    void Update()
    {
        FindEnemy();

        shotTime -= Time.deltaTime;
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

        transform.LookAt(closestEnemy.transform);
        Debug.DrawLine(transform.position, closestEnemy.transform.position);

        if (closestEnemy != null)
        {
            if (playerCon.isMove == false)
            {
                Attack();
            }
        }
    }

    void Attack()
    {
        if (curAmmo > 0)
        {
            if (shotTime <= 0)
            {
                anim.SetTrigger("isFire");

                curAmmo--;

                Instantiate(bullet, firePos.position, firePos.rotation);
                Instantiate(shell, shellPos.position, shellPos.rotation);
                Activate();

                shotTime = nextShotTime;
            }
        }
        else if (curAmmo <= 0)
        {
            StartCoroutine(Reload());
        }        
    }        

    public void Activate()
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

    IEnumerator Reload()
    {
        shotTime = nextShotTime;
        anim.SetTrigger("isReloading");

        Instantiate(mag, magPos.position, magPos.rotation);

        yield return new WaitForSeconds(reloadTime);        

        curAmmo = maxAmmo;
    }

    public void OnDamage(int damage)
    {
        curHp -= damage;
        curHp = Mathf.Max(0, curHp);

        if (curHp <= 0)
        {
            Die();
        }
    }

    void Die()
    {

    }
}
