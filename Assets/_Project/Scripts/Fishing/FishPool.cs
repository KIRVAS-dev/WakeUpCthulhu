using UnityEngine;

namespace CthulhuGame
{
    public class FishPool : MonoBehaviour
    {
        [Header("Fish")]
        [SerializeField] private FishAsset[] _fishArray;
        public FishAsset[] FishArray => _fishArray;

        [Header("Artifact")]
        [SerializeField] private FishAsset[] _artifactArray;
        public FishAsset[] ArtifactArray => _artifactArray;
    }
}