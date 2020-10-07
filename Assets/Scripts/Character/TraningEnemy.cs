using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TraningEnemy : Enemy
{
    void Start()
    {
        init();
    }

    void Update()
    {
        curHp = maxHp;
    }
}
