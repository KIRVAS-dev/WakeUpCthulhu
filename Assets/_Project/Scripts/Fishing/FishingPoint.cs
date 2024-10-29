using UnityEngine;
using System.Collections.Generic;
using System;

namespace CthulhuGame
{
    public class FishingPoint : MonoBehaviour
    {
        private static HashSet<FishingPoint> _allFishingPoints;
        public static IReadOnlyCollection<FishingPoint> AllFishingPoints => _allFishingPoints;

        public static event Action OnFishPointDestroy;

        [SerializeField] private Fish _fishPrefab;
        [SerializeField] private FishPool _fishPoolPrefab;
        [SerializeField] private SpriteRenderer _circleOfFish;
        [SerializeField] private Sprite[] _cirleSprites;
        [SerializeField] private Rotator _rotation;
        
        private Fish _fish;
        private bool _isActive = false; 

        #region UnityEvents      
        private void Start()
        {
            _circleOfFish.enabled = true;
            _rotation.enabled = true;

            if (_cirleSprites.Length > 0)
            {
                int index = UnityEngine.Random.Range(0, _cirleSprites.Length);
                _circleOfFish.sprite = _cirleSprites[index];    
            }

            if (_allFishingPoints == null)
            {
                _allFishingPoints = new HashSet<FishingPoint>();
            }

            _allFishingPoints.Add(this);          
            
            FishingChallenge.Instance.OnTryCatchFish += ShowCatchedFish;
        }

        private void OnDestroy()
        {
            FishingChallenge.Instance.OnTryCatchFish -= ShowCatchedFish;       
        }
        #endregion

        private void ShowCatchedFish(bool success)
        {
            if (_isActive)
            {
                if (success)
                {
                    _fish = Instantiate(_fishPrefab, transform.position, Quaternion.identity);
                    _fish.Sprite.enabled = false; // Attention!
                    
                    if (DropProbability.Value <= 10) // Шанс поймать артефакт - 10%.
                    {
                        var garbage = _fishPoolPrefab.ArtifactArray;
                        if (garbage.Length > 0)
                        {
                            int index = UnityEngine.Random.Range(0, garbage.Length);
                            _fish.Initialize(garbage[index]);
                        }
                    }
                    else
                    {
                        var fish = _fishPoolPrefab.FishArray;
                        if (fish.Length > 0)
                        {
                            int index = UnityEngine.Random.Range(0, fish.Length);
                            _fish.Initialize(fish[index]);
                        }
                    }

                    Player.Instance.FishingRod.AssignFish(_fish);
                }
                else
                {
                    Player.Instance.FishingRod.AssignFish(null);
                }

                FishingChallenge.Instance.OnDisable += DestroyItself;
            }          
        }

        private void DestroyItself()
        {
            FishingChallenge.Instance.OnDisable -= DestroyItself;

            if (_fish != null)
            {
                Destroy(_fish.gameObject);
            }

            _allFishingPoints.Remove(this);

            Player.Instance.FishingRod.ResetActiveFishingPoint();

            OnFishPointDestroy?.Invoke();

            Destroy(gameObject);          
        }

        public void SetActive(bool value)
        {
            _isActive = value;
        }
    }
}