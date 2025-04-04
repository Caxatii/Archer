using Mono.Weapons.Projectile;
using UnityEngine;
using UnityEngine.Pool;

namespace Mono.Weapons.Spawners
{
    public class ArrowSpawner : MonoBehaviour
    {
        [field: SerializeField] public Transform SpawnPoint { get; private set; }
        
        [SerializeField] private Arrow _arrowPrefab;
    
        private ObjectPool<Arrow> _pool;

        private void Awake()
        {
            _pool = new ObjectPool<Arrow>(Create, OnGet, OnRelease);
        }

        public void Throw(float force)
        {
            var arrow = _pool.Get();
            arrow.Blasted += OnCollisionHit;
            arrow.Throw(force);
        }

        private void OnCollisionHit(Arrow arrow)
        {
            arrow.Blasted -= OnCollisionHit;
            _pool.Release(arrow);
        }

        private void OnRelease(Arrow arrow)
        {
            arrow.Reset();
            arrow.gameObject.SetActive(false);
        }

        private void OnGet(Arrow arrow)
        {
            arrow.gameObject.SetActive(true);
            arrow.transform.position = SpawnPoint.position;
            arrow.transform.rotation = SpawnPoint.rotation;
        }

        private Arrow Create()
        {
            return Instantiate(_arrowPrefab, transform);
        }
    }
}
