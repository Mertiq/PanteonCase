using UnityEngine;
using UnityEngine.UI;

namespace Controllers.UIControllers
{
    public class HealthBarController : MonoBehaviour
    {
        [SerializeField] private Image fillImage;

        public void SetHealthBar(float currentAmount, float maxAmount) => fillImage.fillAmount = currentAmount / maxAmount;
    }
}