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
        [SerializeField] private Button _repairButton;
        [SerializeField] private Button _closeButton;
        [SerializeField] private Button _actionButton;
        [SerializeField] private Text _text;

        #region UnityEvents
        private void Start()
        {
            _canvasPanel.SetActive(false);

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
        }

        private void UpdateText()
        {
            var cost = Workshop.CurrentRepairCost();

            _text.text = $"Стоимость починки корабля - {cost} монет"; // Временный текст.
        }

        private void CloseWorkshop()
        {
            _canvasPanel.SetActive(false);
            _actionButton.gameObject.SetActive(true);

            Player.Instance.GiveControlsToPlayer();
        }

        public void OpenWorkshop()
        {
            _canvasPanel.SetActive(true);
            _actionButton.gameObject.SetActive(false);

            Player.Instance.TakeControlsFromPlayer();

            CheckButtonAppearance();
            UpdateText();
        }
    }
}