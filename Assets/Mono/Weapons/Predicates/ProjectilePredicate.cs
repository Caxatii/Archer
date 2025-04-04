using System.Collections.Generic;
using UnityEngine;

namespace Mono.Weapons.Predicates
{
    public class ProjectilePredicate : MonoBehaviour
    {
        [SerializeField] private bool _useRaycast;
        [SerializeField] private int _count;
        [SerializeField] private float _scaleFactor;
        
        [SerializeField] private GameObject _projectilePrefab;

        private readonly List<GameObject> _projectiles = new();
        
        private PredicateBehaviour _predicate;
        
        private void Awake()
        {
            Spawn();
            _predicate ??= _useRaycast ? 
                new RaycastPredicateBehaviour(_projectiles) : 
                new SimplePredicateBehaviour(_projectiles);
            
            Hide();
        }
        
        public void Predict(Vector2 startPoint, Vector2 direction, float gravityScale)
        {
            _predicate.Predict(startPoint, direction, gravityScale);
        }

        public void Hide()
        {
            _predicate.Hide();
        }
        
        private void Spawn()
        {
            for (int i = 0; i < _count; i++)
            {
                GameObject item = Instantiate(_projectilePrefab, transform);
                item.transform.localScale *= Mathf.Pow(_scaleFactor, i);
                _projectiles.Add(item);
            }
        }
    }
}
