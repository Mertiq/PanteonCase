using Controllers.GameBoardControllers;
using Extensions;
using UnityEngine.Events;

namespace Signals
{
    public class GameBoardSignals : SingletonMonoBehaviour<GameBoardSignals>
    {
        public UnityAction<TileController> onTileSelected = delegate { };
    }
}