using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class tultipForTutorial : MonoBehaviour, IPointerEnterHandler,IPointerExitHandler
{
    public GameObject root;
    public TextMeshProUGUI text;
    public string textTultip;
    Camera cam;
    private void Awake()
    {
        cam = Camera.main;
        SceneManager.sceneLoaded += OnLoad;
    }

    private void OnLoad(Scene arg0, LoadSceneMode arg1)
    {
        if (cam == null)
        cam = Camera.main;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Vector3 pos = cam.ScreenToWorldPoint(eventData.position);
        pos.z = root.transform.position.z;
        root.transform.position = pos;
        root.SetActive(true);
        text.text = textTultip;
        
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        root.SetActive(true);
       
    }
}
