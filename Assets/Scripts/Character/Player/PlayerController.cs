using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Animator anim;
    private FixedJoystick joystick;

    [Header("Move")]
    public float speed;
    public bool isMove;
    private Vector3 velocity;

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
        if (joystick == null)
        {
            Debug.Log("Joystickis Null");
        }

        float x = joystick.Horizontal;
        float z = joystick.Vertical;

        velocity = new Vector3(x, 0, z) * speed * Time.deltaTime;

        transform.position += velocity;
        transform.LookAt(transform.position + velocity);

        if (x == 0 && z == 0)
        {
            isMove = false;
            anim.SetBool("isMove", false);
        }
        else
        {
            isMove = true;
            anim.SetBool("isMove", true);
        }
    }
}
