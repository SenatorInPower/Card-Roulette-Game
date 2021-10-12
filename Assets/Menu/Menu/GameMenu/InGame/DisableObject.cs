using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableObject : MonoBehaviour
{
    private void Start()
    {
        gameObject.SetActive(false);
        transform.root.gameObject.SetActive(false);
    }
}
