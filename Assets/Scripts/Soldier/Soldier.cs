using DG.Tweening;
using UnityEngine;

public class Soldier : MonoBehaviour, ISetupable, IClickable
{
    [SerializeField] private GameEvent onSoldierSelected;
    [HideInInspector] public SoldierData data;
    [HideInInspector] public Vector2 position;
    Vector2 offset = new Vector2(1 / Config.BoardScaleFactor / 2, 1 / Config.BoardScaleFactor / 2);

    public void Setup(params object[] args)
    {
        data = (SoldierData)args[0];
        var spawnPos = (Vector2)args[1];
        transform.position = spawnPos;

        position = spawnPos - offset;
    }

    public void Move(Vector2 newPos)
    {
        transform.DOMove(newPos + offset, Time.deltaTime);
        position = newPos;
    }

    public void ResetView(params object[] args)
    {
        throw new System.NotImplementedException();
    }

    public void OnClick()
    {
        onSoldierSelected.Raise(this);
    }
}