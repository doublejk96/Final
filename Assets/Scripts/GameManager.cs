using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region Singleton
    private static GameManager instance;
    public static GameManager Ins
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
    #endregion

    [Header("Enemy")]
    public List<GameObject> enemies;

    [Header("Spawn Enemy")]
    public List<Enemy> enemyList;
    public float enemyCount;

    void Start()
    {
        Player.Ins.Init();
    }

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
