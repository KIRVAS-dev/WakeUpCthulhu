using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace CthulhuGame
{
    public sealed class SphereArea : MonoBehaviour
    {
        [SerializeField] private float _radius;
        [SerializeField] private Color _color;

        public float Radius => _radius;

        public Vector3 GetRandomInsideZone()
        {
            Vector2 random = Random.insideUnitCircle * _radius;
            float shipY = Player.Instance.Ship.transform.position.y;

            return new Vector3(random.x, shipY, random.y) + transform.position;
        }

        public void TrySetRadius(float radius)
        {
            if (radius >= 0)
            {
                _radius = radius;
            }            
        }

#if UNITY_EDITOR
        private void OnDrawGizmosSelected()
        {
            Handles.color = _color;
            Handles.DrawSolidDisc(transform.position, transform.forward, _radius);
        }
#endif
    }
}