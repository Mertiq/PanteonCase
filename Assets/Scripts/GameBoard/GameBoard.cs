using UnityEngine;

public class GameBoard : MonoBehaviour
{
    [SerializeField] private GameObject tilePrefab;
    [SerializeField] private Transform tileHolder;
    [SerializeField] private Vector2Int boardSize;

    private void Start()
    {
        InitializeBoard();
    }

    private void InitializeBoard()
    {
        for (var x = -boardSize.x / 2; x < boardSize.x / 2 + (boardSize.x % 2 == 1 ? 1 : 0); x++)
        {
            for (var y = -boardSize.y / 2; y < boardSize.y / 2 + (boardSize.y % 2 == 1 ? 1 : 0); y++)
            {
                var tile = Instantiate(tilePrefab, tileHolder);
                var position = new Vector3(x, y) * tilePrefab.transform.localScale.x;
                tile.transform.SetLocalPositionAndRotation(position, Quaternion.identity);
                tile.name = $"Tile ({x}, {y})";
            }
        }
    }
}