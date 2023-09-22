using Data.ValueObjects;
using UnityEngine;

namespace Data.UnityObjects
{
    [CreateAssetMenu(fileName = "SO_Building", menuName = "Buildings/SO_Building", order = 0)]
    public class SO_BuildingData : ScriptableObject
    {
        public BuildingData buildingData;
    }
}