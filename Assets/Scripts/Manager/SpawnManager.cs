using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    #region Singleton
    private static SpawnManager instance;
    public static SpawnManager Ins
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<SpawnManager>();

                if (null == instance)
                {
                    Debug.LogError("SpawnManager Not Found");
                }
            }
            return instance;
        }
    }
    #endregion

    private Door door;

    [Header("Enemy Prefab")]
    public List<GameObject> enemies;
    public List<GameObject> bosses;

    [Header("Spawn Enemy List")]
    public List<Enemy> enemyList;
    public List<Enemy> bossList;

    [Header("Spawn Count")]
    public float enemyCount;
    public float bossCount;

    void Start()
    {
        door = FindObjectOfType<Door>();
    }

    void Update()
    {
        if (door.isInDungeon == true)
        {
            if (enemyCount > 0)
            {
                SpawnEnemy();
            }
            else if (enemyList.Count == 0)
            {
                BossSpawn();
            }
        }        
    }    

    void SpawnEnemy()
    {                
        enemyCount--;

        int randomEnemy = Random.Range(0, enemies.Count);
        Vector3 randomPos = transform.position + new Vector3(Random.Range(-5, 5), 0, Random.Range(0, 5));
        GameObject enemyGo = Instantiate(enemies[randomEnemy], randomPos, Quaternion.identity);
        Enemy enemy = enemyGo.GetComponent<Enemy>();
        enemy.transform.parent = transform;
        enemyList.Add(enemy);
    }

    void BossSpawn()
    {
        if (bossCount > 0)
        {
            bossCount--;

            int randomBoss = Random.Range(0, bosses.Count);
            GameObject bossGo = Instantiate(bosses[randomBoss], transform.position, Quaternion.identity);
            Enemy boss = bossGo.GetComponent<Enemy>();
            boss.transform.parent = transform;
            bossList.Add(boss);
            bosses.RemoveAt(randomBoss);
        }        
    }
}
