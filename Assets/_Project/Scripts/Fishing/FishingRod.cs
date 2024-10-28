using System;
using UnityEngine;
using System.Collections.Generic;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace CthulhuGame
{
    public class FishingRod : MonoBehaviour
    {
        [SerializeField] private SphereCollider _fishingRodCollider;

        [SerializeField] private float _speed;
        public float Speed => _speed;

        private FishingPoint _activeFishingPoint;
        public FishingPoint FishingPoint => _activeFishingPoint;

        private Fish _сaughtFish;
        public Fish CaughtFish => _сaughtFish;

        private List<FishingPoint> _fishingPoints;
        private Collider _fishingPointCollider;

        private float _radius;


        private bool _isTriggered = false;

        public event Action<bool> OnFishingPlaceNearby;

        public event Action OnFishAssigned;

        #region UnityEvents
        private void Start()
        {         
            _fishingPoints = new List<FishingPoint>();
            _radius = _fishingRodCollider.radius;
        }

        private void OnTriggerEnter(Collider collision)
        {
            if (collision.TryGetComponent<FishingPoint>(out var fishingPoint)) 
            {
                _fishingPointCollider = collision;

                if (!_isTriggered) // Защита от срабатывания нескольких FishingPoint при попадании в триггер.
                {
                    _isTriggered = true;
                    _activeFishingPoint = fishingPoint;
                    _activeFishingPoint.SetActive(true);

                    OnFishingPlaceNearby?.Invoke(true);
                }
            }
        }

        private void OnTriggerExit(Collider collision) 
        {
            if (collision == _fishingPointCollider)
            {
                if (_isTriggered)
                {
                    _isTriggered = false;
                    _activeFishingPoint.SetActive(false);
                    _activeFishingPoint = null;

                    OnFishingPlaceNearby?.Invoke(false);
                }
            }
        }

#if UNITY_EDITOR      
        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(transform.position, _radius);
        }
#endif
        #endregion

        public void FindFishNearby()
        {
            Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, _radius);

            if (hits.Length > 0)
            {
                _fishingPoints.Clear();

                foreach (var hit in hits)
                {
                    if (hit.gameObject.TryGetComponent<FishingPoint>(out var point))
                    {
                        _fishingPoints.Add(point);
                    }
                }

                FishingPoint closestFishingPoint = null;
                float minDistance = Mathf.Infinity;

                foreach (var fishingPoint in _fishingPoints)
                {
                    float distance = Vector2.Distance(transform.position, fishingPoint.transform.position);

                    if (distance < minDistance)
                    {
                        minDistance = distance;
                        closestFishingPoint = fishingPoint;
                    }
                }

                if (closestFishingPoint != null)
                {
                    if (!_isTriggered)
                    {
                        _isTriggered = true;
                        _activeFishingPoint = closestFishingPoint;
                        _activeFishingPoint.SetActive(true);

                        OnFishingPlaceNearby?.Invoke(true);
                    }
                }
            }
        }

        public void AssignFish(Fish fish)
        {
            _сaughtFish = fish;

            OnFishAssigned?.Invoke();
        }

        public void TryPutFishInShip()
        {
            if (_сaughtFish != null)
            {
                Player.Instance.Ship.FishContainer.ChangeWeightAmount(_сaughtFish.Weight);
                Player.Instance.Ship.FishContainer.ChangeCostAmount(_сaughtFish.Cost);
            }
        }
    }
}