using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Mono.Input
{
    public class TouchZone : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        public event Action<PointerEventData> PointerEntered;
        public event Action<PointerEventData> PointerOut;

        public void OnPointerDown(PointerEventData eventData)
        {
            PointerEntered?.Invoke(eventData);
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            PointerOut?.Invoke(eventData);
        }
    }
}