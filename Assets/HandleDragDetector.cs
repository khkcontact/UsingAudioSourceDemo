using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace SoundPlayerDemo
{
    public class HandleDragDetector : MonoBehaviour, 
        IPointerDownHandler,IPointerUpHandler
       // IBeginDragHandler, IDragHandler, IEndDragHandler

    {
        [Tooltip("visible to show state at Inspector")]
        [SerializeField]
        bool _isDragging;
        public bool IsDragging
        {
            get { return _isDragging; }
            set 
            {  
                _isDragging = value;
                OnPointerStateChangedEvent.Invoke(_isDragging);
            }
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            _isDragging = true;
            Debug.Log("OnPointerDown");
        }

        event System.Action<bool> OnPointerStateChangedEvent = 
            delegate { Debug.Log("OnPointerStateChange"); };
        public void Subscribe(System.Action<bool> onPointerStateChangedHandler)
        {
            OnPointerStateChangedEvent += onPointerStateChangedHandler;
        }
        public void UnSubscribe(System.Action<bool> onPointerStateChangedHandler)
        {
            OnPointerStateChangedEvent -= onPointerStateChangedHandler;
        }
        public void OnPointerUp(PointerEventData eventData)
        {
            _isDragging = false;
            Debug.Log("OnPointerUp");
        }


    }
}
