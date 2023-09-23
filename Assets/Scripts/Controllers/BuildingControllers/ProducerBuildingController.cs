using System.Collections.Generic;
using System.Linq;
using Controllers.SoldierControllers;
using Data;
using Data.ValueObjects;
using Managers;
using Signals;
using UnityEngine;

namespace Controllers.BuildingControllers
{
    public class ProducerBuildingController : BuildingController
    {
        public Vector2 soldierSpawnPoint;
        public Vector2 spawnPoint;
        public List<SoldierController> soldiers = new();

        private void CalculateSpawnPoint(BuildingController buildingController)
        {
            if (this != buildingController) return;

            var rect = Utilities.Utilities.CreateRect(buildingController.transform.position,
                buildingController.Data.buildingData.size);

            var x = rect.xMin;
            var y = rect.yMin;

            soldierSpawnPoint = new Vector2(x + Config.TileRadius, y - Config.TileRadius);
            spawnPoint = new Vector2(x, y - Config.TileDiameter);
            var spawnPointRect = new Rect(spawnPoint, Vector2.one / 2);

            GameBoardManager.Instance.SetTiles(spawnPointRect, false);
            GameBoardManager.Instance.AddSpawnPoint(spawnPoint);
        }

        public bool IsSpawnPointEmpty() => soldiers.All(soldier =>
            soldier.Position != soldierSpawnPoint - new Vector2(Config.TileRadius, Config.TileRadius));


        private void OnEnable() => BuildingSignals.Instance.onBuildingPlaced += CalculateSpawnPoint;
        private void OnDisable() => BuildingSignals.Instance.onBuildingPlaced -= CalculateSpawnPoint;
    }
}