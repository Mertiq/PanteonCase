using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class Soldier : MonoBehaviour, ISetupable, ILeftClickable
{
    [SerializeField] private GameEvent onSoldierSelected;
    [HideInInspector] public SoldierData data;
    [HideInInspector] public Vector2 position;
    [HideInInspector] public bool isMoving;
    private float speed = .3f;
    Vector2 offset = new(1 / Config.BoardScaleFactor / 2, 1 / Config.BoardScaleFactor / 2);

    public void Setup(params object[] args)
    {
        data = (SoldierData)args[0];
        var spawnPos = (Vector2)args[1];
        transform.position = spawnPos;

        position = spawnPos - offset;
    }

    private void Move(Vector2 newPos)
    {
        isMoving = true;
        transform.DOMove(newPos + offset, Time.deltaTime);
        position = newPos;
    }

    public void ResetView(params object[] args)
    {
        throw new System.NotImplementedException();
    }

    public void OnLeftClick()
    {
        onSoldierSelected.Raise(this);
    }

    public IEnumerator FollowPath(List<Tile> path)
    {
        var targetIndex = 0;
        var currentWaypoint = path[targetIndex];
        while (true)
        {
            if (position == currentWaypoint.position)
            {
                targetIndex++;
                if (targetIndex >= path.Count)
                {
                    isMoving = false;
                    yield break;
                }

                currentWaypoint = path[targetIndex];
            }

            yield return new WaitForSeconds(speed);

            Move(currentWaypoint.position);
        }
    }
}