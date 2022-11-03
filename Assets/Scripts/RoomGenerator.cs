using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// Create a procedurally generated room.
/// </summary>
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
    public int enemyLevel = 1;
    public string difficulty;
    public bool mergeDifficulty = false;
    private GameObject[] enemies;
    public TilemapVisualiser tilemap;
    public static HashSet<Vector2Int> floorPositions;
    public float radius = 7f;
    private static float checkRadius;

    private void Awake()
    {
        checkRadius = radius;
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
            enemies = easy.Union(normal).ToArray();
        }

        for (int i = 0; i < enemyNumber; i++)
        {
            Vector2 position = VerifySpawn(floorPositions);

            GameObject enemy = Instantiate(enemies[Random.Range(0, enemies.Length)], position, Quaternion.identity);
            enemy.GetComponent<Enemy>().level = enemyLevel;
            enemy.GetComponent<Enemy>().SetPower(); /* Change the stats according to the level */
        }
    }

    /// <summary>
    /// Generate a spawn position
    /// </summary>
    /// <param name="floorPositions">All the positions of the floor</param>
    /// <returns>The position of the gameObject</returns>
    public static Vector2 VerifySpawn(HashSet<Vector2Int> floorPositions)
    {
        bool goodSpawn = false;
        Vector2Int spawn = new(0, 0);

        while (!goodSpawn)
        {
            int count = 0;
            spawn = floorPositions.ElementAt(Random.Range(0, floorPositions.Count)); /* Take a random value inside floorPositions */

            foreach (Vector2Int direction in WallGenerator.allDirections)
            {
                if (!floorPositions.Contains(spawn + direction))
                    break;
                count++;
            }

            if (count == 8) /* if all surrounding positions are part of floorPositions */
            {
                Collider2D[] result = Physics2D.OverlapCircleAll(spawn, checkRadius);

                if (result.Length == 0) /* if there aren't any other gameObjects in the radius checked */
                    goodSpawn = true;
            }
        }
        return spawn;
    }
}
