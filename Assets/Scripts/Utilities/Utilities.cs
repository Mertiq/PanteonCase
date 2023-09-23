using System.Collections.Generic;
using System.Linq;
using Controllers;
using Data;
using Data.ValueObjects;
using Extensions;
using Managers;
using UnityEngine;

namespace Utilities
{
    public static class Utilities
    {
        public static Rect CreateRect(Vector3 position, Vector2 size)
        {
            const float boardScaleFactor = Config.BoardScaleFactor;

            var width = size.x / boardScaleFactor;
            var height = size.y / boardScaleFactor + 1 / boardScaleFactor;
            var minX = position.x - width / 2;
            var minY = position.y - height / 2 + Config.TileRadius;

            return new Rect(minX, minY, width, height);
        }

        public static List<TileController> FindPath(Vector2 seekerPos, Vector2 targetPos)
        {
            var startTile = GameBoardManager.Instance.tiles[seekerPos];
            var endTile = GameBoardManager.Instance.tiles[targetPos];

            var openSet = new List<TileController>();
            var closedSet = new HashSet<TileController>();

            openSet.Add(startTile);

            while (openSet.Count > 0)
            {
                var tile = openSet[0];

                foreach (var t in openSet
                             .Where(t =>
                                 t.pathfindingTile.FCost < tile.pathfindingTile.FCost ||
                                 t.pathfindingTile.FCost == tile.pathfindingTile.FCost)
                             .Where(t =>
                                 t.pathfindingTile.hCost < tile.pathfindingTile.hCost))
                {
                    tile = t;
                }

                openSet.Remove(tile);
                closedSet.Add(tile);

                if (tile == endTile)
                {
                    return startTile.RetracePath(endTile);
                }

                foreach (var neighbour in tile.GetNeighbours())
                {
                    if (!neighbour.isWalkable || closedSet.Contains(neighbour))
                        continue;

                    var newCostToNeighbour = tile.pathfindingTile.gCost + tile.GetDistance(neighbour);
                    if (newCostToNeighbour < neighbour.pathfindingTile.gCost || !openSet.Contains(neighbour))
                    {
                        neighbour.pathfindingTile =
                            new PathfindingTile(tile, newCostToNeighbour, neighbour.GetDistance(endTile));

                        if (!openSet.Contains(neighbour))
                        {
                            openSet.Add(neighbour);
                        }
                    }
                }
            }

            return null;
        }

        public static float CustomRound(float value, float roundingBase) =>
            Mathf.Round(value / roundingBase) * roundingBase;

        public static TileController GetTile(Vector2 pos) => GameBoardManager.Instance.tiles[pos];

        public static List<TileController> FindNearestPath(Rect rect)
        {
            var emptyNeighbourTiles = rect.GetEmptyNeighbourTiles();

            var minCost = float.MaxValue;
            List<TileController> nearestPath = null;
            foreach (var emptyNeighbourTile in emptyNeighbourTiles)
            {
                var path = FindPath(GameManager.Instance.SelectedSoldierController.Position,
                    emptyNeighbourTile.position);
                var newCost = path.GetCostOfPath();

                if (newCost < minCost)
                {
                    minCost = newCost;
                    nearestPath = path;
                }
            }

            return nearestPath;
        }
    }
}