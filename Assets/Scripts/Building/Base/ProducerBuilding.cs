using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ProducerBuilding : Building
{
    public Vector2 spawnPoint;
    public List<Soldier> soldiers = new();

    public void CalculateSpawnPoint(params object[] args)
    {
        if (this != (Building)args[0]) return;

        var rect = this.CreateRect();

        var x = rect.xMin + Config.TileRadius;
        var y = rect.yMin + Config.TileRadius;
        spawnPoint = new Vector2(x, y);
    }

    public bool IsSpawnPointEmpty() => soldiers.All(soldier =>
        soldier.position != spawnPoint - new Vector2(Config.TileRadius, Config.TileRadius));
}