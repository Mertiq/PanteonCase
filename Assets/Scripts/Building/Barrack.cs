using UnityEngine;

public class Barrack : Building
{
    public void CalculateSpawnPoint(params object[] args)
    {
        var barrackData = (BarrackData)data;
        var rect = this.CreateRect();
        const float offset = 1 / Config.BoardScaleFactor / 2;
        var x = rect.xMin + offset;
        var y = rect.yMin + offset;
        barrackData.spawnPoint = new Vector2(x, y);
    }
}