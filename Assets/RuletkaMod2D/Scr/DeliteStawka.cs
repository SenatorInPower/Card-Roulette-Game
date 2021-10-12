using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.EventSystems;
using System;

public class DeliteStawka : MonoBehaviour, IPointerDownHandler
{
    public static Action<SlotType, StavkaInSlotControl> deliteStawka;
    public void OnPointerDown(PointerEventData eventData)
    {
        AudioEvent.AudioEvents.AudioPlay(AudioAction.ClickToUI);

        SlotType localChips = gameObject.GetComponent<SlotType>();
        StavkaInSlotControl StavkaInSlotControl = gameObject.GetComponent<StavkaInSlotControl>();
        if (localChips != null&& StavkaInSlotControl!=null)
        {
            
            deliteStawka.Invoke(localChips, StavkaInSlotControl);
        }
          

        
    }
    

}
