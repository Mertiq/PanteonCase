using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ProductionSlot : MonoBehaviour
{
    [SerializeField] private Image image;
    [SerializeField] private GameObject selectedImage;
    [SerializeField] private TMP_Text nameText;
    [SerializeField] private GameEvent onBuildingSelected;

    [HideInInspector] public BuildingData data;
    [HideInInspector] public bool isSelected;

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

    public void SetSelected(bool flag)
    {
        isSelected = flag;
        selectedImage.SetActive(flag);
    }
}