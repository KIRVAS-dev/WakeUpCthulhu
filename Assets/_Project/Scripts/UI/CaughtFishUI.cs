using UnityEngine;
using UnityEngine.UI;

namespace CthulhuGame
{
    public class CaughtFishUI : MonoBehaviour
    {
        [SerializeField] private GameObject _canvasPanel;
        [SerializeField] private FishCard _fishCard;
        [SerializeField] private Button _accept;
        [SerializeField] private Button _decline;
        [SerializeField] private GameObject _overweight;

        #region UnityEvents
        private void Start()
        {
            _canvasPanel.SetActive(false);
            _overweight.gameObject.SetActive(false);

            _accept.onClick.AddListener(DoOnAccept);
            _decline.onClick.AddListener(DoOnDecline);

            FishingChallenge.Instance.OnTryCatchFish += ActivatePanel;
            Player.Instance.FishingRod.OnFishAssigned += SetFishImage;
        }

        private void OnDestroy()
        {
            _accept.onClick.RemoveListener(DoOnAccept);
            _decline.onClick.RemoveListener(DoOnDecline);

            FishingChallenge.Instance.OnTryCatchFish -= ActivatePanel;
            Player.Instance.FishingRod.OnFishAssigned -= SetFishImage;
        }
        #endregion

        private void ActivatePanel(bool success)
        {
            if (success)
            {
                _canvasPanel.SetActive(true);
                ActionButton.Instance.gameObject.SetActive(false);
            }
        }

        private void SetFishImage()
        {
            var fish = Player.Instance.FishingRod.CaughtFish;

            if (fish != null)
            {
                ShowFishInformation();
                TryShowFishOverweightButton(fish);               
            }
        }

        private void TryShowFishOverweightButton(Fish fish)
        {
            var ship = Player.Instance.Ship;
            int weight = ship.CurrentWeight + fish.Weight;

            if (weight >= ship.CarryingCapacity)
            {
                _accept.gameObject.SetActive(false);
                _overweight.gameObject.SetActive(true);
            }
        }

        private void ShowFishInformation()
        {
            _fishCard.gameObject.SetActive(true);
            _fishCard.Initialize();
        }

        public void DoOnAccept()
        {
            _canvasPanel.SetActive(false);

            Player.Instance.FishingRod.TryPutFishInShip();
            FishingChallenge.Instance.Deactivate();
            ActionButton.Instance.gameObject.SetActive(true);
        }

        public void DoOnDecline()
        {
            if (_overweight.gameObject.activeSelf)
            {
                _overweight.gameObject.SetActive(false);
                _accept.gameObject.SetActive(true);
            }
            
            _canvasPanel.SetActive(false);

            Player.Instance.FishingRod.AssignFish(null);
            FishingChallenge.Instance.Deactivate();
            ActionButton.Instance.gameObject.SetActive(true);
        }
    }
}