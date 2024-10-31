using UnityEngine;

namespace CthulhuGame
{
    public class Spawner : MonoBehaviour
    {
        public enum SpawnMode
        {
            Start,
            Single,
            Loop
        }

        [SerializeField] private SpawnMode _spawnMode;
        [SerializeField] private GameObject _prefab;
        [SerializeField] private SphereArea _area;
        [SerializeField] private bool _entityIsTurnedToRandomAngle;
        [SerializeField] private float _respawnTime;
        [SerializeField] protected int _numSpawns;

        private GameObject _lastSpawnedEntity = null;
        private float _timer = 0f;

        protected virtual void Start()
        {
            if (_spawnMode == SpawnMode.Start)
            {
                SpawnEntity();
            }
        }

        private void Update()
        {
            switch (_spawnMode)
            {
                case SpawnMode.Single:

                    if (_lastSpawnedEntity == null)
                    {
                        SpawnEntity();
                    }
                    break;

                case SpawnMode.Loop:

                    _timer += Time.deltaTime;

                    if (_timer >= _respawnTime)
                    {
                        SpawnEntity();
                        _timer = 0f;
                    }
                    break;
            }
        }

        private bool AreaIsClean()
        {
            Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, _area.Radius);

            if (hits.Length > 0)
            {
                int maxAttempts = 100; // ћаксимальное количество попыток итераций. Ќужно дл€ ограничений расхода ресурсов.
                for (int i = 0; i < maxAttempts; i++)
                {
                    float x = Random.Range(-_area.Radius, _area.Radius);
                    float y = Random.Range(-_area.Radius, _area.Radius);
                    Vector3 spawnPosition = transform.position + new Vector3(x, y, 0);

                    bool isOverlapping = false;
                    foreach (Collider2D hit in hits)
                    {
                        if (hit.bounds.Contains(spawnPosition))
                        {
                            isOverlapping = true;
                            break;
                        }
                    }

                    if (!isOverlapping)
                    {
                        return true;
                    }
                }
                return false;
            }

            return true;
        }
        protected void SpawnEntity()
        {
            for (int i = 0; i < _numSpawns; i++)
            {
                if (AreaIsClean())
                {
                    var entity = Instantiate(_prefab);

                    entity.transform.position = _area.GetRandomInsideZone();

                    if (_entityIsTurnedToRandomAngle)
                    {
                        entity.transform.rotation = Quaternion.AngleAxis(Random.Range(0, 360), Vector3.up);
                    }
                    else
                    {
                        entity.transform.rotation = Quaternion.identity;
                    }

                    _lastSpawnedEntity = entity;
                }
            }
        }
    }
}