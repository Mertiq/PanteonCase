using UnityEngine;

public class Building : MonoBehaviour, ISetupable, IRightClickable, ILeftClickable, IDamageable
{
    [SerializeField] private GameEvent onBuildingSelectedWithLeftClick;
    [SerializeField] private GameEvent onBuildingSelectedWithRightClick;
    [SerializeField] private HealthBar healthBar;

    public SpriteRenderer iconImage;
    public SpriteRenderer errorImage;

    [HideInInspector] public bool isPlaced;
    [HideInInspector] public BuildingData data;
    [HideInInspector] public float currentHealth;

    public void Setup(params object[] args)
    {
        data = (BuildingData)args[0];
        iconImage.sprite = data.sprite;
        transform.localScale = new Vector3(data.size.x, data.size.y) / Config.BoardScaleFactor;
        currentHealth = data.health;
    }

    public void ResetView(params object[] args)
    {
        isPlaced = false;
        healthBar.SetHealthBar(data.health, data.health);
        GameBoard.Instance.SetTiles(this.CreateRect(), true);
    }

    public void MovementError(bool flag) => errorImage.gameObject.SetActive(flag);

    public void OnRightClick()
    {
        onBuildingSelectedWithRightClick.Raise(this);
    }

    public void OnLeftClick()
    {
        onBuildingSelectedWithLeftClick.Raise(data);
        GameManager.Instance.SetActiveBuilding(this);
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        healthBar.SetHealthBar(currentHealth, data.health);
        if (!IsAlive())
            BuildingCreator.Instance.ReleaseBuilding(this);
    }

    public bool IsAlive() => currentHealth > 0;
}