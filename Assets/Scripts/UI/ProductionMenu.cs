using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ProductionMenu : MonoBehaviour, ISetupable
{
    [SerializeField] private Transform productionHolder;
    [SerializeField] private ProductionSlot productionSlot;

    private List<BuildingData> data;
    private ObjectPool<ProductionSlot> productionSlotObjectPool;
    private List<ProductionSlot> instantiatedSlots = new();

    private void Start()
    {
        productionSlotObjectPool = new ObjectPool<ProductionSlot>(productionSlot, productionHolder, 1);
        Setup();
    }

    public void Setup(params object[] args)
    {
        data = GameDataHolder.Instance.buildingDatas;
        foreach (var buildingData in data)
        {
            var slot = productionSlotObjectPool.GetObject();
            slot.Setup(buildingData);
            instantiatedSlots.Add(slot);
        }
    }

    public void ResetView(params object[] args)
    {
        productionSlotObjectPool.ReleaseAll();
        instantiatedSlots.Clear();
    }

    public void SetSelectedSlot(params object[] args)
    {
        var buildingData = (BuildingData)args[0];

        var lastSelectedSlot = instantiatedSlots.FirstOrDefault(x => x.isSelected);
        var slot = instantiatedSlots.FirstOrDefault(x => x.data == buildingData);

        lastSelectedSlot?.SetSelected(false);

        slot?.SetSelected(slot != lastSelectedSlot);
        
    }
}