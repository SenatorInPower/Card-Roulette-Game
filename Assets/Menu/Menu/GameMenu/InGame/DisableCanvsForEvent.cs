using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableCanvsForEvent : MonoBehaviour
{
    private void Start()
    {
        gameObject.GetComponent<Canvas>().worldCamera = Camera.main;
       
    }
}
