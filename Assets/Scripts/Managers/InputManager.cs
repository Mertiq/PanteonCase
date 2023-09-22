using Extensions;
using Interfaces;
using Signals;
using UnityEngine;

namespace Managers
{
    public class InputManager : SingletonMonoBehaviour<InputManager>
    {
        private new Camera camera;

        private void Awake() => camera = Camera.main;

        private void Update()
        {
            var hit = Physics2D.Raycast(camera.ScreenToWorldPoint(Input.mousePosition),
                transform.position, 100);

            if (hit.collider is null) return;

            if (Input.GetMouseButtonDown(0))
            {
                if (hit.transform.gameObject.TryGetComponent(out ILeftClickable clickable))
                    clickable.OnLeftClick();
            }

            if (Input.GetMouseButtonDown(1))
            {
                if (hit.transform.gameObject.TryGetComponent(out IRightClickable clickable))
                    clickable.OnRightClick();
            }

            BuildingInput();
        }

        private void BuildingInput()
        {
            var building = GameManager.Instance.SelectedBuildingController;

            if (building is null || building.isPlaced) return;

            building.buildingMovementController.Move(camera.ScreenToWorldPoint(Input.mousePosition));

            if (Input.GetMouseButtonUp(0))
            {
                if (GameBoardManager.Instance.IsPlacementValid(Utilities.Utilities.CreateRect(building.transform.position,
                        building.Data.buildingData.size)))
                    building.buildingMovementController.Place();
                else
                {
                    BuildingFactory.Instance.ReleaseItem(building);
                    BuildingSignals.Instance.onBuildingReleased.Invoke(building.Data.buildingData);
                }
            }

            if (Input.GetMouseButtonDown(1))
            {
                BuildingFactory.Instance.ReleaseItem(building);
                BuildingSignals.Instance.onBuildingReleased.Invoke(building.Data.buildingData);
            }
        }
    }
}