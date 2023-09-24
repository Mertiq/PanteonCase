using Data.ValueObjects;
using Managers;
using Signals;
using UnityEngine;

namespace Controllers.BuildingControllers
{
    public class BuildingMovementController : MonoBehaviour
    {
        private BuildingController buildingController;

        private void Awake() => buildingController = GetComponent<BuildingController>();

        public void Move(Vector3 mousePos)
        {
            transform.position = CalculateClampedPosition(mousePos);
            var rect = Utilities.Utilities.CreateRect(buildingController.transform.position,
                buildingController.Data.buildingData.size);
            buildingController.MovementError(rect);
        }

        public void Place()
        {
            GameBoardManager.Instance.SetTiles(Utilities.Utilities.CreateRect(transform.position, buildingController.Data.buildingData.size), false);
            buildingController.isPlaced = true;
            BuildingSignals.Instance.onBuildingPlaced.Invoke(buildingController);
        }

        private Vector3 CalculateClampedPosition(Vector3 mousePos)
        {
            const float diameter = Config.TileDiameter;
            var roundedMouseX = Utilities.Utilities.CustomRound(mousePos.x, diameter);
            var roundedMouseY = Utilities.Utilities.CustomRound(mousePos.y, diameter);

            var roundedMousePos = new Vector3(roundedMouseX, roundedMouseY, 0);

            var bounds = GameBoardManager.Instance.bounds;

            const float boardScaleFactor = Config.BoardScaleFactor;
            var size = buildingController.Data.buildingData.size;

            var minX = bounds.x + size.x / boardScaleFactor / 2;
            var maxX = -bounds.x - size.x / boardScaleFactor / 2;
            var minY = bounds.y + size.y / boardScaleFactor / 2;
            var maxY = (-bounds.y - size.y / boardScaleFactor / 2) - diameter;

            roundedMousePos = size.y % 2 == 0
                ? roundedMousePos
                : new Vector3(roundedMousePos.x, roundedMousePos.y + Config.TileRadius);

            var resultX = Mathf.Clamp(roundedMousePos.x, minX, maxX);
            var resultY = Mathf.Clamp(roundedMousePos.y, minY + Config.TileDiameter, maxY);

            return new Vector3(resultX, resultY, 0);
        }
    }
}