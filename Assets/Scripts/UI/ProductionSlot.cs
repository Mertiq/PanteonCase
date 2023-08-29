using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ProductionSlot : MonoBehaviour
{
    [SerializeField] private Image image;
    [SerializeField] private TMP_Text nameText;
    [SerializeField] private GameEvent onBuildingSelected;
    
    private BuildingData data;

    public void Setup(BuildingData buildingData)
    {
        data = buildingData;
        image.sprite = data.sprite;
        nameText.text = data.buildingName;
    }

    public void OnClick()
    {
        onBuildingSelected.Raise(data);
    }
}