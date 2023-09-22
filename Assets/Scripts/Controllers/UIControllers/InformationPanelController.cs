using Controllers.BuildingControllers;
using Data.UnityObjects;
using Data.ValueObjects;
using Extensions;
using Signals;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Controllers.UIControllers
{
    public class InformationPanelController : MonoBehaviour
    {
        [SerializeField] private TMP_Text buildingNameText;
        [SerializeField] private TMP_Text buildingDescText;
        [SerializeField] private Image buildingIcon;
        [SerializeField] private GameObject buildingPart;
        [SerializeField] private GameObject productionPart;
        [SerializeField] private Transform soldierSlotHolder;
        [SerializeField] private SoldierSlotController soldierSlot;

        private ObjectPool<SoldierSlotController> soldierSlotObjectPool;

        private void Start()
        {
            soldierSlotObjectPool = new ObjectPool<SoldierSlotController>(soldierSlot, soldierSlotHolder, 1);
            OnReset();
        }

        private void Setup(BuildingController buildingController)
        {
            OnReset();
            buildingPart.SetActive(true);

            buildingIcon.sprite = buildingController.Data.buildingData.sprite;
            buildingNameText.text = buildingController.Data.buildingData.buildingName;
            buildingDescText.text = $"Health: {buildingController.Data.buildingData.health}\n" +
                                    $"Size: {buildingController.Data.buildingData.size}";

            var (isProducer, producerBuildingController) = buildingController.IsProducer();

            if (isProducer)
            {
                productionPart.SetActive(true);

                if (producerBuildingController.Data is So_ProducerBuildingData x)
                {
                    x.producerBuildingData.soldiers
                        .ForEach(soldier => soldierSlotObjectPool.GetObject().Setup(soldier.data));
                }
                
            }
        }

        private void OnReset()
        {
            buildingPart.SetActive(false);
            productionPart.SetActive(false);
            soldierSlotObjectPool.ReleaseAll();
        }

        private void OnEnable() => BuildingSignals.Instance.onBuildingClickedWithLeft += Setup;
        private void OnDisable() => BuildingSignals.Instance.onBuildingClickedWithLeft -= Setup;
    }
}