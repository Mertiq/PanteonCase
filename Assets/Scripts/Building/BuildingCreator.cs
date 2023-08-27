﻿using System.Collections.Generic;
using UnityEngine;

public class BuildingCreator : SingletonMonoBehaviour<BuildingCreator>
{
    [SerializeField] private Transform buildingHolder;
    [SerializeField] private List<CustomKeyValuePair<BuildingType, Building>> buildingPrefabs;

    private Dictionary<BuildingType, ObjectPool<Building>> objectPools = new();

    private void Start()
    {
        buildingPrefabs.ForEach(building =>
            objectPools.Add(building.key, new ObjectPool<Building>(building.value, buildingHolder, 1)));
    }

    public void CreateBuilding(BuildingData data)
    {
        objectPools[data.type].GetObject().Setup(data);
    }
}