using UnityEngine;

public class BuildingMovement : MonoBehaviour
{
    [SerializeField] private Building building;
    [SerializeField] private GameEvent onBuildingRelease;
    [SerializeField] private GameEvent onBuildingPlaced;

    private void Update()
    {
        if (building.isPlaced) return;

        Move();

        if (Input.GetMouseButtonDown(1))
        {
            BuildingFactory.Instance.ReleaseBuilding(building);
            onBuildingRelease.Raise(building.data);
        }

        if (Input.GetMouseButtonUp(0))
        {
            if (GameBoard.Instance.IsPlacementValid(building.CreateRect()))
                Place();
            else
            {
                BuildingFactory.Instance.ReleaseBuilding(building);
                onBuildingRelease.Raise(building.data);
            }
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

        var targetPos = new Vector3(Utilities.CustomRound(mousePos.x, .5f), Utilities.CustomRound(mousePos.y, .5f),
            0);

        var bounds = GameBoard.Instance.bounds;

        const float boardScaleFactor = Config.BoardScaleFactor;
        var size = building.data.size;

        var minX = bounds.x + size.x / boardScaleFactor / 2;
        var maxX = -bounds.x - size.x / boardScaleFactor / 2;
        var minY = bounds.y + size.y / boardScaleFactor / 2;
        var maxY = (-bounds.y - size.y / boardScaleFactor / 2) - .5f;

        targetPos = size.y % 2 == 0 ? targetPos : new Vector3(targetPos.x, targetPos.y + 0.25f);

        return new Vector3(Mathf.Clamp(targetPos.x, minX, maxX),
            Mathf.Clamp(targetPos.y, minY + Config.Diameter, maxY), 0);
    }

    private void Place()
    {
        GameBoard.Instance.SetTiles(building.CreateRect(), false);
        building.isPlaced = true;
        onBuildingPlaced.Raise(building);
    }
}