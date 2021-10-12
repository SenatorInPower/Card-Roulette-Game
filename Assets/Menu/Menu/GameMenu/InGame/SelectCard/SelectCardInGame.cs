using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class SelectCardInGame : MonoBehaviour,IPointerClickHandler
{
    public NameScen nameScen;

    public void OnPointerClick(PointerEventData eventData)
    {
        SceneManager.LoadScene(nameScen.scenName);
        DisableInLevel.ScenGames = nameScen.ScenGame;
    }
    // Start is called before the first frame update

}
