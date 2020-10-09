using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraOption : MonoBehaviour
{
    private Transform target;

    [Header("Shake")]
    public float ShakeAmount;
    private float ShakeTime;

    [Header("Position")]
    public Vector3 offset;   

    void Start()
    {
        target = FindObjectOfType<Player>().transform;        
    }

    public void VibrateTime(float time)
    {
        ShakeTime = time;
    }

    void Update()
    {
        Vector3 camPos = target.transform.position + offset;

        transform.position = camPos;

        if (ShakeTime > 0)
        {
            transform.position = Random.insideUnitSphere * ShakeAmount + camPos;
            ShakeTime -= Time.deltaTime;
        }
        else
        {
            ShakeTime = 0f;
            transform.position = camPos;
        }
    }    
}
