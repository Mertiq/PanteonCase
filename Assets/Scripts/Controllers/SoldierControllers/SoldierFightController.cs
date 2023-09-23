using System.Collections;
using Enums;
using Interfaces;
using Managers;
using Signals;
using UnityEngine;

namespace Controllers.SoldierControllers
{
    public class SoldierFightController : MonoBehaviour
    {
        private SoldierController soldierController;

        private void Awake()
        {
            soldierController = GetComponent<SoldierController>();
        }

        private void StartAttack(IDamageable damageable)
        {
            if (GameManager.Instance.SelectedSoldierController.Equals(soldierController))
                StartCoroutine(AttackCoroutine(damageable));
        }

        private IEnumerator AttackCoroutine(IDamageable damageable)
        {
            soldierController.SetState(SoldierState.Attacking);

            while (true)
            {
                yield return new WaitForSeconds(1);

                if (!damageable.IsAlive())
                {
                    soldierController.SetState(SoldierState.Idle);
                    yield break;
                }

                damageable.TakeDamage(soldierController.Data.damage);
            }
        }

        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void OnDisable()
        {
            UnSubscribeEvents();
        }

        private void SubscribeEvents()
        {
            SoldierSignals.Instance.onSoldierStartAttack += StartAttack;
        }

        private void UnSubscribeEvents()
        {
            SoldierSignals.Instance.onSoldierStartAttack -= StartAttack;
        }
    }
}