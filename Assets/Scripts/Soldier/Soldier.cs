using UnityEngine;

public class Soldier : MonoBehaviour, ISetupable, IClickable
{
    [SerializeField] private GameEvent onSoldierSelected;
    [HideInInspector] public SoldierData data;
    [HideInInspector] public Vector2 position;
    
    public void Setup(params object[] args)
    {
        data = (SoldierData)args[0];
        var spawnPos = (Vector2)args[1];
        transform.position = spawnPos;
        
        var offset = new Vector2(1 / Config.BoardScaleFactor / 2, 1 / Config.BoardScaleFactor / 2);
        position = spawnPos - offset;
    }

    public void ResetView(params object[] args)
    {
        throw new System.NotImplementedException();
    }
    
    public void OnClick()
    {
        Debug.Log(transform.position);
        onSoldierSelected.Raise(this);
    }
}