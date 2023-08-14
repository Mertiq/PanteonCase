using System.Collections.Generic;
using UnityEngine;

public class ProductionMenu : MonoBehaviour
{
    [SerializeField] private Transform productionHolder;
    [SerializeField] private ProductionSlot productionSlot;

    private List<BuildingData> data;

    private void Start()
    {
        data = GameData.Instance.buildingDatas;
        Setup();
    }

    private void Setup()
    {
        data.ForEach(buildingData => Instantiate(productionSlot, productionHolder).Setup(buildingData));
    }
}