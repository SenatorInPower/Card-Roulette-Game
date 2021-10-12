using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class testSol : MonoBehaviour
{
    public GameObject parrent;
    [Button]

    void testComp()
    {
        if(parrent.GetComponentInChildren<TextMeshPro>())
        print(parrent.GetComponentInChildren<TextMeshPro>());
    }
}
