using TMPro;
using UnityEngine;

public class RaitLogic : MonoBehaviour
{
    public static bool RaitSelected;
    public static bool GameStart;
    public int MinRait = 20;
    static int MinPoint = 0;
    //public Button RaitAddInterecteble;
    //public Button RaitRemoveInterecteble;
    static TextMeshProUGUI BetText;
    static TextMeshProUGUI PointText;
    static TextMeshProUGUI MoneyText;   
    static int Rait;


    private void OnEnable()
    {
       
        BetText = GameObject.FindGameObjectWithTag("BetText").GetComponent<TextMeshProUGUI>();
        PointText = GameObject.FindGameObjectWithTag("PointText").GetComponent<TextMeshProUGUI>();
        MoneyText = GameObject.FindGameObjectWithTag("MoneyText").GetComponent<TextMeshProUGUI>();
        Rait = 0;
        RaitSelected = false;       
        GameStart = false;
        Info();
    }
    private void OnDisable()
    {

    }
    static void Resets()
    {
        GameStart = false;        
        RaitSelected = false;
        Rait = 0;
        Info();
       

    }
    public static void Info()
    {
        MoneyText.text = (PlayerPrefs.GetInt("Money") - Rait).ToString();
        BetText.text = Rait.ToString();
        PointText.text = MinPoint.ToString();

    }
    public void AddRait(int Rait_)
    {
        if (!GameStart)
        {
            if (PlayerPrefs.GetInt("Money") > Rait_ && Rait_ > MinRait)
            {
                Rait += Rait_;
                RaitSelected = true;
                AudioEvent.AudioEvents.AudioPlay(AudioAction.Bet);

            }
            else
            {
                AudioEvent.AudioEvents.AudioPlay(AudioAction.Lose);

            }
            Info();
        }
    }
    public void RemoveRait(int Remove)
    {
        if (!GameStart)
        {
            if (Rait > 1)
            {
                Rait -= Remove;
                RaitSelected = false;
                AudioEvent.AudioEvents.AudioPlay(AudioAction.NoRait);
            }
            else
            {
                AudioEvent.AudioEvents.AudioPlay(AudioAction.Lose);
            }
            Info();
        }
    }
    public static void WinAction(int Koef)
    {
        MinPoint += 20;
        PlayerPrefs.SetInt("Money", PlayerPrefs.GetInt("Money") + Rait * Koef);

        AudioEvent.AudioEvents.AudioPlay(AudioAction.Win);
        Resets();
    }
    public static void LoseAction(int Koef)
    {
        

        PlayerPrefs.SetInt("Money", PlayerPrefs.GetInt("Money") - Rait * Koef);

        AudioEvent.AudioEvents.AudioPlay(AudioAction.Lose);
        Resets();

    }

}
