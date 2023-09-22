using Data.ValueObjects;
using Signals;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Controllers.UIControllers
{
    public class SoldierSlotController : MonoBehaviour
    {
        [SerializeField] private Image icon;
        [SerializeField] private TMP_Text nameText;
        [SerializeField] private TMP_Text descText;

        private SoldierData data;

        public void Setup(SoldierData soldierData)
        {
            data = soldierData;
            icon.sprite = data.sprite;
            nameText.text = data.soldierName;
            descText.text = $"health: {data.health}\n" +
                            $"damage: {data.damage}";
        }
        
        public void OnClick()
        {
            Debug.Log($"{data.soldierName} askerine tıklandı");
            UISignals.Instance.onSoldierSlotClicked.Invoke(data.type);
        }
    }
}