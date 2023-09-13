using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class ProducerBuilding : Building
{
    public GameObject spawnPoint;
    public Vector2 spawnPointPos;
    public List<Soldier> soldiers = new();

    public void CalculateSpawnPoint(params object[] args)
    {
        if (this != (Building)args[0]) return;

        var rect = this.CreateRect();

        var x = rect.xMin;
        var y = rect.yMin;
        spawnPointPos = new Vector2(x + Config.TileRadius, y - Config.TileRadius);
        GameBoard.Instance.SetTiles(new Rect(new Vector2(x, y - Config.Diameter), Vector2.one / 2), false);
    }

    public bool IsSpawnPointEmpty() => soldiers.All(soldier =>
        soldier.position != spawnPointPos - new Vector2(Config.TileRadius, Config.TileRadius));
}