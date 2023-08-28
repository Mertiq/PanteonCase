using UnityEngine;

public class BuildingMovement : MonoBehaviour
{
    [SerializeField] private Building building;

    private void Update()
    {
        if (building.isPlaced) return;

        Move();

        if (Input.GetMouseButtonDown(1))
        {
            BuildingCreator.Instance.ReleaseBuilding(building);
        }

        if (Input.GetMouseButtonUp(0))
        {
            if (GameBoard.Instance.IsPlacementValid(building.CreateRect()))
                Place();
            else
                BuildingCreator.Instance.ReleaseBuilding(building);
        }
    }

    private void Move()
    {
        if (building.isPlaced) return;

        transform.position = ClampPosition();

        building.MovementError(!GameBoard.Instance.IsPlacementValid(building.CreateRect()));
    }

    private Vector3 ClampPosition()
    {
        var mousePos = Camera.main!.ScreenToWorldPoint(Input.mousePosition);

        var targetPos = new Vector3(Extensions.CustomRound(mousePos.x, .5f), Extensions.CustomRound(mousePos.y, .5f),
            0);

        var bounds = GameBoard.Instance.bounds;

        const float boardScaleFactor = Config.BoardScaleFactor;

        var minX = bounds.x + building.data.size.x / boardScaleFactor / 2;
        var maxX = -bounds.x - building.data.size.x / boardScaleFactor / 2;
        var minY = bounds.y + building.data.size.y / boardScaleFactor / 2;
        var maxY = -bounds.y - building.data.size.y / boardScaleFactor / 2;

        targetPos = building.data.size.y % 2 == 0 ? targetPos : new Vector3(targetPos.x, targetPos.y + 0.25f);

        return new Vector3(Mathf.Clamp(targetPos.x, minX, maxX),
            Mathf.Clamp(targetPos.y, minY, maxY), 0);
    }

    private void Place()
    {
        GameBoard.Instance.FillLocation(building.CreateRect());
        building.isPlaced = true;
    }
}