using Data.ValueObjects;
using Enums;
using Extensions;
using UnityEngine.Events;

namespace Signals
{
    public class UISignals : SingletonMonoBehaviour<UISignals>
    {
        public UnityAction<BuildingType> onBuildingSlotClicked = delegate { };
        public UnityAction<SoldierType> onSoldierSlotClicked = delegate { };
    }
}