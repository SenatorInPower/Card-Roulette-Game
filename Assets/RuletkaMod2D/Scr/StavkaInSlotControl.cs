using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StavkaInSlotControl : MonoBehaviour
{
    

    public TextMeshPro stavkaText;
    private int stavka_;
    internal int stavka
    {
        get { return stavka_; }
        set
        {
            stavka_ = value;
            stavkaText.text = value.ToString();
        }
    }
    //internal int StawkaRestory()
    //{

    //    return stavka_;
    //}
    internal void AddStavka(int stavka)
    {
        this.stavka += stavka;
     //   AllSlotControlStawka.Stavka += stavka;
        
    }
   
}
