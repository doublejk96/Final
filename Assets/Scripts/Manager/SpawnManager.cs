using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    private MapGenerator map;

    [Header("Enemy")]
    public List<GameObject> enemies;
    public List<GameObject> bosses;

    [Header("Spawn Enemy")]
    public List<Enemy> enemyList;
    public List<Enemy> bossList;

    public float enemyCount;
    public float bossCount;

    void Start()
    {
        map = FindObjectOfType<MapGenerator>();
    }

    void Update()
    {
        EenemySpawn();
    }

    void EenemySpawn()
    {
        if (enemyCount > 0)
        {
            SpawnEnemy();
        }
        
        if (enemyList.Count <= 0)
        {
            BossSpawn();
        }
    }

    void SpawnEnemy()
    {
        enemyCount--;

        Transform randomTile = map.GetRandomOpenTile();        
        int randomEnemy = Random.Range(0, enemies.Count);
        GameObject enemyGo = Instantiate(enemies[randomEnemy], randomTile.position, Quaternion.identity);
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
