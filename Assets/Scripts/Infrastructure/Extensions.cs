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
        var height = size.y / boardScaleFactor;
        var minX = position.x - width / 2;
        var minY = position.y - height / 2;

        return new Rect(minX, minY, width, height);
    }
}