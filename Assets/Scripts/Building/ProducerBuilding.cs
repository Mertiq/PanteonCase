using UnityEngine;

public class ProducerBuilding : Building
{
    public Vector2 spawnPoint;

    public void CalculateSpawnPoint(params object[] args)
    {
        if (this != (Building) args[0]) return;
        
        var rect = this.CreateRect();
        const float offset = 1 / Config.BoardScaleFactor / 2;
        var x = rect.xMin + offset;
        var y = rect.yMin + offset;
        spawnPoint = new Vector2(x, y);
    }
    
}