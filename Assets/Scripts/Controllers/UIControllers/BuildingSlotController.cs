using Data.ValueObjects;
using DG.Tweening;
using Managers;
using Signals;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Controllers.UIControllers
{
    public class BuildingSlotController : MonoBehaviour
    {
        [SerializeField] private Image icon;
        [SerializeField] private Image selectedImage;
        [SerializeField] private TMP_Text nameText;

        public BuildingData data;

        public void Setup(BuildingData buildingData)
        {
            data = buildingData;
            icon.sprite = data.sprite;
            nameText.text = data.buildingName;
        }

        public void OnClick()
        {
            if (GameManager.Instance.SelectedBuildingController != null) 
                if (!GameManager.Instance.SelectedBuildingController.isPlaced) return;
            
            UISignals.Instance.onBuildingSlotClicked.Invoke(data.type);
        }

        public void SetSelected(bool isSelected) => selectedImage.DOFade(isSelected ? 1f : 0f, 1f);
    }
}