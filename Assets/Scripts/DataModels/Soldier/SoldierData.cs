using UnityEngine;

[CreateAssetMenu(menuName = "Soldier", fileName = "Soldier")]
public class SoldierData : GameData
{
    public string soldierName;
    public SoldierType type;
    public Sprite sprite;
    public Vector2Int size;
    public int health;
    public int damage;
}