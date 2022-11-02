using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Algorithm used for the procedural generation
/// </summary>
public class RandomWalk
{
    public static HashSet<Vector2Int> RunRandomWalk(Vector2Int startPosition, int walkLength)
    {
        HashSet<Vector2Int> path = new();

        path.Add(startPosition);
        Vector2Int previousPosition = startPosition;

        for (int i = 0; i < walkLength; i++)
        {
            var newPosition = previousPosition + Distance2D.GetCardinalDirection();
            path.Add(newPosition);
            previousPosition = newPosition;
        }

        return path;
    }
}

/// <summary>
/// Direction for the procedural generation
/// </summary>
public static class Distance2D
{
    public static List<Vector2Int> direction = new()
    {
        new Vector2Int(1, 0), /* RIGHT direction */
        new Vector2Int(-1, 0), /* LEFT direction */
        new Vector2Int(0, 1), /* UP direction */
        new Vector2Int(0, -1) /* DOWN direction */
    };

    public static Vector2Int GetCardinalDirection()
    {
        return direction[Random.Range(0, direction.Count)];
    }
}