using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameBoard : SingletonMonoBehaviour<GameBoard>
{
    [SerializeField] private GameObject tilePrefab;
    [SerializeField] private Transform tileHolder;
    [SerializeField] private Vector2Int boardSize;
    [HideInInspector] public Rect bounds;

    public List<Rect> filledLocations;

    private void Start()
    {
        InitializeBoard();
        var realBoardSize = new Vector2(boardSize.x / Config.BoardScaleFactor, boardSize.y / Config.BoardScaleFactor);
        bounds = new Rect(new Vector2(-realBoardSize.x / 2, -realBoardSize.y / 2), realBoardSize);
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

    public void FillLocation(Rect location) => filledLocations.Add(location);

    public bool IsPlacementValid(Rect building) =>
        filledLocations.All(fullPosition => !fullPosition.Overlaps(building));
}