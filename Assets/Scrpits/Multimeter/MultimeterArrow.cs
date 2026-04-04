using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Assets.Scrpits.Multimeter
{
    public class MultimeterArrow : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        public event Action PointerEnterArrow;
        public event Action PointerExitArrow;
        
        public void OnPointerEnter(PointerEventData eventData)
        {
            PointerEnterArrow?.Invoke();
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            PointerExitArrow?.Invoke();
        }
    }
}