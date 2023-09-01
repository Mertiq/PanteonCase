using UnityEngine;

public class Tile : MonoBehaviour
{
    [HideInInspector] public bool isWalkable;
    [HideInInspector] public float gCost;
    [HideInInspector] public float hCost;
    [HideInInspector] public Tile parent;
    
    public float FCost => gCost + hCost;

    private void Start()
    {
        isWalkable = true;
    }

    public void Setup(float x, float y)
    {
        transform.SetLocalPositionAndRotation(new Vector3(x, y), Quaternion.identity);
        name = $"Tile ({x}, {y})";
    }
}