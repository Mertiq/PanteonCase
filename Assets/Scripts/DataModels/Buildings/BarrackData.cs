using UnityEngine;

[CreateAssetMenu(menuName = "Building/Barrack", fileName = "Barrack")]
public class BarrackData : ProducerBuildingData
{
    public override void Produce(SoldierData soldierData)
    {
        //SoldierCreator.Instance.CreateSoldier(soldierData, spawnPoint);
    }
}