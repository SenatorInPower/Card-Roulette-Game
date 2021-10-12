using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TotalMoney : MonoBehaviour
{
    public static TotalMoney TotalMoneys;
    [SerializeField]
    private int StatMoneyPlayer;
    [SerializeField]
    private TextMeshProUGUI TextMoney;

    private static int money_;
    private void Awake()
    {
        if (TotalMoneys == null)
        {
            TotalMoneys = this;
            
        }
        else
        {
            Destroy(gameObject);
        }
        SceneManager.sceneLoaded += LoadScen;
       // if (SceneManager.GetActiveScene().name == "Menu") ;
       //gameObject.SetActive(true);
    }

    private void LoadScen(Scene arg0, LoadSceneMode arg1)
    {
        if( (arg0.name== "Ruletka"&& arg0.name != "Menu")|| (arg0.name != "Ruletka" && arg0.name == "Menu"))
        {
            if(TextMoney)
            TextMoney.gameObject.SetActive(true);
        }
        else
        {
            if (TextMoney)
                TextMoney.gameObject.SetActive(false);

        }
    }

    public int Money
    {

        get
        {
            
            return Mathf.Clamp(money_, 0, 10000000);


        }
        set => money_ = value;
    }
    public void Info()
    {
        TextMoney.text = $"All money {money_}";
        PlayerPrefs.SetInt("Money", money_);
    }

    public TextMeshProUGUI ReturnMoneyText()
    {
        return TextMoney;
    }
    private void Start()
    {
       
        if (!PlayerPrefs.HasKey("First"))
        {
            PlayerPrefs.SetInt("First", 1);
            PlayerPrefs.SetInt("Money", StatMoneyPlayer);
            money_ = StatMoneyPlayer;
        }
        else
        {
            money_ = PlayerPrefs.GetInt("Money");
        }
        Info();
    }
    

}
