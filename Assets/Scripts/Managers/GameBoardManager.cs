using System.Collections.Generic;
using System.Linq;
using Controllers;
using Data;
using Data.ValueObjects;
using Extensions;
using UnityEngine;

namespace Managers
{
    public class GameBoardManager : SingletonMonoBehaviour<GameBoardManager>
    {
        [SerializeField] private TileController tileControllerPrefab;
        [SerializeField] private Transform tileHolder;

        public Vector2Int boardSize;

        [Header("Do not fill from Inspector!")]
        public Rect bounds;

        public readonly Dictionary<Vector2, TileController> tiles = new();
        public List<Rect> filledLocations;

        private void Start()
        {
            InitializeBoard();
            var realBoardSize =
                new Vector2(boardSize.x / Config.BoardScaleFactor, boardSize.y / Config.BoardScaleFactor);
            bounds = new Rect(new Vector2(-realBoardSize.x / 2, -realBoardSize.y / 2), realBoardSize);
        }

        private void InitializeBoard()
        {
            var idCounter = 1;
            for (var x = -boardSize.x / 2; x < boardSize.x / 2 + (boardSize.x % 2 == 1 ? 1 : 0); x++)
            {
                for (var y = -boardSize.y / 2; y < boardSize.y / 2 + (boardSize.y % 2 == 1 ? 1 : 0); y++)
                {
                    var tile = Instantiate(tileControllerPrefab, tileHolder);
                    var position = new Vector2(x, y) / Config.BoardScaleFactor;
                    tile.Setup(position.x, position.y, idCounter++);
                    tiles.Add(new Vector2(position.x, position.y), tile);
                }
            }
        }

        public void SetTiles(Rect location, bool isWalkable)
        {
            if (isWalkable)
                filledLocations.Remove(location);
            else
                filledLocations.Add(location);

            for (var i = location.xMin; i < location.xMax; i += .5f)
            {
                for (var j = location.yMin; j < location.yMax; j += .5f)
                {
                    var tile = tiles[new Vector2(i, j)];

                    if (tile != null)
                        tile.isWalkable = isWalkable;
                }
            }
        }

        public bool IsPlacementValid(Rect building) =>
            filledLocations.All(fullPosition => !fullPosition.Overlaps(building));

        public bool IsInBounds(Vector2 pos)
        {
            var isInBoundsOnX = pos.x >= bounds.xMin && pos.x < bounds.xMax;
            var isInBoundsOnY = pos.y >= bounds.yMin && pos.y < bounds.yMax;
            return isInBoundsOnX && isInBoundsOnY;
        }
    }
}