using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DontDestroyCanvas : MonoBehaviour
{
     static DontDestroyCanvas rootCnavas;
     Canvas canvas;
    private void Awake()
    {
        if(rootCnavas != null)
        {
            Destroy(gameObject);

        }
        else
        {
            rootCnavas = this;
            SceneManager.sceneLoaded += LoadLevel;
            DontDestroyOnLoad(gameObject);
        }
        
   

       

    }
    static bool first;
    private void LoadLevel(Scene arg0, LoadSceneMode arg1)
    {
        if (arg0.name == "Menu")
        {
            if (first)
            {
                  gameObject.SetActive(false);
            }
            else
            {
                first = true;
            }
         
        }
        else
        {
            gameObject.SetActive(true);

        }

        canvas = GetComponent<Canvas>();
        canvas.worldCamera = Camera.main;

       
    }
   
}
