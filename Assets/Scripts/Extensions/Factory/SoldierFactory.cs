using System.Collections.Generic;
using System.Linq;
using Controllers.BuildingControllers;
using Controllers.SoldierControllers;
using Data.UnityObjects;
using Enums;
using Managers;
using Signals;
using UnityEngine;

namespace Extensions.Factory
{
    public class SoldierFactory : SingletonMonoBehaviour<SoldierFactory>
    {
        [SerializeField] private Transform itemHolder;
        [SerializeField] private List<CustomKeyValuePair<SoldierType, SoldierController>> itemPrefabs;

        private readonly Dictionary<SoldierType, ObjectPool<SoldierController>> objectPools = new();

        private List<SO_SoldierData> data;

        private void Awake() => data = Resources.LoadAll<SO_SoldierData>("Data/Soldiers").ToList();

        private void Start() =>
            itemPrefabs.ForEach(item =>
                objectPools.Add(item.key, new ObjectPool<SoldierController>(item.value, itemHolder, 1)));

        private void CreateItem(SoldierType type)
        {
            var soldierData = data.FirstOrDefault(x => x.data.type.Equals(type))!.data;

            var building = (ProducerBuildingController)GameManager.Instance.SelectedBuildingController;

            if (!building.IsSpawnPointEmpty()) return;

            var soldier = objectPools[soldierData.type].GetObject();
            soldier.OnSetup(soldierData, building.soldierSpawnPoint);
            building.soldiers.Add(soldier);
        }

        public void ReleaseItem(SoldierController item)
        {
            item.OnReset();
            objectPools[item.Data.type].ReleaseObject(item);
            GameManager.Instance.SetSelectedBuilding(null);
        }


        private void OnEnable() => UISignals.Instance.onSoldierSlotClicked += CreateItem;
        private void OnDisable() => UISignals.Instance.onSoldierSlotClicked -= CreateItem;
    }
}