using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Seed", menuName = "ScriptableObjects/Seed", order = 1)]
public class SeedData : ItemData
{
    public Sprite[] growStages = new Sprite[4];
    [Min(0)] public int minGrowDuration;
    [Min(0)] public int maxGrowDuration;
    public VegetableData vegetable;
}
