using System.Collections.Generic;
using System.Linq;
using Data.UnityObjects;
using Enums;
using Extensions;
using Signals;
using UnityEngine;

namespace Controllers.UIControllers
{
    public class ProductionPanelController : MonoBehaviour
    {
        [SerializeField] private Transform productHolder;
        [SerializeField] private BuildingSlotController productSlotController;

        private ObjectPool<BuildingSlotController> buildingSlotObjectPool;
        private readonly List<BuildingSlotController> instantiatedSlots = new();
        private BuildingSlotController selectedSlotController;

        private void Start()
        {
            buildingSlotObjectPool = new ObjectPool<BuildingSlotController>(productSlotController, productHolder, 1);
            Setup();
        }

        private void Setup()
        {
            var buildings = Resources.LoadAll<SO_BuildingData>("Data/Buildings").ToList();

            foreach (var building in buildings)
            {
                var slot = buildingSlotObjectPool.GetObject();
                slot.Setup(building.buildingData);
                instantiatedSlots.Add(slot);
            }
        }

        public void OnReset()
        {
            buildingSlotObjectPool.ReleaseAll();
            instantiatedSlots.Clear();
        }

        private void SetSelectedSlot(BuildingType type)
        {
            if (selectedSlotController is not null && selectedSlotController.data.type.Equals(type)) return;

            selectedSlotController?.SetSelected(false);
            selectedSlotController = GetSlotByType(type);
            selectedSlotController.SetSelected(true);
        }

        private BuildingSlotController GetSlotByType(BuildingType type) => instantiatedSlots.FirstOrDefault(x => x.data.type.Equals(type));
        private void OnEnable() => SubscribeEvents();

        private void SubscribeEvents()
        {
            UISignals.Instance.onBuildingSlotClicked += SetSelectedSlot;
        }

        private void OnDisable() => UnSubscribeEvents();

        private void UnSubscribeEvents()
        {
            UISignals.Instance.onBuildingSlotClicked -= SetSelectedSlot;
        }
    }
}