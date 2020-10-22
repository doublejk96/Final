using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    public Vector2 mapSize;

    public Transform tilePrefab;

    [Range(0, 1)]
    public float outLinePercent;

    public void GenerateMap()
    {
        // 콜라이더 생성
        GetComponent<BoxCollider>().size = new Vector3(mapSize.x, 0.05f, mapSize.y);

        string parentName = "Generate Map";

        if (transform.Find(parentName))
        {
            DestroyImmediate(transform.Find(parentName).gameObject);
        }

        Transform mapHolder = new GameObject(parentName).transform;
        mapHolder.parent = transform;

        for (int x = 0; x < mapSize.x; x++)
        {
            for (int y = 0; y < mapSize.y; y++)
            {
                Vector3 newTilePos = new Vector3(-mapSize.x / 2f + 0.5f + x, 0, -mapSize.y / 2 + 0.5f + y);
                Transform newTile = Instantiate(tilePrefab, newTilePos, Quaternion.Euler(Vector3.right * 90)) as Transform;
                newTile.parent = mapHolder;
                newTile.localScale = Vector3.one;
            }
        }
    }
}
