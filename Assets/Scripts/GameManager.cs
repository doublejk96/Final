using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Runtime.Remoting.Messaging;
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

    [System.Serializable]
    public class Wave
    {
        public int enemyCount;
        public int bossCount;
    }

    public Wave[] waves;

    Wave currentWave;
    int curWaveNum;

    public List<Enemy> enemyList;
    public Enemy[] enemyPrefab;

    public List<Enemy> bossList;
    public Enemy[] bossPrefab;

    int RemainingSpawnEnemy;
    int RemainingAliveEnemy;

    int RemainingSpawnBoss;
    int RemainingAliveBoss;

    private MapGenerator map;

    void Start()
    {
        map = FindObjectOfType<MapGenerator>();

        WaveStart();
    }

    void Update()
    {
        EnemySpawn();
    }


    public void WaveStart()
    {
        curWaveNum++;

        if (curWaveNum - 1 < waves.Length)
        {
            currentWave = waves[curWaveNum - 1];

            RemainingSpawnEnemy = currentWave.enemyCount;
            RemainingAliveEnemy = RemainingSpawnEnemy;
        }
        else if (curWaveNum - 1 >= waves.Length)
        {
            RemainingSpawnBoss = currentWave.bossCount;
            RemainingAliveBoss = RemainingSpawnBoss;

            BossSpawn();
        }
    }    

    void EnemySpawn()
    {
        Transform randomTIle = map.GetRandomOpenTile();

        if (RemainingSpawnEnemy > 0)
        {
            for (int i = 0; i < enemyPrefab.Length; i++)
            {
                RemainingSpawnEnemy--;

                Enemy enemy = Instantiate(enemyPrefab[i], randomTIle.position, Quaternion.identity) as Enemy;
                enemy.transform.parent = transform;
                enemyList.Add(enemy);
                enemy.OnDie += EnemyDie;
            }            
        }
    }

    void BossSpawn()
    {
        Transform randomTIle = map.GetRandomOpenTile();

        if (RemainingSpawnBoss > 0)
        {
            for (int i = 0; i < bossPrefab.Length; i++)
            {
                RemainingSpawnBoss--;

                Enemy boss = Instantiate(bossPrefab[i], randomTIle.position, Quaternion.identity) as Enemy;
                boss.transform.parent = transform;
                enemyList.Add(boss);
                boss.OnDie += BossDie;
            }
        }
    }

    void EnemyDie()
    {        
        RemainingAliveEnemy--;
        if (RemainingAliveEnemy == 0)
        {
            Invoke("WaveStart", 1f);
        }
    }

    void BossDie()
    {
        RemainingAliveBoss--;
        if (RemainingAliveBoss == 0)
        {
            Debug.Log("클리어");
        }
    }
}
