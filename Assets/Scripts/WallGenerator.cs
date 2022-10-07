using System.Collections.Generic;
using UnityEngine;

public class WallGenerator
{
    public static List<Vector2Int> allDirections = new()
    {
        new Vector2Int(1, 0), /* RIGHT direction */
        new Vector2Int(-1, 0), /* LEFT direction */
        new Vector2Int(0, 1), /* UP direction */
        new Vector2Int(0, -1), /* DOWN direction */
        new Vector2Int(-1, 1), /* UP LEFT direction */
        new Vector2Int(1, 1), /* UP RIGHT direction */
        new Vector2Int(-1, -1), /* DOWN LEFT direction */
        new Vector2Int(1, -1), /* UP RIGHT direction */
    };

    public static void CreateWalls(HashSet<Vector2Int> floorPositions, TilemapVisualiser tilemap)
    {
        IEnumerable<Vector2Int> wallsPositions = FindWallsPositions(floorPositions, allDirections);

        foreach (Vector2Int position in wallsPositions)
        {
            tilemap.PaintSingleWall(position);
        }

    }

    private static HashSet<Vector2Int> FindWallsPositions(HashSet<Vector2Int> floorPositions, List<Vector2Int> directions)
    {
        HashSet<Vector2Int> wallsPositions = new();

        foreach (Vector2Int position in floorPositions)
        {
            foreach (Vector2Int direction in directions)
            {
                Vector2Int neighbourPosition = position + direction;

                if (!floorPositions.Contains(neighbourPosition))
                    wallsPositions.Add(neighbourPosition);
            }
        }

        return wallsPositions;
    }
}
