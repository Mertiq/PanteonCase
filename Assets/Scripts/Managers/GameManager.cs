using UnityEngine;

public class GameManager : SingletonMonoBehaviour<GameManager>
{
    [HideInInspector] public Building activeBuilding;

    public void SetActiveBuilding(Building building)
    {
        activeBuilding = building;
    }
}