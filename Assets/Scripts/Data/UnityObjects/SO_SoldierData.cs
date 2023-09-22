using Data.ValueObjects;
using UnityEngine;

namespace Data.UnityObjects
{
    [CreateAssetMenu(fileName = "SO_Soldier", menuName = "Soldiers/SO_Soldier", order = 0)]
    public class SO_SoldierData : ScriptableObject
    {
        public SoldierData data;
    }
}