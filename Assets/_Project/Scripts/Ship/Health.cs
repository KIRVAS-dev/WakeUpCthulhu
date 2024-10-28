using UnityEngine;
using System;

namespace CthulhuGame
{
    public class Health : MonoBehaviour
    {
        [SerializeField] private int _maxHealth;
        public int MaxHealth => _maxHealth;

        [SerializeField] private int _currentHealth;
        public int CurrentHealth => _currentHealth;

        [Header("Collisions")]

        [SerializeField] private int _damageConstant;
        public int DamageConstant => _damageConstant;

        [SerializeField] private float _damageMultiplier;
        public float DamageMultiplier => _damageMultiplier;

        [SerializeField] private bool _isIndestructible;     

        public event Action OnHealthChanged;
        public event Action OnDeath;

        #region UnityEvents
        private void Start()
        {
            RestoreHealth(); // Заменить на загрузку сохраненного показателя здоровья.
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (!_isIndestructible)
            {
                float collisionSpeed = collision.relativeVelocity.magnitude;
                int damage = Mathf.RoundToInt(_damageConstant + collisionSpeed * _damageMultiplier);

                TryChangeHealthAmount(-Math.Abs(damage));
            }
        }
        #endregion

        public void TryChangeHealthAmount(int amount)
        {
            if (amount != 0)
            {
                _currentHealth = Mathf.Clamp(_currentHealth + amount, 0, _maxHealth);

                if (_currentHealth == 0)
                {
                    OnDeath?.Invoke();
                }
                else
                {
                    OnHealthChanged?.Invoke();
                }
            }
        }

        public void RestoreHealth()
        {
            _currentHealth = _maxHealth;

            OnHealthChanged?.Invoke();
        }

        public void SetMaxHealth(int health)
        {
            if (health > 0)
            {
                _maxHealth = health;
            }
        }
    }
}