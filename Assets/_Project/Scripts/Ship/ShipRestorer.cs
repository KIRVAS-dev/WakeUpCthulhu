using UnityEngine;
using System;

namespace CthulhuGame
{
    public class ShipRestorer : MonoBehaviour
    {
        [SerializeField] private RestorePoint _restorePoint;
        public RestorePoint RestorePoint => _restorePoint;

        [Range(0, 1)]
        [SerializeField] private float _restoredHealthPercentage;

        public event Action OnShipRestored;

        public void RestoreShip()
        {
            var ship = Player.Instance.Ship;
            int health = Mathf.RoundToInt(ship.Health.MaxHealth * _restoredHealthPercentage);

            ship.Health.TryChangeHealthAmount(health);
            ship.FishContainer.ClearContainer();

            if (_restorePoint != null)
            {
                var position = _restorePoint.Transform.position;
                ship.gameObject.transform.position = position;
            }

            OnShipRestored?.Invoke();
        }
    }
}