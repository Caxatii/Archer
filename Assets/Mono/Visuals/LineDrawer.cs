using Mono.Input;
using UnityEngine;

namespace Mono.Visuals
{
    [RequireComponent(typeof(LineRenderer))]
    public class LineDrawer : MonoBehaviour
    {
        [SerializeField] private UserInput _userInput;
        
        private LineRenderer _lineRenderer;

        private void Awake()
        {
            _lineRenderer = GetComponent<LineRenderer>();
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
            _lineRenderer.SetPosition(1, point);
        }

        private void OnExit(Vector2 point)
        {
            _lineRenderer.positionCount = 0;
        }

        private void OnEnter(Vector2 point)
        {
            _lineRenderer.positionCount = 2;
            _lineRenderer.SetPosition(0, point);
        }
        
    }
}
