using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TilemapGenerator : MonoBehaviour
{
    public Tilemap wallTilemap = null;
    public Tilemap floorTilemap = null;
    public TileBase wallTile = null;
    public TileBase floorTile = null;
    TileBase noTile = null;
    public Texture2D debugTexture = null;

    public Vector2Int roomSize = new Vector2Int(16, 16);

    [ContextMenu("Generate tilemap")]
    void GenerateTilemap()
    {
        Debug.Log("Clearing tilemap.");
        ClearTilemap();

        Debug.Log("Drawing tilemap...");
        int[] roomTilemap = MapTextureExtractor.GetTextureData(debugTexture, roomSize.x, roomSize.y, 16);
        DrawRoom(roomTilemap);
    }

    void DrawRoom(int[] tilePos, int xOffset = 0, int yOffset = 0)
    {
        Debug.Log("Drawing room...");
        Debug.Log("Room length : "+tilePos.Length);
        for (int i = 0; i < tilePos.Length; i++)
        {
            if (tilePos[i] == 1)
            {
                wallTilemap.SetTile(new Vector3Int(i % roomSize.x + xOffset, i / roomSize.y + yOffset, 0), wallTile);
            }
            else floorTilemap.SetTile(new Vector3Int(i % roomSize.x + xOffset, i / roomSize.y + yOffset, 0), floorTile);
        }
    }

    public void DrawDungeon(Room[] rooms)
    {
        foreach (var room in rooms)
        {
            int[] roomTilemap = MapTextureExtractor.GetTextureData(debugTexture, roomSize.x, roomSize.y, roomSize.x * room.roomType, roomSize.y * room.roomLayout);
            DrawRoom(roomTilemap, roomSize.x * room.position.x, roomSize.y * room.position.y);
        }
        
    }

    [ContextMenu("Get tilemap info")]
    void GetTilemapInformations()
    {
        Debug.Log("cellBounds :" + wallTilemap.cellBounds.ToString());
        Debug.Log("color :" + wallTilemap.color.ToString());
        Debug.Log("origin :" + wallTilemap.origin.ToString());
        Debug.Log("size :" + wallTilemap.size.ToString());
        Debug.Log("tileAnchor :" + wallTilemap.tileAnchor.ToString());
        Debug.Log("localBounds :" + wallTilemap.localBounds.ToString());
    }

    [ContextMenu("Clear Tilemap")]
    void ClearTilemap()
    {
        wallTilemap.ClearAllTiles();
    }
}
