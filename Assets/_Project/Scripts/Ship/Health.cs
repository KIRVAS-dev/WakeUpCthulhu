using UnityEngine;
using System;
using static UnityEditor.Experimental.GraphView.GraphView;

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

        [SerializeField] private AudioSource _audioSource;

        public event Action OnHealthChanged;
        public event Action OnDeath;

        #region UnityEvents
        private void Start()
        {
            if (_currentHealth <= 0)
            {
                RestoreHealth();
            }
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (!_isIndestructible)
            {
                float collisionSpeed = collision.relativeVelocity.magnitude;
                int damage = Mathf.RoundToInt(_damageConstant + collisionSpeed * _damageMultiplier);

                TryChangeHealthAmount(-Math.Abs(damage));

                if (collision.gameObject.CompareTag("Garbage")) // Attention!
                {
                    Destroy(collision.transform.parent.gameObject);   

                    if (_audioSource)
                    {
                        _audioSource.Play();
                    }
                }
            }
        }
        #endregion

        public void TryChangeHealthAmount(int amount)
        {
            if (amount != 0)
            {
                _currentHealth = Mathf.Clamp(_currentHealth + amount, 0, _maxHealth);

                OnHealthChanged?.Invoke();

                if (_currentHealth == 0)
                {
                    OnDeath?.Invoke();
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