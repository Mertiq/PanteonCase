using Controllers.BuildingControllers;
using Data.ValueObjects;
using Extensions;
using Interfaces;
using UnityEngine;
using UnityEngine.Events;

namespace Signals
{
    public class BuildingSignals : SingletonMonoBehaviour<BuildingSignals>
    {
        public UnityAction<IDamageable, Vector3, Vector2Int> onBuildingClickedWithRight = delegate { };
        public UnityAction<BuildingController> onBuildingClickedWithLeft = delegate { };
        public readonly UnityAction<BuildingData> onBuildingReleased = delegate { };
        public UnityAction<BuildingController> onBuildingPlaced = delegate { };
        public UnityAction onSelectedBuildingDead = delegate { };
    }
}