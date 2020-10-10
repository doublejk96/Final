using System.Collections;
using System.Collections.Generic;
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
    }

    public Wave[] waves;

    Wave currentWave;
    int curWaveNum;

    public List<Enemy> enemyList;

    public Enemy[] enemyPrefab;

    int RemainingSpawnEnemy; // 남아있는 소환될 적
    float nextSpawnTime; // 다음 스폰 시간

    MapGenerator map;

    void Start()
    {
        map = FindObjectOfType<MapGenerator>();

        NextWave();
    }

    void Update()
    {     
        EnemySpawn();
    }

    public void NextWave()
    {
        curWaveNum++;

        if (curWaveNum - 1 < waves.Length)
        {
            currentWave = waves[curWaveNum - 1];

            RemainingSpawnEnemy = currentWave.enemyCount;
        }
    }

    void EnemySpawn()
    {
        Transform randomTIle = map.GetRandomOpenTile();

        // 소환될 적이 0보다 많고, 현재 시간이 다음 스폰 시간보다 크다면
        if (RemainingSpawnEnemy > 0 && nextSpawnTime < Time.time)
        {
            for (int i = 0; i < enemyPrefab.Length; i++)
            {
                RemainingSpawnEnemy--;

                Enemy enemy = Instantiate(enemyPrefab[i], randomTIle.position, Quaternion.identity) as Enemy;
                enemy.transform.parent = transform;
                enemyList.Add(enemy);                
            }            
        }
    }
}
