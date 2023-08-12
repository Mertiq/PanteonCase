using UnityEngine;

public class GameBoard : MonoBehaviour
{
    [SerializeField] private GameObject tilePrefab;
    [SerializeField] private Vector2Int boardSize;

    private void Start()
    {
        InitializeBoard();
    }

    private void InitializeBoard()
    {
        for (var x = -boardSize.x / 2; x < boardSize.x / 2; x++)
        {
            for (var y = -boardSize.y / 2; y < boardSize.y / 2; y++)
            {
                var tile = Instantiate(tilePrefab, transform);
                var position = new Vector3(x, y) * tilePrefab.transform.localScale.x;
                tile.transform.SetLocalPositionAndRotation(position, Quaternion.identity);
            }
        }
    }
}