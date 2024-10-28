using UnityEngine;
using UnityEngine.UI;

namespace CthulhuGame
{
    public class WeightUI : MonoBehaviour
    {
        [SerializeField] private Image _image;

        #region UnityEvents
        private void Awake()
        {
            _image.fillAmount = 0f;
        }

        private void Start()
        {
            UpdateImage();
            Player.Instance.Ship.OnWeightChanged += UpdateImage;  
        }

        private void OnDestroy()
        {
            UpdateImage();
            Player.Instance.Ship.OnWeightChanged -= UpdateImage;
        }
        #endregion

        private void UpdateImage()
        {
            var ship = Player.Instance.Ship;

            _image.fillAmount = (float) ship.CurrentWeight / ship.CarryingCapacity;
        }
    }
}