using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Vegetable", menuName = "ScriptableObjects/Vegetable", order = 2)]
public class VegetableData : ItemData
{
    [Min(0)] public int value = 0;
}
