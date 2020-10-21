using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Animator anim;
    private FixedJoystick joystick;

    public bool isMove = false;

    void Start()
    {
        anim = GetComponent<Animator>();
        joystick = FindObjectOfType<FixedJoystick>();
    }

    void FixedUpdate()
    {
        Move();
        Reload();        
    }    

    void Move()
    {
        float x = joystick.Horizontal;
        float z = joystick.Vertical;

        float speed = Player.Ins.MoveSpeed;
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

    public void Attack()
    {        
        if (isMove == false)
        {
            if (GunManager.Ins.fireTime <= 0)
            {
                if (GunManager.Ins.curAmmo > 0)
                {
                    GunManager.Ins.curAmmo--;

                    anim.SetTrigger("isFire");

                    float nextAttack = (GunManager.Ins.attackDelay / GunManager.Ins.attackSpeed);
                    GunManager.Ins.fireTime = nextAttack;
                }                
            }
        }
    }

    void Fire()
    {
        GameObject bullet = GunManager.Ins.bulletPrefab;
        Transform firePos = GunManager.Ins.FirePos;
        Instantiate(bullet, firePos.position, firePos.rotation);        
    }

    void Reload()
    {
        if (GunManager.Ins.curAmmo == 0)
        {
            GunManager.Ins.curAmmo = GunManager.Ins.maxAmmo;
        }
    }
}
