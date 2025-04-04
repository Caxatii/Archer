using Mono.Input;
using UnityEngine;

namespace Mono.Archer
{
    public class ArcherSpinner : MonoBehaviour
    {
        [SerializeField] private int _minAngle;
        [SerializeField] private int _maxAngle;
        
        [SerializeField] private UserInput _input;
        [SerializeField] private BoneEditor _editor;

        private Vector2 _startPoint;
        private readonly Vector2 _relative = Vector2.left;
        
        private void OnEnable()
        {
            _input.Holding += OnHolding;
            _input.Entered += OnEnter;
        }

        private void OnDisable()
        {
            _input.Holding -= OnHolding;
            _input.Entered -= OnEnter;
        }

        private void OnEnter(Vector2 point)
        {
            _startPoint = point;
        }

        private void OnHolding(Vector2 point)
        {
            Vector2 direction = -(_startPoint - point).normalized; 
            float angle = Vector2.SignedAngle(_relative, direction);
            float lerp = Mathf.InverseLerp(_minAngle, _maxAngle, angle);
            
            _editor.SetRotation(lerp);
        }
    }
}
