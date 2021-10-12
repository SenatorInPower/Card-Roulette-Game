using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
namespace Card2
{
    public class MenuBehavior : MonoBehaviour
    {

        

       

        private static bool _playBackMusic = true;
        private static bool _init = false;

        void Start()
        {
           
        }

      
        public void StartGame()
        {
            SceneManager.LoadScene("Card2");
        }

        public void Quit()
        {
            SceneManager.LoadScene("Card2");
        }

      
   

        public void LoadMenu()
        {
            SceneManager.LoadScene("Menu");
        }

      
    }
}