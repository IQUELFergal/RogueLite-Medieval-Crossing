using UnityEngine;

public class Room
{
    public Vector2Int position;
    [Range(0,15)] public int roomType;
    [Min(0)] public int roomLayout;

    public Room(Vector2Int pos, int rType = 0, int rLayout = 0)
    {
        position = pos;
        roomType = rType;
        roomLayout = rLayout;
    }
}
