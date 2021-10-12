using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DisableInLevel : MonoBehaviour
{
    public static ScenGame ScenGames;

    public ScenGame ScenGame;
    private void Awake()
    {
        SceneManager.sceneLoaded += scenLoad;
    }
    bool stopINEneble=true;
    private void OnEnable()
    {
        if (stopINEneble)
        {
            if (ScenGames != ScenGame)
            {
                gameObject.SetActive(false);
            }
            else
            {
                gameObject.SetActive(true);

            }
        }

    }
    private void scenLoad(Scene arg0, LoadSceneMode arg1)
    {
      
        if (ScenGames != ScenGame)
        {
            stopINEneble = false;
            gameObject.SetActive(false);
        }
        else
        {
            gameObject.SetActive(true);

        }
    }
}
