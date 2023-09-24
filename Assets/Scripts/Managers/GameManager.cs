using Controllers.BuildingControllers;
using Controllers.SoldierControllers;
using Extensions;
using Signals;

namespace Managers
{
    public class GameManager : SingletonMonoBehaviour<GameManager>
    {
        public BuildingController SelectedBuildingController { get; private set; }
        public SoldierController SelectedSoldierController { get; private set; }

        public void SetSelectedBuilding(BuildingController buildingController) =>
            SelectedBuildingController = buildingController;

        private void SetSelectedSoldier(SoldierController soldierController) =>
            SelectedSoldierController = soldierController;

        private void OnEnable() => SubscribeEvents();

        private void SubscribeEvents()
        {
            BuildingSignals.Instance.onBuildingClickedWithLeft += SetSelectedBuilding;
            SoldierSignals.Instance.onSoldierClickWithLeft += SetSelectedSoldier;
        }

        private void OnDisable() => UnSubscribeEvents();

        private void UnSubscribeEvents()
        {
            BuildingSignals.Instance.onBuildingClickedWithLeft -= SetSelectedBuilding;
            SoldierSignals.Instance.onSoldierClickWithLeft -= SetSelectedSoldier;
        }
    }
}