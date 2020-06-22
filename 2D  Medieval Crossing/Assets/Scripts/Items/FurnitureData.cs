using UnityEngine;

[CreateAssetMenu(fileName = "New FurnitureData", menuName = "ScriptableObjects/FurnitureData", order = 1)]
public class FurnitureData : ItemData
{
    public Sprite[] sprites = new Sprite[4];
    public ColliderSettings[] colliderSettings = new ColliderSettings[4];

    [System.Serializable]
    public class ColliderSettings
    {
        [Range(0, 1)] public float scaleY = 1;
        [Range(0, 1)] public float offsetY = 0;
    }

}
