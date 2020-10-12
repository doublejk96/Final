using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.AI;

public class MapGenerator : MonoBehaviour
{
    public Map[] maps;
    public int mapIndex;

    public Vector2 maxMapSize;
    public Transform tilePrefab;
    public Transform[] obstaclePrefab;
    public Transform navmeshMaskPrefab;
    public Transform navmeshFloor;    

    [Range(0,1)]
    public float outLine;

    public float tileSize;

    List<Coord> allTileCoords;
    Queue<Coord> shuffleTileCoords;
    Queue<Coord> shuffleOpenTileCoords;
    Transform[,] tileMap;

    Map curMap;

    void Start()
    {
        GeneratorMap();
    }

    public void GeneratorMap()
    {
        curMap = maps[mapIndex];
        tileMap = new Transform[curMap.mapSize.x, curMap.mapSize.y];
        System.Random prng = new System.Random(curMap.seed);
        GetComponent<BoxCollider>().size = new Vector3(curMap.mapSize.x * tileSize, 0.1f, curMap.mapSize.y * tileSize);

        // Coord 생성
        allTileCoords = new List<Coord>();
        for (int x = 0; x < curMap.mapSize.x; x++)
        {
            for (int y = 0; y < curMap.mapSize.y; y++)
            {
                allTileCoords.Add(new Coord(x, y));
            }
        }
        shuffleTileCoords = new Queue<Coord>(Utility.ShuffleArray(allTileCoords.ToArray(), curMap.seed));

        // 맵 홀더 오브젝트 생성
        string holderName = "Generatre Map";
        if (transform.Find(holderName))
        {
            DestroyImmediate(transform.Find(holderName).gameObject);
        }

        Transform mapHolder = new GameObject(holderName).transform;
        mapHolder.parent = transform;

        // 타일 생성
        for (int x = 0; x < curMap.mapSize.x; x++)
        {
            for (int y = 0; y < curMap.mapSize.y; y++)
            {
                Vector3 tilePos = CoordToPos(x, y);
                Transform newTile = Instantiate(tilePrefab, tilePos, Quaternion.Euler(Vector3.right * 90)) as Transform;
                newTile.localScale = Vector3.one * (1 - outLine) * tileSize;
                newTile.parent = mapHolder;
                tileMap[x, y] = newTile;
            }
        }

        // 장애물 생성
        bool[,] obstacleMap = new bool[(int)curMap.mapSize.x, (int)curMap.mapSize.y];

        int obstacleCount = (int)(curMap.mapSize.x * curMap.mapSize.y * curMap.obstaclePercent);
        int curObstacleCount = 0;
        List<Coord> allOpenCoords = new List<Coord>(allTileCoords);

        for (int i = 0; i < obstacleCount; i++)
        {
            Coord randomCoord = GetRandomCoord();
            obstacleMap[randomCoord.x, randomCoord.y] = true;
            curObstacleCount++;

            if (randomCoord != curMap.mapCenter && MapIsFullAccessible(obstacleMap, curObstacleCount))
            {
                Vector3 obstaclePos = CoordToPos(randomCoord.x, randomCoord.y);
                int rand = Random.Range(0, obstaclePrefab.Length);
                Transform newObstacle = Instantiate(obstaclePrefab[rand], obstaclePos + Vector3.up * 0.5f, Quaternion.identity) as Transform;
                newObstacle.localScale = Vector3.one * (1 - outLine) * tileSize;
                newObstacle.parent = mapHolder;

                Renderer obstacleRenderer = newObstacle.GetComponent<Renderer>();
                Material obstacleMaterial = new Material(obstacleRenderer.sharedMaterial);
                float ColorPercent = randomCoord.y / (float)curMap.mapSize.y;
                obstacleMaterial.color = Color.Lerp(curMap.forwardColor, curMap.backColor, ColorPercent);
                obstacleRenderer.sharedMaterial = obstacleMaterial;

                allOpenCoords.Remove(randomCoord);
            }
            else
            {
                obstacleMap[randomCoord.x, randomCoord.y] = false;
                curObstacleCount--;
            }
        }

        shuffleOpenTileCoords = new Queue<Coord>(Utility.ShuffleArray(allOpenCoords.ToArray(), curMap.seed));

        // Navmesh Mask 생성
        Transform maskLeft = Instantiate(navmeshMaskPrefab, (Vector3.left * (curMap.mapSize.x + maxMapSize.x) / 4f) + Vector3.up * 0.5f * tileSize, Quaternion.identity) as Transform;
        maskLeft.parent = mapHolder;
        maskLeft.localScale = new Vector3((maxMapSize.x - curMap.mapSize.x) / 2f, 1, curMap.mapSize.y) * tileSize;

        Transform maskRight = Instantiate(navmeshMaskPrefab, (Vector3.right * (curMap.mapSize.x + maxMapSize.x) / 4f) + Vector3.up * 0.5f * tileSize, Quaternion.identity) as Transform;
        maskRight.parent = mapHolder;
        maskRight.localScale = new Vector3((maxMapSize.x - curMap.mapSize.x) / 2f, 1, curMap.mapSize.y) * tileSize;

        Transform maskTop = Instantiate(navmeshMaskPrefab, (Vector3.forward * (curMap.mapSize.x + maxMapSize.x) / 4f) + Vector3.up * 0.5f * tileSize, Quaternion.identity) as Transform;
        maskTop.parent = mapHolder;
        maskTop.localScale = new Vector3(maxMapSize.x, 1, (maxMapSize.y - curMap.mapSize.y) / 2f) * tileSize;

        Transform maskBottom = Instantiate(navmeshMaskPrefab, (Vector3.back * (curMap.mapSize.x + maxMapSize.x) / 4f) + Vector3.up * 0.5f * tileSize, Quaternion.identity) as Transform;
        maskBottom.parent = mapHolder;
        maskBottom.localScale = new Vector3(maxMapSize.x, 1, (maxMapSize.y - curMap.mapSize.y) / 2f) * tileSize;

        navmeshFloor.localScale = new Vector3(maxMapSize.x, maxMapSize.y) * tileSize;
    }

    bool MapIsFullAccessible(bool[,] obstacleMap, int curObstacleCount)
    {
        bool[,] mapFlags = new bool[obstacleMap.GetLength(0), obstacleMap.GetLength(1)];
        Queue<Coord> queue = new Queue<Coord>();
        queue.Enqueue(curMap.mapCenter);
        mapFlags[curMap.mapCenter.x, curMap.mapCenter.y] = true;

        int accessibleTileCount = 1;

        while (queue.Count > 0)
        {
            Coord tile = queue.Dequeue();

            for (int x = -1; x <= 1; x++)
            {
                for (int y = -1; y <= 1; y++)
                {
                    int neighbourX = tile.x + x;
                    int neighbourY = tile.y + y;
                    if (x == 0 || y == 0)
                    {
                        if (neighbourX >= 0 && neighbourX < obstacleMap.GetLength(0) && neighbourY >= 0 && neighbourY < obstacleMap.GetLength(1))
                        {
                            if (!mapFlags[neighbourX, neighbourY] && !obstacleMap[neighbourX, neighbourY])
                            {
                                mapFlags[neighbourX, neighbourY] = true;
                                queue.Enqueue(new Coord(neighbourX, neighbourY));
                                accessibleTileCount++;
                            }
                        }
                    }
                }
            }
        }

        int targetAccesseibleTileCount = (int)(curMap.mapSize.x * curMap.mapSize.y - curObstacleCount);

        return targetAccesseibleTileCount == accessibleTileCount;
    }

    Vector3 CoordToPos(int x, int y)
    {
        return new Vector3(-curMap.mapSize.x / 2f + 0.5f + x, 0, -curMap.mapSize.y / 2f + 0.5f + y) * tileSize;
    }

    public Coord GetRandomCoord()
    {
        Coord randomCoord = shuffleTileCoords.Dequeue();
        shuffleTileCoords.Enqueue(randomCoord);

        return randomCoord;
    }

    public Transform GetRandomOpenTile()
    {
        Coord randomCoord = shuffleOpenTileCoords.Dequeue();
        shuffleOpenTileCoords.Enqueue(randomCoord);

        return tileMap[randomCoord.x, randomCoord.y];
    }

    [System.Serializable]
    public struct Coord
    {
        public int x;
        public int y;

        public Coord(int _x, int _y)
        {
            x = _x;
            y = _y;
        }

        public static bool operator == (Coord c1, Coord c2)
        {
            return c1.x == c2.x && c1.y == c2.y;
        }

        public static bool operator !=(Coord c1, Coord c2)
        {
            return !(c1 == c2);
        }
    }    

    [System.Serializable]
    public class Map
    {
        public Coord mapSize;
        [Range(0,1)]
        public float obstaclePercent;

        public int seed;

        public Color forwardColor;
        public Color backColor;

        public Coord mapCenter
        {
            get
            {
                return new Coord(mapSize.x / 2, mapSize.y / 2);
            }
        }

    }
}
