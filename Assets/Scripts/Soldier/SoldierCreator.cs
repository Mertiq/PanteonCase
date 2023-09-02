using System.Collections.Generic;
using UnityEngine;

public class SoldierCreator : SingletonMonoBehaviour<SoldierCreator>
{
    [SerializeField] private Transform soldierHolder;
    [SerializeField] private List<CustomKeyValuePair<SoldierType, Soldier>> soldierPrefabs;

    private Dictionary<SoldierType, ObjectPool<Soldier>> objectPools = new();

    private void Start()
    {
        soldierPrefabs.ForEach(soldier =>
            objectPools.Add(soldier.key, new ObjectPool<Soldier>(soldier.value, soldierHolder, 1)));
    }

    public void CreateSoldier(params object[] args)
    {
        var data = (SoldierData)args[0];
        var building = (ProducerBuilding)GameManager.Instance.activeBuilding;

        objectPools[data.type].GetObject().Setup(data, building.spawnPoint);
    }

    public void ReleaseSoldier(Soldier soldier) => objectPools[soldier.data.type].ReleaseObject(soldier);
}