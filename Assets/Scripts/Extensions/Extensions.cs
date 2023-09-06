using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class Extensions
{
    public static Rect CreateRect(this Building building)
    {
        const float boardScaleFactor = Config.BoardScaleFactor;
        var size = building.data.size;
        var position = building.transform.position;

        var width = size.x / boardScaleFactor;
        var height = size.y / boardScaleFactor + 1 / boardScaleFactor;
        var minX = position.x - width / 2;
        var minY = position.y - height / 2;

        return new Rect(minX, minY - .25f, width, height);
    }

    public static float GetDistance(this Tile tileA, Tile tileB)
    {
        var tileAPosition = tileA.position;
        var tileBPosition = tileB.position;
        var x = Mathf.Abs(tileAPosition.x - tileBPosition.x);
        var y = Mathf.Abs(tileAPosition.y - tileBPosition.y);

        if (x > y)
            return 14 * y + 10 * (x - y);
        return 14 * x + 10 * (y - x);
    }

    public static List<Tile> GetNeighbours(this Tile tile)
    {
        var neighbours = new List<Tile>();
        var gameBoard = GameBoard.Instance;

        for (var x = -0.5f; x <= 0.5; x += 0.5f)
        {
            for (var y = -0.5f; y <= 0.5; y += 0.5f)
            {
                if (x == 0 && y == 0)
                    continue;

                var position = tile.position;
                var newPos = new Vector2(position.x + x, position.y + y);

                if (gameBoard.IsInBounds(newPos))
                {
                    neighbours.Add(gameBoard.tiles[newPos]);
                }
            }
        }

        return neighbours;
    }

    public static List<Tile> RetracePath(this Tile startTile, Tile endTile)
    {
        var path = new List<Tile>();
        var currentNode = endTile;

        while (currentNode != startTile)
        {
            path.Add(currentNode);
            currentNode = currentNode.parent;
        }

        path.Reverse();

        return path;
    }

    public static BuildingType GetBuildingType(this BuildingData data)
    {
        return data switch
        {
            BarrackData => BuildingType.Barrack,
            PowerPlantData => BuildingType.PowerPlant,
            _ => throw new ArgumentOutOfRangeException($"{data} type is not implemented!")
        };
    }

    public static List<Tile> GetEmptyNeighbourTiles(this Building building)
    {
        var neighbours = new List<Tile>();
        var rect = building.CreateRect();

        const float diameter = Config.Diameter;

        var minX = rect.xMin - diameter;
        var minY = rect.yMin - diameter;
        var maxX = rect.xMax + diameter;
        var maxY = rect.yMax + diameter;

        for (var i = minY; i < maxY; i += diameter)
        {
            for (var j = minX; j < maxX; j += diameter)
            {
                var tilePos = new Vector2(j, i);
                if (!GameBoard.Instance.IsInBounds(tilePos)) continue;
                var tile = Utilities.GetTile(tilePos);
                if (tile.isWalkable)
                    neighbours.Add(tile);
            }
        }

        return neighbours;
    }

    public static float GetCostOfPath(this List<Tile> path) => path.Sum(tile => tile.FCost);
}