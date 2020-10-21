using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    #region Singleton
    private static Player instance;
    public static Player Ins
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<Player>();

                if (null == instance)
                {
                    Debug.LogError("Player Not Found");
                }
            }
            return instance;
        }
    }
    #endregion 

    private PlayerController controller;

    [Header("Hp")]
    public float curHp;
    public float maxHp;

    [Header("etc")]
    public float MoveSpeed;

    public void Init()
    {
        controller = GetComponent<PlayerController>();

        RestPlayer();
    }

    void RestPlayer()
    {
        curHp = maxHp;
    }
}
