using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InformationMenu : MonoBehaviour
{
    [SerializeField] private TMP_Text buildingNameText;
    [SerializeField] private TMP_Text buildingDescText;
    [SerializeField] private Image buildingIcon;
    [SerializeField] private GameObject productionPart;
    [SerializeField] private Transform soldierHolder;
    [SerializeField] private SoldierSlot soldierSlot;

    private ObjectPool<SoldierSlot> soldierSlotObjectPool;
    
    private BuildingData data;
    private ProducerBuildingData producerdata;

    private void Start()
    {
        soldierSlotObjectPool = new ObjectPool<SoldierSlot>(soldierSlot, soldierHolder, 1);
    }

    public void Setup(params object[] args)
    {
        ResetView();
        data = (BuildingData)args[0];
        buildingNameText.text = data.buildingName;
        buildingDescText.text = $"health: {data.health}";
        buildingIcon.sprite = data.sprite;

        // if (producerBuildingData != null)
        // {
        //     productionPart.SetActive(true);
        //     producerdata = producerBuildingData;
        //     producerdata.soldiers.ForEach(soldierData => soldierSlotObjectPool.GetObject().Setup(soldierData));
        // }
    }

    private void ResetView()
    {
        productionPart.SetActive(false);
        soldierSlotObjectPool.ReleaseAll();
    }
}