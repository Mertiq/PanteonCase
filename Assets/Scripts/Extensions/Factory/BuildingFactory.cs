using System.Collections.Generic;
using System.Linq;
using Controllers.BuildingControllers;
using Data.UnityObjects;
using Enums;
using Signals;
using UnityEngine;

namespace Extensions.Factory
{
    public class BuildingFactory : SingletonMonoBehaviour<BuildingFactory>
    {
        [SerializeField] private Transform itemHolder;
        [SerializeField] private List<CustomKeyValuePair<BuildingType, BuildingController>> itemPrefabs;

        private readonly Dictionary<BuildingType, ObjectPool<BuildingController>> objectPools = new();

        private List<SO_BuildingData> data;

        private void Awake() => data = Resources.LoadAll<SO_BuildingData>("Data/Buildings").ToList();

        private void Start() => itemPrefabs.ForEach(item =>
            objectPools.Add(item.key, new ObjectPool<BuildingController>(item.value, itemHolder, 1)));

        private void CreateItem(BuildingType type)
        {
            var building = data.FirstOrDefault(x => x.buildingData.type.Equals(type));
            objectPools[type].GetObject().OnSetup(building);
        }

        public void ReleaseItem(BuildingController item)
        {
            item.OnReset();
            objectPools[item.Data.buildingData.type].ReleaseObject(item);
        }

        private void OnEnable() => SubscribeEvents();

        private void SubscribeEvents() => UISignals.Instance.onBuildingSlotClicked += CreateItem;

        private void OnDisable() => UnSubscribeEvents();

        private void UnSubscribeEvents() => UISignals.Instance.onBuildingSlotClicked -= CreateItem;
    }
}