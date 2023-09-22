using Controllers.UIControllers;
using Data.ValueObjects;
using Enums;
using Extensions;
using Interfaces;
using Managers;
using Signals;
using UnityEngine;

namespace Controllers.SoldierControllers
{
    [RequireComponent(typeof(SoldierMovementController), typeof(SoldierFightController))]
    public class SoldierController : MonoBehaviour, ILeftClickable, IRightClickable, IDamageable
    {
        #region Variables

        #region SerializedVariables

        [SerializeField] private SpriteRenderer iconImage;
        [SerializeField] private HealthBarController healthBar;

        #endregion

        #region PrivateVariables

        private SoldierData data;
        private float currentHealth;

        #endregion

        #region PublicVariables

        [Header("Do not fill from Inspector!")]

        public SoldierState state;
        public SoldierData Data => data;
        public Vector2 Position => transform.position - Offset;
        public static Vector3 Offset => new(1 / Config.BoardScaleFactor / 2, 1 / Config.BoardScaleFactor / 2);

        #endregion

        #endregion

        public void OnSetup(SoldierData soldierData, Vector2 spawnPosition)
        {
            data = soldierData;
            currentHealth = data.health;
            iconImage.sprite = data.sprite;
            transform.position = spawnPosition;

            var rect = new Rect(Position, Vector2.one / 2);
            GameBoardManager.Instance.SetTiles(rect, false);
        }

        public void OnReset()
        {
            GameBoardManager.Instance.SetTiles(new Rect(Position,data.size), true);
            healthBar.SetHealthBar(data.health, data.health);
        }

        public void OnLeftClick() => SoldierSignals.Instance.onSoldierClickWithLeft.Invoke(this);

        public void OnRightClick() => SoldierSignals.Instance.onSoldierClickWithRight.Invoke(this, transform.position, data.size);

        public void TakeDamage(float damage)
        {
            currentHealth -= damage;
            healthBar.SetHealthBar(currentHealth, data.health);
            
             if (!IsAlive())
                 SoldierFactory.Instance.ReleaseItem(this);
        }

        public bool IsAlive() => currentHealth > 0;

        public void SetState(SoldierState soldierState) => state = soldierState;
    }
}