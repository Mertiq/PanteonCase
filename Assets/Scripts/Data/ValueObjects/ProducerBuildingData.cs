using System;
using System.Collections.Generic;
using Data.UnityObjects;

namespace Data.ValueObjects
{
    [Serializable]
    public struct ProducerBuildingData
    {
        public List<SO_SoldierData> soldiers;
    }
}