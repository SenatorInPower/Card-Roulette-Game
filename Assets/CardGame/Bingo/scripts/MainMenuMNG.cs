using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
namespace Bingo
{
    public class MainMenuMNG : MonoBehaviour
    {

        public void PlayGame()
        {
            SceneManager.LoadScene(1);
        }

        public void CloseGame()
        {
            Application.Quit();
        }
    }
}