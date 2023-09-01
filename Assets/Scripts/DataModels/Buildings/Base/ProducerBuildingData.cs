using System.Collections.Generic;
using UnityEngine;

public abstract class ProducerBuildingData : BuildingData
{
    public List<SoldierData> soldiers;
    public Vector2 spawnPoint;
    
    public abstract void Produce(SoldierData soldierData);
}