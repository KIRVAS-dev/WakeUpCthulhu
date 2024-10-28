using UnityEngine;
using System;

namespace CthulhuGame
{
    /// <summary>
    /// Рынок, на котором игрок может продать рыбу и купить апгрейды.
    /// </summary>
    public class Market : MonoBehaviour
    {
        private Collider2D _player;

        #region UnityEvents
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.CompareTag("Player")) // Attention!
            {
                _player = collision;

                Player.Instance.Ship.SendMarketMessage(true);
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision == _player) // Attention!
            {
                Player.Instance.Ship.SendMarketMessage(false);

                _player = null;
            }
        }
        #endregion

        public static void SellFish()
        {
            int weight = Player.Instance.Ship.FishContainer.Weight;
            int money = Player.Instance.Ship.FishContainer.Cost;

            Player.Instance.Ship.TryChangeWeightAmount(-Math.Abs(weight));
            Player.Instance.Money.TryChangeMoneyAmount(money);
            Player.Instance.Ship.FishContainer.ClearContainer();
        }
    }
}