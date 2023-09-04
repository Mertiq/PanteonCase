using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameBoard : SingletonMonoBehaviour<GameBoard>
{
    [SerializeField] private Tile tilePrefab;
    [SerializeField] private Transform tileHolder;
    [HideInInspector] public Vector2Int boardSize;
    [HideInInspector] public Rect bounds;

    public Dictionary<Vector2, Tile> tiles = new();
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
                var position = new Vector2(x, y) / Config.BoardScaleFactor;
                tile.Setup(position.x, position.y);
                tiles.Add(new Vector2(position.x, position.y), tile);
            }
        }
    }

    public void SetTiles(Rect location, bool flag)
    {
        if (flag)
            filledLocations.Remove(location);
        else
            filledLocations.Add(location);

        for (var i = location.xMin; i < location.xMax; i += .5f)
        {
            for (var j = location.yMin; j < location.yMax; j += .5f)
            {
                var tile = tiles[new Vector2(i, j)];

                if (tile != null)
                    tile.isWalkable = flag;
            }
        }
    }

    public bool IsPlacementValid(Rect building) =>
        filledLocations.All(fullPosition => !fullPosition.Overlaps(building));

    public bool IsInBounds(Vector2 pos)
    {
        var isInBoundsOnX = pos.x >= bounds.xMin && pos.x < bounds.xMax;
        var isInBoundsOnY = pos.y >= bounds.yMin && pos.y < bounds.yMax;
        return isInBoundsOnX && isInBoundsOnY;
    }

}