using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Animator anim;
    private FixedJoystick joystick;

    public bool isMove = false;
    public bool isFire = false;

    void Start()
    {
        anim = GetComponent<Animator>();
        joystick = FindObjectOfType<FixedJoystick>();
    }

    void FixedUpdate()
    {
        Move();

        
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
                anim.SetTrigger("isFire");

                GunManager.Ins.fireTime = GunManager.Ins.attackDelay;
            }
        }
    }

    void Fire()
    {
        GameObject bullet = GunManager.Ins.bulletPrefab;
        Transform firePos = GunManager.Ins.FirePos;
        Instantiate(bullet, firePos.position, firePos.rotation);        
    }
}
