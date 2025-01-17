using UnityEngine;
using UnityEngine.UI;

namespace CthulhuGame
{
    /// <summary>
    /// Интерфейс мастерской.
    /// </summary>
    public class WorkshopUI : MonoBehaviour
    {
        [SerializeField] private GameObject _canvasPanel;
        [SerializeField] private Image _image;
        [SerializeField] private Button _repairButton;
        [SerializeField] private Button _closeButton;
        [SerializeField] private Button _actionButton;
        [SerializeField] private Text _text;

        #region UnityEvents
        private void Start()
        {
            _canvasPanel.SetActive(false);
            _image.enabled = false;

            _repairButton.onClick.AddListener(RepairShip);
            _closeButton.onClick.AddListener(CloseWorkshop);
        }

        private void OnDestroy()
        {
            _repairButton.onClick.RemoveListener(RepairShip);
            _closeButton.onClick.RemoveListener(CloseWorkshop);
        }
        #endregion

        private void CheckButtonAppearance()
        {
            if ((Workshop.CurrentRepairCost() > 0) && 
                (Player.Instance.Money.CurrentMoney >= Workshop.CurrentRepairCost()))
            {
                _repairButton.interactable = true;
            }
            else
            {
                _repairButton.interactable = false;
            }
        }

        private void RepairShip()
        {
            Workshop.TryRepairShip();

            CheckButtonAppearance();
            UpdateText();
            CloseWorkshop();
        }

        private void UpdateText()
        {
            var cost = Workshop.CurrentRepairCost();

            _text.text = $"Стоимость починки корабля - {cost} монет"; // Временный текст.
        }

        private void CloseWorkshop()
        {
            _image.enabled = false;
            
            _canvasPanel.SetActive(false);
            _actionButton.gameObject.SetActive(true);

            Player.Instance.GiveControlsToPlayer();
        }

        public void OpenWorkshop()
        {
            _image.enabled=true;
            
            _canvasPanel.SetActive(true);
            _actionButton.gameObject.SetActive(false);

            Player.Instance.TakeControlsFromPlayer();

            CheckButtonAppearance();
            UpdateText();
        }
    }
}