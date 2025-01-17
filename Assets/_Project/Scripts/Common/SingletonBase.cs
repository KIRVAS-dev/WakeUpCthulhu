using UnityEngine;

namespace CthulhuGame
{
    [DisallowMultipleComponent]
    public abstract class SingletonBase<T> : MonoBehaviour where T : MonoBehaviour
    {
        [Header("Singleton")]
        [SerializeField] private bool _doNotDestroyOnLoad;

        public static T Instance { get; private set; }

        protected virtual void Awake()
        {
            if (Instance != null)
            {
                Destroy(this);
                return;
            }

            Instance = this as T;

            if (_doNotDestroyOnLoad)
                DontDestroyOnLoad(gameObject);
        }
    }
}