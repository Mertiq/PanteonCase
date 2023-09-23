using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using Enums;
using Extensions;
using Interfaces;
using Managers;
using Signals;
using UnityEngine;

namespace Controllers.SoldierControllers
{
    public class SoldierMovementController : MonoBehaviour
    {
        [SerializeField] private float speed;
        private SoldierController soldierController;

        private void Awake()
        {
            soldierController = GetComponent<SoldierController>();
        }

        private IEnumerator FollowPath(List<TileController> path, IDamageable damageable = null)
        {
            if (soldierController.state != SoldierState.Idle) yield break;

            var targetIndex = 0;

            if (path.Count > 0)
            {
                var currentWaypoint = path[targetIndex];
                while (true)
                {
                    if (soldierController.Position == currentWaypoint.position)
                    {
                        targetIndex++;
                        if (targetIndex >= path.Count)
                        {
                            soldierController.SetState(SoldierState.Idle);
                            break;
                        }

                        currentWaypoint = path[targetIndex];
                    }

                    yield return new WaitForSeconds(speed);

                    MoveOneTile(currentWaypoint.position);
                }
            }

            if (damageable is not null)
                SoldierSignals.Instance.onSoldierStartAttack.Invoke(damageable);
        }

        private void MoveOneTile(Vector3 newPos)
        {
            GameBoardManager.Instance.SetTiles(new Rect(soldierController.Position, Vector2.one / 2), true);

            soldierController.SetState(SoldierState.Moving);

            transform.DOMove(newPos + SoldierController.Offset, speed * Time.deltaTime);

            GameBoardManager.Instance.SetTiles(new Rect(newPos, Vector2.one / 2), false);
        }

        private void MoveToDamageable(IDamageable damageable, Vector3 position, Vector2Int size)
        {
            if (soldierController != GameManager.Instance.SelectedSoldierController) return;

            if (damageable is SoldierController controller)
                if (soldierController == controller)
                    return;

            var rect = Utilities.Utilities.CreateRect(position, size);

            if (rect.GetNeighbourTiles().Any(neighbor => neighbor.position == soldierController.Position))
            {
                SoldierSignals.Instance.onSoldierStartAttack.Invoke(damageable);
            }
            else
            {
                var nearestPath = Utilities.Utilities.FindNearestPath(rect);

                if (nearestPath is not null)
                    StartCoroutine(FollowPath(nearestPath, damageable));
            }
        }

        private void MoveToTile(TileController tileController)
        {
            if (soldierController != GameManager.Instance.SelectedSoldierController) return;

            var path = Utilities.Utilities.FindPath(soldierController.Position, tileController.position);

            if (path is not null)
                StartCoroutine(FollowPath(path));
        }

        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            GameBoardSignals.Instance.onTileSelected += MoveToTile;
            SoldierSignals.Instance.onSoldierClickWithRight += MoveToDamageable;
            BuildingSignals.Instance.onBuildingClickedWithRight += MoveToDamageable;
        }

        private void OnDisable()
        {
            UnSubscribeEvents();
        }

        private void UnSubscribeEvents()
        {
            GameBoardSignals.Instance.onTileSelected += MoveToTile;
            SoldierSignals.Instance.onSoldierClickWithRight -= MoveToDamageable;
            BuildingSignals.Instance.onBuildingClickedWithRight -= MoveToDamageable;
        }
    }
}