﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [Header("Enemy")]
    public List<GameObject> enemies;

    [Header("Spawn Enemy")]
    public List<Enemy> enemyList;
    public float enemyCount;

    void Update()
    {
        EenemySpawn();
    }

    void EenemySpawn()
    {
        if (enemyCount > 0)
        {
            enemyCount--;

            int rand = Random.Range(0, enemies.Count);
            GameObject enemyGo = Instantiate(enemies[rand], transform.position, Quaternion.identity);
            Enemy enemy = enemyGo.GetComponent<Enemy>();
            enemy.transform.parent = transform;
            enemyList.Add(enemy);
        }
    }
}