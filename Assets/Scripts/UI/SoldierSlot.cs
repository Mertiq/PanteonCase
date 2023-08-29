using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SoldierSlot : MonoBehaviour
{
    [SerializeField] private Image image;
    [SerializeField] private TMP_Text nameText;
    [SerializeField] private TMP_Text descText;
    
    private SoldierData data;

    public void Setup(SoldierData soldierData)
    {
        data = soldierData;
        image.sprite = data.sprite;
        nameText.text = data.soldierName;
        descText.text = $"health: {data.health}\n" +
                        $"damage: {data.damage}";
    }

    public void OnClick()
    {
        
    }
}