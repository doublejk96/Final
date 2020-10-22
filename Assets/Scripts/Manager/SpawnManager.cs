using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [Header("Enemy")]
    public List<GameObject> enemies;

    [Header("Spawn Enemy")]
    public List<Enemy> enemyList;

    [Header("Spawn Transform")]
    public List<Transform> transformList;

    public int enemyCount;

    void Update()
    {
        EenemySpawn();
    }

    void EenemySpawn()
    {
        if (enemyCount > 0)
        {
            enemyCount--;

            int randomEnemy = Random.Range(0, enemies.Count);
            int radnomTransform = Random.Range(0, transformList.Count);
            GameObject enemyGo = Instantiate(enemies[randomEnemy], transformList[radnomTransform].position, Quaternion.identity);
            Enemy enemy = enemyGo.GetComponent<Enemy>();
            enemy.transform.parent = transform;
            enemyList.Add(enemy);
        }
    }
}
