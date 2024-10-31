using CthulhuGame;
using System;
using UnityEngine;

public class GarbageHealth : MonoBehaviour
{
    [SerializeField] private int _maxHealth;
    public int MaxHealth => _maxHealth;

    [SerializeField] private int _currentHealth;
    public int CurrentHealth => _currentHealth;

    [SerializeField] private int damage;

    [SerializeField] private bool _isIndestructible;

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
            if (collision.gameObject.CompareTag("Player")) // Attention!
            {
                TryChangeHealthAmount(-Math.Abs(damage));
            }
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
                Destroy(gameObject);
            }
        }
    }

    public void RestoreHealth()
    {
        _currentHealth = _maxHealth;
    }

    public void SetMaxHealth(int health)
    {
        if (health > 0)
        {
            _maxHealth = health;
        }
    }
}
