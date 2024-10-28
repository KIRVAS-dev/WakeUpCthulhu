using UnityEngine;

namespace CthulhuGame
{
    /// <summary>
    /// Скрипт создани¤ разных видов рыбы.
    /// </summary>
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