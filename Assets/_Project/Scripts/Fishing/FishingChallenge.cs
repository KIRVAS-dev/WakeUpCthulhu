using System;
using UnityEngine;
using UnityEngine.UI;

namespace CthulhuGame
{
    public sealed class FishingChallenge : SingletonBase<FishingChallenge>
    {
        [SerializeField] private Canvas _canvas;
        [SerializeField] private Image _fishCircleImage;
        [SerializeField] private Image _playerCircleImage;              

        private Vector3 _defaultPlayerScale;    

        private Color _defaultFishColor;
        private Color _defaultPlayerColor;

        private readonly float _minScale = 1f;
        private readonly float _maxScale = 6f;
        private readonly float _passScaleMin = 3f;
        private readonly float _passScaleMax = 4f;

        private readonly float _minSpeed = 1f;
        private readonly float _defaultSpeed = 6f; //4f

        private float _scale;
        private float _speed;

        private bool _isLooped;

        public event Action<bool> OnTryCatchFish;  
        public event Action OnEnable;
        public event Action OnDisable;

        #region UnityEvents
        private void Start()
        {
            SaveParametrs();

            _canvas.gameObject.SetActive(false);
        }

        private void Update()
        {
            if (ActionButton.Instance.Type == ActionButton.ActionType.CatchFish)
            {
                DoCircleAnimation();
            }
            else
            {
                Deactivate();
            }
        }
        #endregion

        private void SaveParametrs()
        {
            _defaultFishColor = _fishCircleImage.color;
            _defaultPlayerColor = _playerCircleImage.color;

            _defaultPlayerScale = _playerCircleImage.rectTransform.localScale;
            _scale = _minScale;

            _isLooped = false;
        }

        private void RestoreParametrs()
        {
            _fishCircleImage.color = _defaultFishColor;
            _playerCircleImage.color = _defaultPlayerColor;

            _playerCircleImage.rectTransform.localScale = _defaultPlayerScale;
            _scale = _minScale;

            _speed = _defaultSpeed;

            _isLooped = false;
        }
        private void DoCircleAnimation()
        {
            if (_isLooped)
            {
                _scale -= _speed * Time.deltaTime;

                if (_scale <= _minScale)
                {
                    _isLooped = false;
                }
            }
            else
            {
                _scale += _speed * Time.deltaTime;

                if (_scale >= _maxScale)
                {
                    _isLooped = true;
                }
            }

            _playerCircleImage.rectTransform.localScale = new Vector3(_scale, _scale, _scale);

            if (_scale >= _passScaleMin && _scale <= _passScaleMax)
            {
                _fishCircleImage.color = Color.cyan;
            }
            else
            {
                _fishCircleImage.color = _defaultFishColor;
            }
        }

        public void Activate()
        { 
            transform.position = Player.Instance.FishingRod.FishingPoint.gameObject.transform.position;

            _speed = _defaultSpeed - Player.Instance.FishingRod.Speed;

            if (_speed <= _minSpeed)
            {
                _speed = _minSpeed;
            }

            _canvas.gameObject.SetActive(true);           
            enabled = true;
            OnEnable?.Invoke();

            Player.Instance.TakeControlsFromPlayer();
        }

        public void Deactivate()
        {
            RestoreParametrs();
            _canvas.gameObject.SetActive(false);
            OnDisable?.Invoke();

            Player.Instance.GiveControlsToPlayer();
        }
        public void TryCatchFish()
        {
            enabled = false;

            if (_scale >= _passScaleMin && _scale <= _passScaleMax)
            {
                _fishCircleImage.color = Color.green;
                OnTryCatchFish?.Invoke(true);
            }
            else
            {
                _fishCircleImage.color = Color.red;
                OnTryCatchFish?.Invoke(false);
            }
        }
    }
}