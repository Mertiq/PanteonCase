using UnityEngine;

public class Building : MonoBehaviour, ISetupable, IClickable
{
    [SerializeField] private GameEvent onBuildingSelected;

    public SpriteRenderer iconImage;
    public SpriteRenderer errorImage;

    [HideInInspector] public bool isPlaced;
    [HideInInspector] public BuildingData data;

    public void Setup(params object[] args)
    {
        data = (BuildingData)args[0];
        iconImage.sprite = data.sprite;
        transform.localScale = new Vector3(data.size.x, data.size.y) / Config.BoardScaleFactor;
    }

    public void ResetView(params object[] args)
    {
        throw new System.NotImplementedException();
    }

    public void MovementError(bool flag) => errorImage.gameObject.SetActive(flag);

    public void OnClick()
    {
        onBuildingSelected.Raise(data);
        GameManager.Instance.SetActiveBuilding(this);
    }
}