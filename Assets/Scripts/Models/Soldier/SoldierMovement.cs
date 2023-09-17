﻿using System.Collections.Generic;
using UnityEngine;

public class SoldierMovement : MonoBehaviour
{
    private Soldier lastSelectedSoldier;

    public void SetSoldier(params object[] args) => lastSelectedSoldier = (Soldier)args[0];

    public void MoveToBuilding(params object[] args)
    {
        var building = (Building)args[0];

        var nearestPath = FindNearestPath(building.CreateRect());

        if (nearestPath is not null)
        {
            StartCoroutine(lastSelectedSoldier.FollowPath(nearestPath, building));
        }
    }

    public void MoveToSoldier(params object[] args)
    {
        var soldier = (Soldier)args[0];

        var nearestPath = FindNearestPath(soldier.CreateRect());

        if (nearestPath is not null)
        {
            StartCoroutine(lastSelectedSoldier.FollowPath(nearestPath, soldier));
        }
    }

    public void Move(params object[] args)
    {
        var tile = (Tile)args[0];

        if (lastSelectedSoldier is null) return;

        var path = Utilities.FindPath(lastSelectedSoldier.position, tile.position);

        if (path is not null)
            StartCoroutine(lastSelectedSoldier.FollowPath(path));
    }

    private List<Tile> FindNearestPath(Rect rect)
    {
        var emptyNeighbourTiles = rect.GetEmptyNeighbourTiles();

        var minCost = float.MaxValue;
        List<Tile> nearestPath = null;
        foreach (var emptyNeighbourTile in emptyNeighbourTiles)
        {
            var path = Utilities.FindPath(lastSelectedSoldier.position, emptyNeighbourTile.position);
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