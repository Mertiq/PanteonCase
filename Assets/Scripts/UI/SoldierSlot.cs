using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SoldierSlot : MonoBehaviour, ISetupable
{
    [SerializeField] private Image image;
    [SerializeField] private TMP_Text nameText;
    [SerializeField] private TMP_Text descText;
    [SerializeField] private GameEvent onSoldierSelected;

    private SoldierData data;
    private ProducerBuildingData buildingData;

    public void Setup(params object[] args)
    {
        data = (SoldierData)args[0];
        buildingData = (ProducerBuildingData)args[1];
        image.sprite = data.sprite;
        nameText.text = data.soldierName;
        descText.text = $"health: {data.health}\n" +
                        $"damage: {data.damage}";
    }

    public void ResetView(params object[] args)
    {
        throw new System.NotImplementedException();
    }

    public void OnClick()
    {
        onSoldierSelected.Raise(data, buildingData.spawnPoint);
    }
}