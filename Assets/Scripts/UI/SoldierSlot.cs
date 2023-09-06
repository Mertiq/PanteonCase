using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SoldierSlot : MonoBehaviour
{
    [SerializeField] private Image image;
    [SerializeField] private TMP_Text nameText;
    [SerializeField] private TMP_Text descText;
    [SerializeField] private GameEvent onSoldierSlotSelected;

    private SoldierData data;

    public void Setup(params object[] args)
    {
        data = (SoldierData)args[0];
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
        onSoldierSlotSelected.Raise(data);
    }
}