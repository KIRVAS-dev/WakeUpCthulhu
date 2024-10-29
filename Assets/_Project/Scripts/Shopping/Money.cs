using UnityEngine;
using System;

namespace CthulhuGame
{
    public class Money : MonoBehaviour
    {
        [SerializeField] private int _currentMoney;
        public int CurrentMoney => _currentMoney;

        public event Action OnMoneyChanged;

        #region UnityEvents
        private void Start()
        {
            Player.Instance.Ship.Health.OnDeath += RestoreMoney;
        }

        private void OnDestroy()
        {
            Player.Instance.Ship.Health.OnDeath -= RestoreMoney;
        }
        #endregion;

        private void RestoreMoney()
        {
            _currentMoney = 0;
        }

        public void TryChangeMoneyAmount(int amount)
        {            
            if (amount != 0)
            {
                int currentMoney = _currentMoney + amount;

                if (currentMoney >= 0)
                {
                    _currentMoney = currentMoney;

                    OnMoneyChanged?.Invoke();
                }
            }
        } 
    }
}