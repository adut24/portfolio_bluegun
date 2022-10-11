using System.Collections.Generic;
using System.Linq;
using UnityEditor;
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
    public TilemapVisualiser tilemap;

    private void Awake()
    {
        RunProceduralGeneration();
    }

    public void RunProceduralGeneration()
    {
        HashSet<Vector2Int> floorPositions = RunRandomWalk();
        floorPositions = FillHoles(floorPositions);
        tilemap.PaintFloorTiles(floorPositions);
        WallGenerator.CreateWalls(floorPositions, tilemap);
        bool goodSpawn = false;
        Vector2Int playerSpawn = new(0, 0);
        while (!goodSpawn)
        {
            playerSpawn = floorPositions.ElementAt(Random.Range(0, floorPositions.Count));

            if (floorPositions.Contains(playerSpawn + new Vector2Int(0, 1)) && floorPositions.Contains(playerSpawn + new Vector2Int(0, -1)))
                goodSpawn = true;
        }

        GameObject.Find("Player").transform.position = (Vector2)playerSpawn;
        SetBombs(floorPositions, bombNumber);
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

    private void SetBombs(HashSet<Vector2Int> floorPositions, int bombNumber)
    {
        GameObject bomb = (GameObject)AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Bomb.prefab", typeof(GameObject));
        for (int i = 0; i < bombNumber; i++)
        {
            createdBomb = Instantiate(bomb, (Vector2)floorPositions.ElementAt(Random.Range(0, floorPositions.Count)), Quaternion.identity);
            createdBomb.GetComponent<BombExplosion>().bombDamage = bombPower;
        }
    }
}
