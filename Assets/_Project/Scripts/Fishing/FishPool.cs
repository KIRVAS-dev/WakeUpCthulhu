using UnityEngine;

namespace CthulhuGame
{
    public class FishPool : MonoBehaviour
    {
        [Header("Garbage fish")]
        [SerializeField] private FishAsset[] _garbageArray;
        public FishAsset[] GarbageArray => _garbageArray;

        private FishAsset[] _currentArray;
        public FishAsset[] CurrentArray => _currentArray;
    }
}