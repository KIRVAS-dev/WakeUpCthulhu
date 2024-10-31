using UnityEngine;
using System;

namespace CthulhuGame
{
    public class Market : MonoBehaviour
    {
        private Collider _player;

        #region UnityEvents
        private void OnTriggerEnter(Collider collision)
        {
            if (collision.gameObject.CompareTag("Player")) // Attention!
            {
                _player = collision;

                Player.Instance.Ship.SendMarketMessage(true);
            }
        }

        private void OnTriggerExit(Collider collision)
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
            Player.Instance.Reputation.TryChangeReputationAmount(money);
        }
    }
}