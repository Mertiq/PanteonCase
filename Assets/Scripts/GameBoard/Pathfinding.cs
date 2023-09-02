using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class Pathfinding
{
    public static void FindPath(Vector2 seekerPos, Vector2 targetPos)
    {
        var startTile = GameBoard.Instance.tiles[seekerPos];
        var endTile = GameBoard.Instance.tiles[targetPos];

        var openSet = new List<Tile>();
        var closedSet = new HashSet<Tile>();

        openSet.Add(startTile);

        while (openSet.Count > 0)
        {
            var tile = openSet[0];
            
            foreach (var t in openSet.Where(t => t.FCost < tile.FCost || t.FCost == tile.FCost)
                         .Where(t => t.hCost < tile.hCost))
            {
                tile = t;
            }

            openSet.Remove(tile);
            closedSet.Add(tile);

            if (tile == endTile)
            {
                GameBoard.Instance.path = startTile.RetracePath(endTile);
                
                return;
            }

            foreach (var neighbour in tile.GetNeighbours())
            {
                if (!neighbour.isWalkable || closedSet.Contains(neighbour))
                    continue;

                var newCostToNeighbour = tile.gCost + tile.GetDistance(neighbour);
                if (newCostToNeighbour < neighbour.gCost || !openSet.Contains(neighbour))
                {
                    neighbour.gCost = newCostToNeighbour;
                    neighbour.hCost = neighbour.GetDistance(endTile);
                    neighbour.parent = tile;

                    if (!openSet.Contains(neighbour))
                    {
                        openSet.Add(neighbour);
                    }
                }
            }
        }
    }
}