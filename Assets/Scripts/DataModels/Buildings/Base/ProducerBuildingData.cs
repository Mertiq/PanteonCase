using System.Collections.Generic;

public abstract class ProducerBuildingData : BuildingData
{
    public List<SoldierData> soldiers;
    
    public abstract void Produce(SoldierData soldierData);
}