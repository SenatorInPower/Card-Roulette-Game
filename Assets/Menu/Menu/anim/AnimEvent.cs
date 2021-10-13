using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AnimEvent : MonoBehaviour
{
    static string AnimFin;
    public Animator Animator;
    public bool AnimFaid;
    private void Awake()
    {
        SceneManager.sceneLoaded += LoadScenEvent;
    }
    private void OnEnable()
    {
        if (AnimFaid)
        {
            Animator.Play("CloseMenyInStart");
        }
    }
    private void LoadScenEvent(Scene arg0, LoadSceneMode arg1)
    {
        if (arg0.name == "Menu")
        {

        }
        else
        {
            
        }
    }
    public void PrintEvent(string s)
    {
        AnimFin = s;

    }
}


