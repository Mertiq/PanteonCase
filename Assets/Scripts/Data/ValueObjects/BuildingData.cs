using System;
using Enums;
using UnityEngine;

namespace Data.ValueObjects
{
    [Serializable]
    public struct BuildingData
    {
        public string buildingName;
        public BuildingType type;
        public Vector2Int size;
        public byte health;
        public Sprite sprite;
    }
}