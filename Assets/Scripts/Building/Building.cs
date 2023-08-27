using UnityEngine;

public class Building : MonoBehaviour
{
    public SpriteRenderer image;
    private BuildingData data;
    private bool isPlaced;

    private void Update()
    {
        if (isPlaced) return;

        if (Input.GetMouseButtonDown(1))
        {
            //Release 
        }

        Move();
    }

    public void Setup(BuildingData buildingData)
    {
        data = buildingData;
        image.sprite = data.sprite;
        transform.localScale = new Vector3(data.size.x, data.size.y) / Config.BoardScaleFactor;
    }

    private void Move()
    {
        if (isPlaced) return;

        var cameraMain = Camera.main;
        var pos = cameraMain.ScreenToWorldPoint(Input.mousePosition);
        var x = Extensions.CustomRound(pos.x, .5f);
        var y = Extensions.CustomRound(pos.y, .5f);

        transform.position = new Vector3(x, y, 0);
    }

    private void Place()
    {
        isPlaced = true;
    }
}