using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    private Transform target;

    public Vector3 offset;

    void Start()
    {
        target = FindObjectOfType<Player>().transform;
    }

    void FixedUpdate()
    {
        Vector3 camPos = target.position + offset;

        transform.position = camPos;
    }
}
