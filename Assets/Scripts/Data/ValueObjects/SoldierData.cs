using System;
using Enums;
using UnityEngine;

namespace Data.ValueObjects
{
    [Serializable]
    public struct SoldierData
    {
        public string soldierName;
        public SoldierType type;
        public int health;
        public int damage;
        public Vector2Int size;
        public Sprite sprite;
        public Color pathPaintColor;
    }
}