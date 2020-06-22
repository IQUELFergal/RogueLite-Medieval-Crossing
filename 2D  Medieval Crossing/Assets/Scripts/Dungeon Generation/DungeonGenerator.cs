using System.Collections.Generic;
using UnityEngine;

public enum Direction { up, right, down, left };

public class DungeonGenerator : MonoBehaviour
{
    Room[] roomList;
    public DungeonData dungeonData;
    public bool useCustomLayoutRange = false;
    [Min(0)] public int minLayout = 0;
    [Min(1)] public int maxLayout = 1;

    //Positions visited by th diggers
    List<Vector2Int> positionVisited = new List<Vector2Int>();

    private static readonly Dictionary<Direction, Vector2Int> directionMouvementMap = new Dictionary<Direction, Vector2Int>
    {
        { Direction.up, Vector2Int.up},
        { Direction.right, Vector2Int.right},
        { Direction.down, Vector2Int.down},
        { Direction.left, Vector2Int.left}
    };

    private void Start()
    {
        if (dungeonData != null)
        {
            TilemapGenerator gen = GetComponent<TilemapGenerator>();
            Vector2Int[] roomPositions = GenerateRoomPositions();
            roomList = CreateRooms(roomPositions);
            gen.DrawDungeon(roomList);
        }
        else Debug.LogError("No DungeonData found.");
    }


    public Vector2Int[] GenerateRoomPositions()
    {
        Digger[] diggers = new Digger[dungeonData.numberOfDiggers];
        Vector2Int startPos = Vector2Int.zero;
        positionVisited.Add(startPos);
        for (int i = 0; i < diggers.Length; i++)
        {
            diggers[i] = new Digger(startPos);
        }

        int iterations = UnityEngine.Random.Range(dungeonData.minIterations, dungeonData.maxIterations);
        for (int i = 0; i < iterations; i++)
        {
            for (int j = 0; j < diggers.Length; j++)
            {
                Vector2Int newPos = diggers[j].Move(directionMouvementMap);
                if (!positionVisited.Contains(newPos)) positionVisited.Add(newPos);
            }
        }
        return positionVisited.ToArray();
    }


    public Room[] CreateRooms(Vector2Int[] positions)
    {
        Room[] rooms = new Room[positions.Length];
        for (int i = 0; i < rooms.Length; i++)
        {
            int roomType = 0;
            //For each direction
            for (int j = 0; j < directionMouvementMap.Count; j++)
            {
                //Check for neighbor rooms inside the array
                for (int k = 0; k < positions.Length; k++)
                {
                    //Bitmask the roomType to get the good one
                    //See : https://gamedevelopment.tutsplus.com/tutorials/how-to-use-tile-bitmasking-to-auto-tile-your-level-layouts--cms-25673
                    roomType += (positions[i] + directionMouvementMap[(Direction)j] == positions[k] ? (int)Mathf.Pow(2, j) : 0);
                }
            }
            //Randomize the layout
            int roomLayout = 0;
            if (useCustomLayoutRange) roomLayout = Random.Range(minLayout, maxLayout);
            else roomLayout = Random.Range(dungeonData.minLayout, dungeonData.maxLayout);
            rooms[i] = new Room(positions[i], roomType, roomLayout);
        }
        return rooms;
    }

    private void OnValidate()
    {
        if (minLayout >= maxLayout) maxLayout = minLayout + 1;
    }
}
