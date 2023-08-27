using UnityEngine;

public static class Extensions
{
    public static float CustomRound(float value, float roundingBase) =>
        Mathf.Round(value / roundingBase) * roundingBase;
}