using UnityEngine;

public class Building : MonoBehaviour
{
    public SpriteRenderer image;
    private BuildingData data;

    public void Setup(BuildingData buildingData)
    {
        data = buildingData;
        image.sprite = data.sprite;
    }
}