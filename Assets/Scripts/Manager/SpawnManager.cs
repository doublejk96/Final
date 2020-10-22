using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.WSA.Input;

public class SpawnManager : MonoBehaviour
{
    private MapGenerator map;

    [Header("Enemy")]
    public List<GameObject> enemies;

    [Header("Spawn Enemy")]
    public List<Enemy> enemyList;

    public int enemyCount;

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
}
