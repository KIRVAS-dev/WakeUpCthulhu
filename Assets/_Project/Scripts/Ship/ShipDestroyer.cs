using UnityEngine;

namespace CthulhuGame
{
    public class ShipSpriteEnabler : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer _sprite;

        #region UnityEvents
        private void Start()
        {
            Player.Instance.Ship.Health.OnDeath += DisableSprite;
            Player.Instance.ShipRestorer.OnShipRestored += EnableSprite;
        }

        private void OnDestroy()
        {
            Player.Instance.Ship.Health.OnDeath -= DisableSprite;
            Player.Instance.ShipRestorer.OnShipRestored -= EnableSprite;
        }
        #endregion

        private void DisableSprite()
        {
            _sprite.enabled = false;
        }

        private void EnableSprite()
        {
            _sprite.enabled = true;
        }
    }
}