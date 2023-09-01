using UnityEngine;

public class Soldier : MonoBehaviour, ISetupable
{
    [HideInInspector] public SoldierData data;

    public void Setup(params object[] args)
    {
        data = (SoldierData)args[0];
        var spawnPos = (Vector2)args[1];
        Debug.Log(spawnPos);
        transform.position = spawnPos;
    }

    public void ResetView(params object[] args)
    {
        throw new System.NotImplementedException();
    }
}