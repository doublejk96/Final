using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    #region Singleton
    private static PlayerController instance;
    public static PlayerController Ins
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<PlayerController>();

                if (null == instance)
                {
                    Debug.LogError("PlayerController Not Found");
                }
            }
            return instance;
        }
    }
    #endregion 

    private Animator anim;
    private FixedJoystick joystick;

    public bool isMove = false;
    public bool isReload = false;

    void Start()
    {
        anim = GetComponent<Animator>();
        joystick = FindObjectOfType<FixedJoystick>();
    }

    void FixedUpdate()
    {
        Move();
        FindEnemy();
        ReloadAnim();
    }  

    void Move()
    {
        float x = joystick.Horizontal;
        float z = joystick.Vertical;

        float speed = Player.Ins.MoveSpeed;

        if (Player.Ins.curHp > 0)
        {
            if (isReload == false)
            {
                Vector3 velocity = new Vector3(x, 0, z) * speed * Time.deltaTime;

                if (x == 0 && z == 0)
                {
                    isMove = false;
                    anim.SetBool("isRun", false);
                }
                else
                {
                    isMove = true;
                    anim.SetBool("isRun", true);

                    transform.position += velocity;
                    transform.LookAt(transform.position + velocity);
                }
            }            
        }        
    }

    void MoveSound()
    {
        SoundManager.Ins.Play("Player Move Sound");
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

        if (isMove == false)
        {
            if (closestEnemy != null)
            {
                transform.LookAt(closestEnemy.transform);

                Attack();
            }
        }
        else if (closestEnemy == null)
        {
            return;
        }
    }

    public void Attack()
    {        
        if (isMove == false)
        {           
            if (GunController.Ins.fireTime <= 0)
            {
                if (GunController.Ins.curAmmo > 0)
                {               
                    anim.SetTrigger("isFire");

                    float nextAttack = (GunController.Ins.smgAttackDelay / Player.Ins.attackSpeed);
                    GunController.Ins.fireTime = nextAttack;
                }
            }                    
        }
    }

    void Fire()
    {
        Player.Ins.cam.ShakeCamera(0.1f, 0.1f);

        GunController.Ins.curAmmo--;
        SoundManager.Ins.Play("Player Fire Sound");

        GameObject bullet = GunController.Ins.bulletPrefab;
        Transform firePos = GunController.Ins.FirePos;
        Instantiate(bullet, firePos.position, firePos.rotation);

        GameObject shell = GunController.Ins.shellPrefab;
        Transform shellPos = GunController.Ins.shellPos;
        Instantiate(shell, shellPos.position, shellPos.rotation);
    }

    void ReloadAnim()
    {
        if (isReload == false)
        {
            if (GunController.Ins.curAmmo == 0)
            {
                isReload = true;
                anim.SetTrigger("isReload");
            }
        }        
    }

    void Reload()
    {
        isReload = false;
        GunController.Ins.curAmmo = GunController.Ins.maxAmmo;        
    }
}
