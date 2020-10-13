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
    public int curWaveNum;

    public List<Enemy> enemyList;   
    public List<Enemy> bossList;

    public List<GameObject> enemyPrefab;
    public List<GameObject> bossPrefab;

    int RemainingSpawnEnemy;
    int RemainingAliveEnemy;

    private MapGenerator map;

    void Start()
    {
        map = FindObjectOfType<MapGenerator>();
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
        else if (curWaveNum - 1 == waves.Length)
        {          
            BossSpawn();
        }
    }    

    void EnemySpawn()
    {
        if (RemainingSpawnEnemy > 0)
        {
            RemainingSpawnEnemy--;

            StartCoroutine(SpawnEnemy());
        }        
    }

    IEnumerator SpawnEnemy()
    {
        float spawnDelay = 1;
        float tileFlashSpeed = 4;

        Transform randomTile = map.GetRandomOpenTile();

        Material tileMat = randomTile.GetComponent<Renderer>().material;
        Color initialColor = tileMat.color;
        Color flashColor = Color.red;
        float spawnTimer = 0;

        while (spawnTimer < spawnDelay)
        {
            tileMat.color = Color.Lerp(initialColor, flashColor, Mathf.PingPong(spawnTimer * tileFlashSpeed, 1));

            spawnTimer += Time.deltaTime;
            yield return null;
        }

        int i = Random.Range(0, enemyPrefab.Count);
        GameObject enemyGo = Instantiate(enemyPrefab[i], randomTile.position, Quaternion.identity);

        Enemy enemy = enemyGo.GetComponent<Enemy>();
        enemy.transform.parent = transform;
        enemyList.Add(enemy);
        enemy.OnDie += EnemyDie;
    }

    void BossSpawn()
    {
        Transform randomTIle = map.GetRandomOpenTile();       
            
        int i = Random.Range(0, bossPrefab.Count);

        GameObject bossGo = Instantiate(bossPrefab[i], randomTIle.position, Quaternion.identity);
        bossPrefab.RemoveAt(i);

        Enemy boss = bossGo.GetComponent<Enemy>();
        boss.transform.parent = transform;
        bossList.Add(boss);
        boss.OnDie += BossDie;
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
        Debug.Log("클리어");
    }
}
