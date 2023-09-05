using System.Collections.Generic;
using UnityEngine;

public class SoldierMovement : MonoBehaviour
{
    private Soldier lastSelectedSoldier;

    public void SetSoldier(params object[] args) => lastSelectedSoldier = (Soldier)args[0];

    public void MoveToBuilding(params object[] args)
    {
        var building = (Building)args[0];

        var nearestPath = FindNearestPath(building);

        if (nearestPath is not null && !lastSelectedSoldier.isMoving)
        {
            StartCoroutine(lastSelectedSoldier.FollowPath(nearestPath, building));
        }
    }

    public void Move(params object[] args)
    {
        var tile = (Tile)args[0];

        if (lastSelectedSoldier is null) return;

        var path = Pathfinding.FindPath(lastSelectedSoldier.position, tile.position);

        if (path is not null && !lastSelectedSoldier.isMoving)
            StartCoroutine(lastSelectedSoldier.FollowPath(path));
    }

    private List<Tile> FindNearestPath(Building building)
    {
        var emptyNeighbourTiles = building.GetEmptyNeighbourTiles();

        var minCost = float.MaxValue;
        List<Tile> nearestPath = null;
        foreach (var emptyNeighbourTile in emptyNeighbourTiles)
        {
            var path = Pathfinding.FindPath(lastSelectedSoldier.position, emptyNeighbourTile.position);
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