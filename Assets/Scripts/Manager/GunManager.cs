using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunManager : MonoBehaviour
{
    #region Singleton
    private static GunManager instance;
    public static GunManager Ins
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<GunManager>();

                if (null == instance)
                {
                    Debug.LogError("GunManager Not Found");
                }
            }
            return instance;
        }
    }
    #endregion

    public Transform FirePos;
    public GameObject bulletPrefab;

    [Header("Bullet Power")]
    public float damage;
    public float speed;

    [Header("Fire Rate")]
    public float fireTime;
    public float attackDelay;

    void Start()
    {
        fireTime = attackDelay;
    }

    void Update()
    {
        fireTime -= Time.deltaTime;
        fireTime = Mathf.Max(0, fireTime);
    }
}
