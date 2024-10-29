using UnityEngine;
using System;

namespace CthulhuGame
{
    public class FishContainer : MonoBehaviour
    {
        [SerializeField] private int _totalFishWeight;
        public int Weight => _totalFishWeight;

        [SerializeField] private int _totalFishCost;
        public int Cost => _totalFishCost;
      
        public event Action<int> OnFishCaught;

        private void Start()
        {
            Player.Instance.Ship.Health.OnDeath += ClearContainer;
        }

        private void OnDestroy()
        {
            Player.Instance.Ship.Health.OnDeath -= ClearContainer;
        }

        public void ChangeWeightAmount(int amount)
        {
            _totalFishWeight += amount;

            if (_totalFishWeight < 0)
            {
                _totalFishWeight = 0;
            }

            OnFishCaught(amount);
        }

        public void ChangeCostAmount(int amount)
        {
            _totalFishCost += amount;

            if ( _totalFishCost < 0)
            {
                _totalFishCost = 0;
            }
        }

        public void ClearContainer()
        {
            _totalFishWeight = 0;
            _totalFishCost = 0;
        }
    }
}