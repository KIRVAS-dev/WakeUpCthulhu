using UnityEngine;
using UnityEngine.UI;

namespace CthulhuGame
{
    /// <summary>
    /// Отображает здоровье корабля игрока в интерфейсе.
    /// </summary>
    public class HealthUI : MonoBehaviour
    {
        [SerializeField] private Image _image;

        #region UnityEvents
        private void Awake()
        {
            _image.fillAmount = 0f;
        }

        private void Start()
        {
            //Player.Instance.Ship.OnShipInitialized += UpdateImage;
            Player.Instance.Ship.Health.OnHealthChanged += UpdateImage;
        }

        private void OnDestroy()
        {
            //Player.Instance.Ship.OnShipInitialized -= UpdateImage;
            Player.Instance.Ship.Health.OnHealthChanged -= UpdateImage;
        }
        #endregion

        private void UpdateImage()
        {
            var health = Player.Instance.Ship.Health;

            _image.fillAmount = (float)health.CurrentHealth / health.MaxHealth;
        }
    }
}