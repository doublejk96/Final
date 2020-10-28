using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    #region Singleton
    private static MapGenerator instance;
    public static MapGenerator Ins
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<MapGenerator>();

                if (null == instance)
                {
                    Debug.LogError("MapGenerator Not Found");
                }
            }
            return instance;
        }
    }
    #endregion

    public Vector2 mapSize;

    public Transform tilePrefab;
    public Transform newTile;
    public Transform navemeshFloor;

    void Start()
    {
        GenerateMap();
    }

    public void GenerateMap()
    {
        GetComponent<BoxCollider>().size = new Vector3(mapSize.x, 0.05f, mapSize.y);

        string holderName = "Generate Map";
        if (transform.Find(holderName))
        {
            DestroyImmediate(transform.Find(holderName).gameObject);
        }        

        Transform mapHolder = new GameObject(holderName).transform;
        mapHolder.parent = transform;

        for (int x = 0; x < mapSize.x; x++)
        {
            for (int y = 0; y < mapSize.y; y++)
            {
                Vector3 newTilePos = new Vector3(-mapSize.x / 2 + 0.5f + x, 0, -mapSize.y / 2 + 0.5f + y);
                newTile = Instantiate(tilePrefab, newTilePos, Quaternion.Euler(Vector3.right * 90)) as Transform;
                newTile.localScale = Vector3.one;
                newTile.parent = mapHolder;
            }
        }

        navemeshFloor.localScale = new Vector3(mapSize.x, mapSize.y);
    }
}
