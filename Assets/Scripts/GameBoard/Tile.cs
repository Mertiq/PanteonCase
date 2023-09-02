using UnityEngine;

public class Tile : MonoBehaviour, IClickable
{
    [SerializeField] private GameEvent onEmptyTileSelected;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [HideInInspector] public bool isWalkable;
    [HideInInspector] public float gCost;
    [HideInInspector] public float hCost;
    [HideInInspector] public Tile parent;
    [HideInInspector] public Vector2 position;

    public float FCost => gCost + hCost;

    private void Start()
    {
        isWalkable = true;
    }

    public void Setup(float x, float y)
    {
        transform.SetLocalPositionAndRotation(new Vector3(x, y), Quaternion.identity);
        name = $"Tile ({x}, {y})";
        position = new Vector2(x, y);
    }

    public void OnClick()
    {
        if (!isWalkable) return;
        onEmptyTileSelected.Raise(this);
    }

    public void SetSprite()
    {
        spriteRenderer.color = Color.green;
    }
}