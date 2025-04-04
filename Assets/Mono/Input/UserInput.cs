using System;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Mono.Input
{
    public class UserInput : MonoBehaviour
    {
        [SerializeField] private TouchZone _touchZone;
        
        private Camera _camera;
        [CanBeNull] private PointerEventData _pointerEventData;
        
        public event Action<Vector2> Entered;
        public event Action<Vector2> Out;
        public event Action<Vector2> Holding;

        private void Awake()
        {
            _camera = Camera.main;
        }

        private void Update()
        {
            if(_pointerEventData == null)
                return;
            
            Holding?.Invoke(ConvertToWorld(_pointerEventData.position));
        }

        private void OnEnable()
        {
            _touchZone.PointerEntered += OnPointerEnter;
            _touchZone.PointerOut += OnPointerExit;
        }

        private void OnDisable()
        {
            _touchZone.PointerEntered -= OnPointerEnter;
            _touchZone.PointerOut -= OnPointerExit;
        }

        private void OnPointerExit(PointerEventData pointer)
        {
            Out?.Invoke(ConvertToWorld(pointer.position));
            _pointerEventData = null;
        }

        private void OnPointerEnter(PointerEventData pointer)
        {
            Entered?.Invoke(ConvertToWorld(pointer.position));
            _pointerEventData = pointer;
        }

        private Vector2 ConvertToWorld(Vector3 vector)
        {
            return _camera.ScreenToWorldPoint(vector);
        }
    }
}
