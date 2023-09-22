using System.Collections.Generic;
using System.Linq;
using Controllers;
using Data;
using Data.ValueObjects;
using Managers;
using UnityEngine;

namespace Extensions
{
    public static class Extensions
    {
        public static float GetDistance(this TileController tileA, TileController tileB)
        {
            var x = Mathf.Abs(tileA.position.x - tileB.position.x);
            var y = Mathf.Abs(tileA.position.y - tileB.position.y);

            if (x > y)
                return 14 * y + 10 * (x - y);
            return 14 * x + 10 * (y - x);
        }

        public static List<TileController> GetNeighbours(this TileController tileController)
        {
            var neighbours = new List<TileController>();
            var gameBoard = GameBoardManager.Instance;

            for (var x = -0.5f; x <= 0.5; x += 0.5f)
            {
                for (var y = -0.5f; y <= 0.5; y += 0.5f)
                {
                    if (x == 0 && y == 0)
                        continue;

                    var position = tileController.position;
                    var newPos = new Vector2(position.x + x, position.y + y);

                    if (gameBoard.IsInBounds(newPos))
                    {
                        neighbours.Add(gameBoard.tiles[newPos]);
                    }
                }
            }

            return neighbours;
        }

        public static List<TileController> RetracePath(this TileController startTileController, TileController endTileController)
        {
            var path = new List<TileController>();
            var currentNode = endTileController;

            while (currentNode != startTileController)
            {
                path.Add(currentNode);
                currentNode = currentNode.pathfindingTile.parent;
            }

            path.Reverse();

            return path;
        }

        public static List<TileController> GetEmptyNeighbourTiles(this Rect rect)
        {
            var neighbours = new List<TileController>();

            const float diameter = Config.TileDiameter;

            var minX = rect.xMin - diameter;
            var minY = rect.yMin - diameter;
            var maxX = rect.xMax + diameter;
            var maxY = rect.yMax + diameter;

            for (var i = minY; i < maxY; i += diameter)
            {
                for (var j = minX; j < maxX; j += diameter)
                {
                    var tilePos = new Vector2(j, i);
                    if (!GameBoardManager.Instance.IsInBounds(tilePos)) continue;
                    var tile = Utilities.Utilities.GetTile(tilePos);
                    if (tile.isWalkable)
                        neighbours.Add(tile);
                }
            }

            return neighbours;
        }

        public static float GetCostOfPath(this List<TileController> path) => path.Sum(tile => tile.pathfindingTile.FCost);
    }
}