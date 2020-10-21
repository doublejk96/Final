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

        float nextFireTime = GunManager.Ins.attackDelay;
        if (isMove == false)
        {
            if (GunManager.Ins.fireTime <= 0)
            {
                anim.SetTrigger("isFire");

                GunManager.Ins.fireTime = nextFireTime;
            }            
        }
    }    

    void Move()
    {
        float x = joystick.Horizontal;
        float z = joystick.Vertical;

        float speed = Player.Ins.MoveSpeed;

        if (x == 0 && z == 0)
        {
            isMove = false;
            anim.SetBool("isRun", false);
        }
        else
        {
            isMove = true;
            anim.SetBool("isRun", true);
        }

        Vector3 velocity = new Vector3(x, 0, z) * speed * Time.deltaTime;

        transform.position += velocity;
        transform.LookAt(transform.position + velocity);
    }

    public void Attack()
    {
        GameObject bullet = GunManager.Ins.bulletPrefab;
        Transform firePos = GunManager.Ins.FirePos;
        Instantiate(bullet, firePos.position, firePos.rotation);        
    }
}
