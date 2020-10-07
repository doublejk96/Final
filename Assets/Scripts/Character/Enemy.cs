﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Transform target;

    [Header("HP")]
    public int curHp;
    public int maxHp;

    void Start()
    {
        target = FindObjectOfType<Player>().transform;

        curHp = maxHp;
    }

    void Update()
    {
        LookPlayer();
    }

    void LookPlayer()
    {
        transform.LookAt(target.transform);
    }
}
