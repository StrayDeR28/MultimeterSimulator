using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Assets.Scrpits.Multimeter
{
    public class MultimeterModeSwitcher : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        public bool IsPointerOnModeSwitcher { get; private set; }
        
        public void OnPointerEnter(PointerEventData eventData)
        {
            IsPointerOnModeSwitcher = true;
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            IsPointerOnModeSwitcher = false;
        }
    }
}