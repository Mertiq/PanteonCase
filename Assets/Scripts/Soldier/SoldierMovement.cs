using UnityEngine;

public class SoldierMovement : MonoBehaviour
{
    private Soldier lastSelectedSoldier;

    public void SetSoldier(params object[] args) => lastSelectedSoldier = (Soldier)args[0];

    public void Move(params object[] args)
    {
        var tile = (Tile)args[0];

        if (lastSelectedSoldier is null) return;

        var path = Pathfinding.FindPath(lastSelectedSoldier.position, tile.position);

        if (path is not null && !lastSelectedSoldier.isMoving)
            StartCoroutine(lastSelectedSoldier.FollowPath(path));
    }
}