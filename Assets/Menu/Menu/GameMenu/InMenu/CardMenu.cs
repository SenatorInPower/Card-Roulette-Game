using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Menu.Scr
{
    public class CardMenu : MonoBehaviour
    {

        public Material materialStart;
        public Material materialSelect;
        internal static SelectCard selectCard;
        private void Awake()
        {
            
            int childCount = transform.childCount;
            Transform[] obj = new Transform[transform.childCount];
            for (int i = 0; i < childCount; i++)
            {
                obj[i]= transform.GetChild(i);
                SelectCard select= obj[i].gameObject.AddComponent<SelectCard>();
                select.materialStart=materialStart;
                select.materialSelect = materialSelect;

            }    

                
        }


    }
}