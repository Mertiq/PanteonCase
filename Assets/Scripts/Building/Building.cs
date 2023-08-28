using UnityEngine;

public class Building : MonoBehaviour
{
    public SpriteRenderer iconImage;
    public SpriteRenderer errorImage;
    [HideInInspector] public bool isPlaced;
    [HideInInspector] public BuildingData data;

    public void Setup(BuildingData buildingData)
    {
        data = buildingData;
        iconImage.sprite = data.sprite;
        transform.localScale = new Vector3(data.size.x, data.size.y) / Config.BoardScaleFactor;
    }

    public void MovementError(bool flag) => errorImage.gameObject.SetActive(flag);
}