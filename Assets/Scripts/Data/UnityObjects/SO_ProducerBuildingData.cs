using Data.ValueObjects;
using UnityEngine;

namespace Data.UnityObjects
{
    [CreateAssetMenu(fileName = "SO_ProducerBuilding", menuName = "Buildings/SO_ProducerBuilding", order = 0)]
    public class SO_ProducerBuildingData : SO_BuildingData
    {
        public ProducerBuildingData producerBuildingData;
    }
}