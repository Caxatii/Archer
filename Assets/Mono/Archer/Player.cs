using Mono.Input;
using Mono.Weapons.Predicates;
using Mono.Weapons.Spawners;
using UnityEngine;

namespace Mono.Archer
{
    [RequireComponent(typeof(ArrowSpawner))]
    public class Player : MonoBehaviour
    {
        [SerializeField] private float _sensitivity;
        [SerializeField] private float _maxThrowPower;
        
        [SerializeField] private UserInput _userInput;
        [SerializeField] private ArcherAnimator _animator;
        [SerializeField] private ProjectilePredicate _predicate;
        
        private Vector2 _startTouchPosition;
        
        private ArrowSpawner _arrowSpawner;
       
        private void Awake()
        {
            _arrowSpawner = GetComponent<ArrowSpawner>();
        }

        private void OnEnable()
        {
            _userInput.Holding += OnHolding;
            _userInput.Entered += OnEnter;
            _userInput.Out += OnExit;
        }

        private void OnDisable()
        {
            _userInput.Holding -= OnHolding;
            _userInput.Entered -= OnEnter;
            _userInput.Out -= OnExit;
        }

        private void OnHolding(Vector2 point)
        {
            _predicate.Predict(_arrowSpawner.SpawnPoint.position,
                _arrowSpawner.SpawnPoint.right *
                CalculateThrowDirection(point)
                    .magnitude);
        }

        private void OnExit(Vector2 point)
        {
            _animator.PlayThrow();
            _arrowSpawner.Throw(CalculateThrowDirection(point).magnitude);
            _predicate.Hide();
        }

        private void OnEnter(Vector2 point)
        {
            _startTouchPosition = point;
        }

        private Vector2 CalculateThrowDirection(Vector2 point)
        {
            return Vector2.ClampMagnitude(
                (_startTouchPosition - point) * _sensitivity, 
                _maxThrowPower
                );
        }
    }
}