using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour
{
    #region Singleton
    private static GunController instance;
    public static GunController Ins
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<GunController>();

                if (null == instance)
                {
                    Debug.LogError("GunController Not Found");
                }
            }
            return instance;
        }
    }
    #endregion

    public Transform FirePos;    
    public GameObject bulletPrefab;

    public Transform shellPos;
    public GameObject shellPrefab;

    [Header("Bullet Power")]
    public float damage;
    public float speed;

    [Header("Fire Rate")]
    public float fireTime;
    public float smgAttackDelay;

    [Header("Ammo")]
    public float curAmmo;
    public float maxAmmo;

    void Start()
    {
        fireTime = smgAttackDelay / Player.Ins.attackSpeed;
        curAmmo = maxAmmo;
    }

    void Update()
    {
        fireTime -= Time.deltaTime;
        fireTime = Mathf.Max(0, fireTime);
    }
}
