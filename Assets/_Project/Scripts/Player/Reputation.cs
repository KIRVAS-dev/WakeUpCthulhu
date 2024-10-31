using CthulhuGame;
using UnityEngine;
using System;

public class Reputation : MonoBehaviour
{
    [SerializeField] private int _currentReputation;
    public int CurrentRep => _currentReputation;

    [SerializeField] private int _targetReputation;
    public int TargetRep => _targetReputation;

    public event Action OnReputationChanged;

    #region UnityEvents
    private void Start()
    {
        Player.Instance.Ship.Health.OnDeath += RestoreReputation;
    }

    private void OnDestroy()
    {
        Player.Instance.Ship.Health.OnDeath -= RestoreReputation;
    }
    #endregion;

    private void RestoreReputation()
    {
        _currentReputation = 0;
    }

    public void TryChangeReputationAmount(int amount)
    {
        if (amount != 0)
        {
            int currentReputation = _currentReputation + amount;

            if (currentReputation >= 0)
            {
                _currentReputation = currentReputation;

                OnReputationChanged?.Invoke();
            }
        }
    }
}