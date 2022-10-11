using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RoomGenerator : MonoBehaviour
{
    private Vector2Int startPosition = Vector2Int.zero;
    public int iterations = 10;
    public int walkLength = 10;
    public bool randomStart;
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
        GameObject.Find("Player").transform.position = (Vector2)floorPositions.ElementAt(Random.Range(0, floorPositions.Count));
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
}
