using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orientation : MonoBehaviour
{
    
    WaitForSeconds waitTime = new WaitForSeconds(0.1f);
   
    IEnumerator Or()
    {
        while (true)
        {
            if (Screen.orientation != ScreenOrientation.Landscape)
            {
                Screen.orientation= ScreenOrientation.Landscape;
            }
          yield return waitTime;
        }
    }
    private void Awake()
    {
        StartCoroutine(Or());
    }
}
