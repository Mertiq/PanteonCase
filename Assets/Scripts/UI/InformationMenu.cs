using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InformationMenu : MonoBehaviour, ISetupable
{
    [SerializeField] private TMP_Text buildingNameText;
    [SerializeField] private TMP_Text buildingDescText;
    [SerializeField] private Image buildingIcon;
    [SerializeField] private GameObject buildingPart;
    [SerializeField] private GameObject productionPart;
    [SerializeField] private Transform soldierHolder;
    [SerializeField] private SoldierSlot soldierSlot;

    private ObjectPool<SoldierSlot> soldierSlotObjectPool;

    private void Start()
    {
        soldierSlotObjectPool = new ObjectPool<SoldierSlot>(soldierSlot, soldierHolder, 1);
    }

    public void Setup(params object[] args)
    {
        ResetView();
        buildingPart.SetActive(true);

        var data = args[0] switch
        {
            BuildingData buildingData => buildingData,
            Building building => building.data,
            _ => null
        };

        if (data == null) return;

        buildingNameText.text = data.buildingName;
        buildingDescText.text = $"health: {data.health}";
        buildingIcon.sprite = data.sprite;

        if (data is ProducerBuildingData producerBuildingData)
        {
            productionPart.SetActive(true);
            producerBuildingData.soldiers.ForEach(soldierData => soldierSlotObjectPool.GetObject().Setup(soldierData));
        }
    }

    public void ResetView(params object[] args)
    {
        buildingPart.SetActive(false);
        productionPart.SetActive(false);
        soldierSlotObjectPool.ReleaseAll();
    }
}