using UnityEngine;

public class BuildingData : ScriptableObject
{
    public string buildingName;
    public BuildingType type;
    public Sprite sprite;
    public Vector2Int size;
    public int health;
}
