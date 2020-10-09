using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitLight : MonoBehaviour
{
    Player player;
    Animator anim;

    void Start()
    {
        player = FindObjectOfType<Player>();
        anim = GetComponent<Animator>();
    }
}
