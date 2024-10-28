using UnityEngine;

namespace CthulhuGame
{
    public class RestorePoint : MonoBehaviour
    {
        [SerializeField] private Transform _transform;
        public Transform Transform => _transform;

        public void SetTransform(Transform transform)
        {
            _transform = transform;
        }
    }
}