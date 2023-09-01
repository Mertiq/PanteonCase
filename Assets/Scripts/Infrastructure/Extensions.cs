using System;
using System.Collections.Generic;
using UnityEngine;

public static class Extensions
{
    public static float CustomRound(float value, float roundingBase) =>
        Mathf.Round(value / roundingBase) * roundingBase;

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

    public static float GetDistance(this Tile nodeA, Tile nodeB)
    {
        var nodeAPosition = nodeA.transform.position;
        var nodeBPosition = nodeB.transform.position;
        var x = Mathf.Abs(nodeAPosition.x - nodeBPosition.x);
        var y = Mathf.Abs(nodeAPosition.y - nodeBPosition.y);

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

                var position = tile.transform.position;
                var checkX = position.x + x;
                var checkY = position.y + y;

                if (checkX >= 0 && checkX < gameBoard.boardSize.x && checkY >= 0 && checkY < gameBoard.boardSize.y)
                {
                    neighbours.Add(gameBoard.tiles[new Vector2(checkX, checkY)]);
                }
            }
        }

        return neighbours;
    }

    public static List<Tile> RetracePath(this Tile startNode, Tile endNode)
    {
        var path = new List<Tile>();
        var currentNode = endNode;

        while (currentNode != startNode)
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
}