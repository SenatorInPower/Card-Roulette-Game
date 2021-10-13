using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class OpenMenu : MonoBehaviour,IPointerDownHandler
{
    public Animator animator;
    bool open=true;
    public bool AnimFaid;
    private void OnEnable()
    {
        if (AnimFaid)
        {
            animator.Play("CloseMenyInStart");
            open = true;
        }
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        if (open)
        {
           
            animator.Play("Start");
        }
        else
        {
            

            animator.Play("End");

        }
        AudioEvent.AudioEvents.AudioPlay(AudioAction.ClickToUI);
        open = !open;
    }
   

}
