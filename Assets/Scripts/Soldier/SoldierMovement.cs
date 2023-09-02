using System.Collections;
using UnityEngine;

public class SoldierMovement : MonoBehaviour
{
    private Soldier lastSelectedSoldier;
    private float speed = .3f;

    public void SetSoldier(params object[] args)
    {
        lastSelectedSoldier = (Soldier)args[0];
    }

    public void Move(params object[] args)
    {
        var tile = (Tile)args[0];

        if (lastSelectedSoldier is null) return;

        Pathfinding.FindPath(lastSelectedSoldier.position, tile.position);

        StartCoroutine(FollowPath());
    }

    private IEnumerator FollowPath()
    {
        var path = GameBoard.Instance.path;
        var targetIndex = 0;
        var currentWaypoint = path[targetIndex];
        while (true)
        {
            if (lastSelectedSoldier.position == currentWaypoint.position)
            {
                targetIndex++;
                if (targetIndex >= path.Count)
                    yield break;
                currentWaypoint = path[targetIndex];
            }
            
            yield return new WaitForSeconds(speed);

            lastSelectedSoldier.Move(currentWaypoint.position);
            
        }
    }
}