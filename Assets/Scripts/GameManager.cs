using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    public static GameManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<GameManager>();

                if (null == instance)
                {
                    Debug.LogError("GameManager Not Found");
                }
            }
            return instance;
        }
    }

    [Header("Portal")]
    public GameObject portal;

    [Header("Wave")]
    public int enemyCount;
    public float spawnDelay;

    private float nextSpawnTime;
    
    private int aliveEnemy;

    public List<Enemy> enemyList;
    public Enemy enemyPrefab;    

    void Start()
    {
        aliveEnemy = enemyCount;
    }

    void Update()
    {        
        EnemySpawn();
    }

    void EnemySpawn()
    {
        if (enemyCount > 0 && nextSpawnTime < Time.time)
        {
            Enemy enemy = Instantiate(enemyPrefab, transform.position, Quaternion.identity);
            enemyCount--;
            nextSpawnTime = Time.time + spawnDelay;

            enemy.transform.parent = transform;
            enemy.OnDie += EnemyDie;

            enemyList.Add(enemy);
        }        
    }

    void EnemyDie()
    {
        aliveEnemy--;
        if (aliveEnemy == 0)
        {
            Invoke("OpenPortal", 1);
        }
    }

    void OpenPortal()
    {
        portal.SetActive(true);
    }
}
