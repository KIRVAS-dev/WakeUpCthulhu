using UnityEngine;
using System;

namespace CthulhuGame
{
    public class Ship : MonoBehaviour
    {
        [SerializeField] private Rigidbody _rb;
        public Rigidbody Rb => _rb;

        [SerializeField] private int _carryingCapacity;
        public int CarryingCapacity => _carryingCapacity;

        [SerializeField] private int _currentWeight;
        public int CurrentWeight => _currentWeight;

        [SerializeField] private Health _health;
        public Health Health => _health;

        [SerializeField] private FishContainer _fishContainer;
        public FishContainer FishContainer => _fishContainer;

        [SerializeField] private AudioSource _engineSound;
        public AudioSource EngineSound => _engineSound;

        public event Action<bool> OnMarketNearby;
        public event Action<bool> OnWorkshopNearby;
        public event Action OnWeightChanged;

        #region UnityEvents
        private void Start()
        {
            _fishContainer.OnFishCaught += TryChangeWeightAmount;
        }

        private void OnDestroy()
        {
            _fishContainer.OnFishCaught -= TryChangeWeightAmount;
        }
        #endregion
        
        public void SendMarketMessage(bool value)
        {
            OnMarketNearby?.Invoke(value);
        }

        public void SendWorkshopMessage(bool value)
        {
            OnWorkshopNearby?.Invoke(value);
        }

        public void TryChangeWeightAmount(int amount)
        {
            if (amount != 0)
            {
                int currentWeight = _currentWeight + amount;

                if (currentWeight >= 0 || currentWeight <= _carryingCapacity)
                {
                    _currentWeight = currentWeight;

                    OnWeightChanged?.Invoke();
                }
            }
        }
    }
}