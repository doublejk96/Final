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

    [Header("Spawn")]
    public int enemyCount;
    public float spawnDelay;
    private float nextSpawnTime;

    public List<Enemy> enemyList;
    public Enemy enemyPrefab;    

    void Start()
    {

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

            enemyList.Add(enemy);
        }        
    }
}
