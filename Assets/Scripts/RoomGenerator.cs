using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RoomGenerator : MonoBehaviour
{
    private Vector2Int startPosition = Vector2Int.zero;
    private GameObject createdBomb;
    public int iterations = 10;
    public int walkLength = 10;
    public bool randomStart;
    public int bombNumber;
    public int bombPower;
    public int enemyNumber;
    public string difficulty;
    public bool mergeDifficulty = false;
    private GameObject[] enemies;
    public TilemapVisualiser tilemap;
    public HashSet<Vector2Int> floorPositions;

    private void Awake()
    {
        RunProceduralGeneration();
    }

    public void RunProceduralGeneration()
    {
        floorPositions = RunRandomWalk();
        floorPositions = FillHoles(floorPositions);
        tilemap.PaintFloorTiles(floorPositions);
        WallGenerator.CreateWalls(floorPositions, tilemap);
        SpawnBombs(bombNumber);
        SpawnEnemies(enemyNumber);
        SpawnPlayer();
    }

    protected HashSet<Vector2Int> RunRandomWalk()
    {
        Vector2Int currentPosition = startPosition;
        HashSet<Vector2Int> floorPositions = new();

        for (int i = 0; i < iterations; i++)
        {
            HashSet<Vector2Int> path = RandomWalk.RunRandomWalk(currentPosition, walkLength);
            floorPositions.UnionWith(path); /* Add element from path not already in floorPositions */
            if (randomStart)
                currentPosition = floorPositions.ElementAt(Random.Range(0, floorPositions.Count));
        }

        return floorPositions;
    }

    protected HashSet<Vector2Int> FillHoles(HashSet<Vector2Int> floorPositions)
    {
        foreach (Vector2Int position in floorPositions.ToList())
        {
            foreach (Vector2Int direction in Distance2D.direction)
            {
                Vector2Int neighbourPosition = position + direction;

                if (!floorPositions.Contains(neighbourPosition))
                    floorPositions.Add(neighbourPosition);
            }
        }
        return floorPositions;
    }

    private void SpawnPlayer()
    {
        Vector2 playerSpawn = VerifySpawn(floorPositions);

        GameObject.Find("Player").transform.position = playerSpawn;
    }

    private void SpawnBombs(int bombNumber)
    {
        GameObject bomb = Resources.Load<GameObject>("Bomb");

        for (int i = 0; i < bombNumber; i++)
        {
            Vector2 bombSpawn = VerifySpawn(floorPositions);

            createdBomb = Instantiate(bomb, bombSpawn, Quaternion.identity);
            createdBomb.GetComponent<BombExplosion>().bombDamage = bombPower;
        }
    }

    private void SpawnEnemies(int enemyNumber)
    {
        if (!mergeDifficulty)
            enemies = Resources.LoadAll<GameObject>("Enemies/" + difficulty);
        else
        {
            GameObject[] easy = Resources.LoadAll<GameObject>("Enemies/Easy");
            GameObject[] normal = Resources.LoadAll<GameObject>("Enemies/Normal");
            GameObject[] hard = Resources.LoadAll<GameObject>("Enemies/Hard");
            GameObject[] middle = easy.Union(normal).ToArray();
            enemies = middle.Union(hard).ToArray();
        }

        for (int i = 0; i < enemyNumber; i++)
        {
            Vector2 position = VerifySpawn(floorPositions);
            Instantiate(enemies[Random.Range(0, enemies.Length)], position, Quaternion.identity);
        }
    }

    private Vector2 VerifySpawn(HashSet<Vector2Int> floorPositions)
    {
        bool goodSpawn = false;
        Vector2Int spawn = new(0, 0);

        while (!goodSpawn)
        {
            int count = 0;
            spawn = floorPositions.ElementAt(Random.Range(0, floorPositions.Count));

            foreach (Vector2Int direction in WallGenerator.allDirections)
            {
                if (!floorPositions.Contains(spawn + direction))
                    break;
                count++;
            }

            if (count == 8)
            {
                Collider2D[] result = Physics2D.OverlapCircleAll(spawn, 7f);
                if (result.Length == 0)
                    goodSpawn = true;
            }
        }
        return spawn;
    }
}
