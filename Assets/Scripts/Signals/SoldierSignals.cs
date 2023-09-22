using Controllers;
using Controllers.SoldierControllers;
using Extensions;
using Interfaces;
using UnityEngine;
using UnityEngine.Events;

namespace Signals
{
    public class SoldierSignals : SingletonMonoBehaviour<SoldierSignals>
    {
        public UnityAction<SoldierController> onSoldierClickWithLeft = delegate { };
        public UnityAction<IDamageable, Vector3, Vector2Int> onSoldierClickWithRight = delegate { };
        public UnityAction<IDamageable> onSoldierStartAttack = delegate { };
    }
}