using Controllers.UIControllers;
using Data.UnityObjects;
using Data.ValueObjects;
using Extensions;
using Interfaces;
using Managers;
using Signals;
using UnityEngine;

namespace Controllers.BuildingControllers
{
    [RequireComponent(typeof(BuildingMovementController))]
    public class BuildingController : MonoBehaviour, IRightClickable, ILeftClickable, IDamageable
    {
        #region Variables

        #region SerializedVariables

        [SerializeField] private SpriteRenderer iconImage;
        [SerializeField] private SpriteRenderer errorImage;
        [SerializeField] private HealthBarController healthBar;

        #endregion

        #region PrivateVariables

        private SO_BuildingData data;
        private float currentHealth;

        #endregion

        #region PublicVariables

        [Header("Do not fill from Inspector!")]
        public BuildingMovementController buildingMovementController;

        public SO_BuildingData Data => data;
        public bool isPlaced;
        private (bool, ProducerBuildingController) isProducer;

        #endregion

        #endregion

        private void Awake()
        {
            buildingMovementController = GetComponent<BuildingMovementController>();
            isProducer = (TryGetComponent<ProducerBuildingController>(out var producerBuildingController), producerBuildingController);
        }

        public void OnSetup(SO_BuildingData buildingData)
        {
            data = buildingData;
            iconImage.sprite = data.buildingData.sprite;
            transform.localScale = new Vector3(data.buildingData.size.x, data.buildingData.size.y) / Config.BoardScaleFactor;
            currentHealth = data.buildingData.health;
            BuildingSignals.Instance.onBuildingClickedWithLeft.Invoke(this);
        }

        public void OnReset()
        {
            isPlaced = false;
            healthBar.SetHealthBar(data.buildingData.health, data.buildingData.health);

            GameBoardManager.Instance.SetTiles(Utilities.Utilities.CreateRect(transform.position, data.buildingData.size), true);
        }

        public void MovementError(Rect rect) =>
            errorImage.gameObject.SetActive(!GameBoardManager.Instance.IsPlacementValid(rect));

        public void OnRightClick()
        {
            BuildingSignals.Instance.onBuildingClickedWithRight?.Invoke(this, transform.position, data.buildingData.size);
        }

        public void OnLeftClick()
        {
            BuildingSignals.Instance.onBuildingClickedWithLeft?.Invoke(this);
        }

        public void TakeDamage(float damage)
        {
            currentHealth -= damage;

            healthBar.SetHealthBar(currentHealth, data.buildingData.health);

            if (!IsAlive())
                BuildingFactory.Instance.ReleaseItem(this);
        }

        public bool IsAlive() => currentHealth > 0;

        public (bool, ProducerBuildingController) IsProducer() => isProducer;
    }
}