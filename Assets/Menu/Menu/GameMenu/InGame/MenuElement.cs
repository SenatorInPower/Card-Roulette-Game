using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

public class MenuElement : MonoBehaviour, IPointerDownHandler
{
    public bool AnimFaid;


    Animator animator;
  
    static MenuElement element;
    private void Awake()
    {
        animator=gameObject.GetComponent<Animator>();
    }
    private void OnEnable()
    {
        if (AnimFaid)
        {
        
            animator.Play("CloseInStart");
            element = null;
        }
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        if (element != this)
        {
            if(element!=null)
            element.animator.Play("Close");

            AudioEvent.AudioEvents.AudioPlay(AudioAction.ClickToCard);

            animator.Play("Open");
            element = this;
        }
        else
        {
            
            AudioEvent.AudioEvents.AudioPlay(AudioAction.ClickToCard);

            animator.Play("Close");
            element = null;
        }
        
    }

}
