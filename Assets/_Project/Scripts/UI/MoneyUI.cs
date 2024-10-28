using UnityEngine;
using UnityEngine.UI;

namespace CthulhuGame
{
    /// <summary>
    /// Отображает деньги игрока в интерфейсе.
    /// </summary>
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
            Player.Instance.Money.OnMoneyChanged -= UpdateText;
        }
        #endregion

        private void UpdateText()
        {
            _text.text = Player.Instance.Money.CurrentMoney.ToString();
        }
    }
}