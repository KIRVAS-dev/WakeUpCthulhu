using System;
using UnityEngine;

public class CthulhuTimer : MonoBehaviour
{
    [SerializeField] private float _targetTime;
    public float TargetTime => _targetTime;

    private float _currentTime;
    public float currentTime => _currentTime;

    private float _eventTime = 3f;
    private float timer;

    private bool _isRunning;

    public event Action OnSecondsPassed;
    public event Action OnCthulhuTimerPassed;

    private void Update()
    {
        if (_isRunning)
        {
            if (_currentTime < _targetTime)
            {
                _currentTime += Time.deltaTime;

                DoTimerCall();
            }
            else
            {
                _isRunning = false;
                OnCthulhuTimerPassed?.Invoke();
            }
        } 
    }

    private void DoTimerCall()
    {
        timer += Time.deltaTime;

        if (timer >= _eventTime)
        {
            OnSecondsPassed?.Invoke();
            timer = 0;
        }     
    }

    public void ResetTime()
    {
        _currentTime = 0;
        _isRunning = true;
    }
}
