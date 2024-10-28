using UnityEngine;
using UnityEngine.UI;

namespace CthulhuGame
{
    public class MoneyUI : MonoBehaviour
    {
        [SerializeField] private Text _text;

        #region UnityEvents
        private void Start()
        {
            UpdateText();
            
            Player.Instance.Money.OnMoneyChanged += UpdateText;
        }

        private void OnDestroy()
        {
            UpdateText();
            
            Player.Instance.Money.OnMoneyChanged -= UpdateText;
        }
        #endregion

        private void UpdateText()
        {
            _text.text = Player.Instance.Money.CurrentMoney.ToString();
        }
    }
}