using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
namespace Sol
{
    public class ChipsControl : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
    {
        public int StavkaChips;
        public ChipsMove Chips;
        static Camera camera;
        private void Awake()
        {
            camera = Camera.main;
        }
        public void OnDrag(PointerEventData eventData)
        {
            // Vector3 vect = new Vector3(Help, eventData.pointerCurrentRaycast.worldPosition.y, eventData.pointerCurrentRaycast.worldPosition.z);
            //Vector3 vect = new Vector3(Help, Input.mousePosition.y/Screen.height, Input.mousePosition.x/Screen.width);
            //coin.transform.position = vect;
            Vector3 cursorPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, gameObject.transform.position.z);
            Vector3 cursorPosition = camera.ScreenToWorldPoint(cursorPoint);
            Chips.transform.position = cursorPosition;
        }
        public void OnPointerDown(PointerEventData eventData)
        {
            Chips.stavka = StavkaChips;
            Chips.gameObject.SetActive(true);
        }

        public void OnPointerUp(PointerEventData eventData)
        {

            Chips.gameObject.SetActive(false);
        }


    }
}