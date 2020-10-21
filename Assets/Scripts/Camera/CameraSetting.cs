using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class CameraSetting : MonoBehaviour
{
    private Transform target;

    public Vector3 offset;

    private float shakeAmount;
    private float shakeTime;

    void Start()
    {
        target = FindObjectOfType<Player>().transform;
    }

    void FixedUpdate()
    {
        Vector3 camPos = target.transform.position + offset;

        transform.position = camPos;

        if (shakeTime > 0)
        {
            transform.position = Random.insideUnitSphere * shakeTime + camPos;

            shakeTime -= Time.deltaTime;
        }
        else
        {
            transform.position = camPos;

            shakeTime = 0f;
        }
    }

    public void ShakeCamera(float time, float amount)
    {
        if (target == null)
        {
            return;
        }

        shakeTime = time;
        shakeAmount = amount;
    }
}
