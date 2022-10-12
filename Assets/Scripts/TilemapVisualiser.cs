using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TilemapVisualiser : MonoBehaviour
{
    public Tilemap floorTilemap, wallTilemap;
    public TileBase floorTile, wallTile;

    public void PaintFloorTiles(IEnumerable<Vector2Int> floorPositions)
    {
        /* IEnumerable makes possible to loop through a collection */
        PaintTiles(floorPositions, floorTilemap, floorTile);
    }
    private void PaintTiles(IEnumerable<Vector2Int> positions, Tilemap tilemap, TileBase tile)
    {
        foreach (Vector2Int position in positions)
        {
            PaintSingleTile(tilemap, tile, position);
        }
    }

    private void PaintSingleTile(Tilemap tilemap, TileBase tile, Vector2Int position)
    {
        Vector3Int tilePosition = tilemap.WorldToCell((Vector3Int)position);
        tilemap.SetTile(tilePosition, tile);
    }

    public void PaintSingleWall(Vector2Int position)
    {
        PaintSingleTile(wallTilemap, wallTile, position);
    }
}
