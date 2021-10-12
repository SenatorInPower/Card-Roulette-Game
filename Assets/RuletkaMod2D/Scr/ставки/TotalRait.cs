using Sirenix.OdinInspector;
using Sirenix.Serialization;
using System.Collections;
using TMPro;
using UnityEngine;

public class TotalRait : SerializedMonoBehaviour
{
    [OdinSerialize]
    public SharStart sharStart;

    private static int Rait_;


    public TextMeshProUGUI RaitText;
    float timerResetText = 3;
    public void ResetRait()
    {
        Rait_ = 0;
    }
    private void OnDisable()
    {
        ResetRait();
    }
    public void RaitInfoStart()
    {
        RaitText.text = $"All Rait 0";

    }
    public void ReturnMoney(int stawka)
    {
        Rait_ -= stawka;
        RaitText.text = $"All Rait {Rait_}";
        TotalMoney.TotalMoneys.Money += stawka;
        TotalMoney.TotalMoneys.Info();
    }
    public bool StawkaReal(int stawka)
    {


        timerResetText = 3;

        if (stawka > 100000)
        {
            RaitText.text = "Rait is over";
            AudioEvent.AudioEvents.AudioPlay(AudioAction.NoRait);

            return false;
        }
        else
        {
            if ((TotalMoney.TotalMoneys.Money - stawka) >= 0)
            {
                TotalMoney.TotalMoneys.Money -= stawka;
                TotalMoney.TotalMoneys.Info();
                Rait_ += stawka;
                RaitText.text = $"All Rait {Rait_}";
                sharStart.RaitSelect = true;
                AudioEvent.AudioEvents.AudioPlay(AudioAction.Bet);
                return true;
            }
            else
            {
                AudioEvent.AudioEvents.AudioPlay(AudioAction.NoRait);
                RaitText.text = "No money for rait";

                sharStart.RaitSelect = false;

                StartCoroutine(Waiter());
                return false;
            }
        }



    }
    IEnumerator Waiter()
    {

        while (true)
        {
            timerResetText -= Time.deltaTime;
            if (timerResetText < 0)
            {
                InvokeResetRait();
                break;
            }
            yield return null;
        }
    }
    void InvokeResetRait()
    {
        if (Rait_ > 0)
        {
            RaitText.text = $"All Rait {Rait_}";
        }
        else
        {
            RaitText.text = "All Rait 0";

        }
    }
}
