using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Image image;

    public void SetHealthBar(float currentAmount, float maxAmount)
    {
        image.fillAmount = currentAmount / maxAmount;
    }
}
