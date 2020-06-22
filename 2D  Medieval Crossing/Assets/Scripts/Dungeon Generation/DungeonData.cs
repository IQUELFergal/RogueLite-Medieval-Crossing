using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New DungeonData", menuName = "ScriptableObjects/DungeonData", order = 1)]
public class DungeonData : ScriptableObject
{
    public int numberOfDiggers = 1;
    public int minIterations = 1;
    public int maxIterations = 1;
    [Min(0)] public int minLayout = 0;
    [Min(1)] public int maxLayout = 1;
}
