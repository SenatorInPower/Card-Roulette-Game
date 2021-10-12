using Sirenix.OdinInspector;
using Sirenix.Serialization;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;


public class ChipsMove : SerializedMonoBehaviour
{
  
    public TotalRait RaitControl;
    [OdinSerialize]
    public IStawka stawka;

    [OdinSerialize]
    public IStawkaInSlot StawkaInSlot;

    public GameObject CriateChipsInSlot;
    public TextMeshPro stavkaText;
    private int stavka_;
  
    internal int stavka
    {
        get { return stavka_; }
        set {
            stavka_ = value;
            stavkaText.text = value.ToString(); } }

    private GameObject colliderContact;


    
    private void OnDisable()
    {
        if (RaitControl.StawkaReal(stavka))
        {



            if (colliderContact != null)
            {
                StavkaInSlotControl stavkaInSlot = colliderContact.GetComponent<StavkaInSlotControl>();

                    if (stavkaInSlot)
                    {
                    stavkaInSlot.AddStavka(stavka_);

                    SlotType slot = colliderContact.GetComponent<SlotType>();




                    if (slot.typeStawka == TypeStawka.Slot || slot.typeStawka == TypeStawka.Zero)
                    {
                        InitStawkaInSlot(colliderContact.name, stavka_);
                    }
                    else
                    {
                        InitNoSlotCoef(slot.typeStawka, slot.Coeff, stavka_);
                    }

                }
                else
                {


                    stavkaInSlot = colliderContact.AddComponent<StavkaInSlotControl>();



                    GameObject instChipsText = Instantiate(CriateChipsInSlot, colliderContact.transform.position, Quaternion.identity);


                    stavkaInSlot.stavkaText = instChipsText.GetComponentInChildren<TextMeshPro>();

                    stavkaInSlot.AddStavka(stavka_);

                    SlotType slot = colliderContact.GetComponent<SlotType>();



                    slot.localChips = instChipsText;

                    if (slot.typeStawka == TypeStawka.Slot || slot.typeStawka == TypeStawka.Zero)
                    {
                        InitStawkaInSlot(colliderContact.name, stavka_);
                    }
                    else
                    {
                        InitNoSlotCoef(slot.typeStawka, slot.Coeff, stavka_);
                    }

                }

            }
        }
    }

    void InitStawkaInSlot(string slotID,int stawka)
    {
        if (!this.StawkaInSlot.typeStavkasDictSlot.ContainsKey(slotID))
        {
            this.StawkaInSlot.typeStavkasDictSlot.Add(slotID, stawka);

        }
        else
        { 
            this.StawkaInSlot.typeStavkasDictSlot[slotID]+= stawka;
           
        }
    }
    

    
    void InitNoSlotCoef(TypeStawka stawka,int coeff, int stawkaCount)
    {


        if (!this.stawka.typeStavkasDictNoSlot.ContainsKey(stawka))
        {
            this.stawka.typeStavkasDictNoSlot.Add(stawka, new StawkaTypeCriation(stawka, 1, coeff, stawkaCount));
       
        }
        else
        {
            this.stawka.typeStavkasDictNoSlot[stawka].countElement++;
            this.stawka.typeStavkasDictNoSlot[stawka].Stawka += stawkaCount;
        }
    }
    void TypeStawkaGen(TypeStawka stawka, int coeff, int stawkaCount)
    {
        InitNoSlotCoef( stawka, coeff,  stawkaCount);
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        colliderContact = collision.gameObject;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (colliderContact != null)
        {
            if (collision.tag != "Sector")
            {
                colliderContact = null;
            }
           else if (colliderContact == collision)
            {
                colliderContact = null;
            }
        } 
    }
}
