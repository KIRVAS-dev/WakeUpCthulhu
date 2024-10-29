using UnityEngine;

namespace CthulhuGame
{
    [CreateAssetMenu]
    public class FishAsset : ScriptableObject
    {
        public Sprite Sprite;
        public string Name;
        public string Description;
        public int Cost;
        public int Weight;
    }
}