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
    public int bossCount;
    public float spawnDelay;

    private float nextSpawnTime;
    
    private int aliveEnemy;
    private int aliveBoss;

    public List<Enemy> enemyList;
    public List<Enemy> bossList;
    public Enemy enemyPrefab;    
    public Enemy bossPrefab;

    void Start()
    {
        aliveEnemy = enemyCount;
        aliveBoss = bossCount;
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
            Invoke("BossSpawn", 1);
        }
    }

    void BossSpawn()
    {
        if (bossCount > 0 && nextSpawnTime < Time.time)
        {
            Enemy boss = Instantiate(bossPrefab, transform.position, Quaternion.identity);
            bossCount--;
            nextSpawnTime = Time.time + spawnDelay;

            boss.transform.parent = transform;
            boss.OnDie += BossDie;

            enemyList.Add(boss);
        }
    }

    void BossDie()
    {
        aliveBoss--;
        if (aliveBoss == 0)
        {
            Invoke("OpenPortal", 1);
        }
    }

    void OpenPortal()
    {
        portal.SetActive(true);
    }
}
