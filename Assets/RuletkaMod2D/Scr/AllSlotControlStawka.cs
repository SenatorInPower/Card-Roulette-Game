using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public enum TypeStawka
{

    Slot,
    Line1,
    Line2,
    Line3,
    _1_12,
    _13_24,
    _25_36,
    _1_18,
    _19_36,
    Even,
    Odd,
    Zero,
    Red,
    Black
};
public class AllSlotControlStawka : MonoBehaviour, IStawka, IStawkaInSlot
{

  //  public static int Stavka { get { return Stavka_; } set { Stavka_ = value; } }
    public TotalRait totalRaitReset;

    public SharLogic sharFinAction;
    public GameObject parrentRuletkaNumber;
    public Button startRotationShar;
    public Button restart;
    Dictionary<TypeStawka, StawkaTypeCriation> IStawka.typeStavkasDictNoSlot { get => typeStavkasDictNoSlot; }
    Dictionary<TypeStawka, StawkaTypeCriation> typeStavkasDictNoSlot;
    Dictionary<string, int> IStawkaInSlot.typeStavkasDictSlot { get => typeStavkasDictInSlot; }
    Dictionary<string, int> typeStavkasDictInSlot;

  //  private static int Stavka_;


    private void OnEnable()
    {
        sharFinAction.FinAction += FinMetod;
        startRotationShar.onClick.AddListener(StartRuletka);
        DeliteStawka.deliteStawka += StawkaDelite;
        restart.onClick.AddListener(RestarScen);
        totalRaitReset.RaitInfoStart();
    }



    private void OnDisable()
    {
        DeliteStawka.deliteStawka -= StawkaDelite;
        sharFinAction.FinAction -= FinMetod;
        startRotationShar.onClick.RemoveListener(StartRuletka);
        restart.onClick.RemoveListener(RestarScen);
       

    }
    
    void RestarScen()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        totalRaitReset.ResetRait();
    }
    void StawkaDelite(SlotType type, StavkaInSlotControl stawka)
    {
        //if (type.typeStawka == TypeStawka.Slot || type.typeStawka == TypeStawka.Zero)
        //{
        //    typeStavkasDictInSlot[type.gameObject.name] = 0;
        //    totalRaitReset.ReturnMoney(stawka.StawkaRestory());
        //}
        //else
        //{
        //    typeStavkasDictNoSlot[type.typeStawka].Stawka = 0;
        //    totalRaitReset.ReturnMoney(stawka.StawkaRestory());

        //}

        if (type.typeStawka == TypeStawka.Slot || type.typeStawka == TypeStawka.Zero)
        {
            totalRaitReset.ReturnMoney(typeStavkasDictInSlot[type.gameObject.name]);
            typeStavkasDictInSlot[type.gameObject.name] = 0;

        }
        else
        {
            totalRaitReset.ReturnMoney(typeStavkasDictNoSlot[type.typeStawka].Stawka);
            typeStavkasDictNoSlot[type.typeStawka].Stawka = 0;


        }

        Destroy(type.localChips);

        Destroy(stawka);
    }
    void StartRuletka()
    {
        if(sharFinAction.RaitSelect)
        sharFinAction.DownButtonStart = true;
    }
    bool StawkaInType(TypeStawka typeStawka, int winSlot)       //проверять если ли победа в данной категории
    {
        int[] slotNumber12 = null;
        int[] slotNumber18 = null;
        switch (typeStawka)
        {
            case TypeStawka.Slot:
                return false;

            case TypeStawka.Line1:

                slotNumber12 = new int[] { 3, 6, 9, 12, 15, 18, 21, 24, 27, 30, 33, 36 };

                for (int i = 0; i < slotNumber12.Length; i++)
                {
                    if (slotNumber12[i] == winSlot)
                    {
                        return true;
                    }
                }
                return false;

            case TypeStawka.Line2:

                slotNumber12 = new int[] { 2, 5, 8, 11, 14, 17, 20, 23, 26, 29, 32, 35 };

                for (int i = 0; i < slotNumber12.Length; i++)
                {
                    if (slotNumber12[i] == winSlot)
                    {
                        return true;
                    }
                }
                return false;

            case TypeStawka.Line3:

                slotNumber12 = new int[] { 1, 4, 7, 10, 13, 16, 19, 22, 25, 28, 31, 34 };

                for (int i = 0; i < slotNumber12.Length; i++)
                {
                    if (slotNumber12[i] == winSlot)
                    {
                        return true;
                    }
                }
                return false;

            case TypeStawka._1_12:

                if (winSlot > 0 && winSlot < 13)
                {
                    return true;
                }
                else
                {
                    return false;
                }


            case TypeStawka._13_24:

                if (winSlot > 12 && winSlot < 25)
                {
                    return true;
                }
                else
                {
                    return false;
                }

            case TypeStawka._25_36:

                if (winSlot > 24 && winSlot < 37)
                {
                    return true;
                }
                else
                {
                    return false;
                }

            case TypeStawka._1_18:
                if (winSlot > 0 && winSlot < 19)
                {
                    return true;
                }
                else
                {
                    return false;
                }

            case TypeStawka._19_36:

                if (winSlot > 18 && winSlot < 37)
                {
                    return true;
                }
                else
                {
                    return false;
                }

            case TypeStawka.Even:   //чет

                if (winSlot % 2 == 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }

            case TypeStawka.Odd:     //нечет

                if (winSlot % 2 != 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }

            case TypeStawka.Zero:
                return false;

            //    if (winSlot == 0)
            //    {
            //        return true;
            //    }
            //    else
            //    {
            //        return false;
            //    }


            case TypeStawka.Red:

                slotNumber18 = new int[] { 1, 3, 5, 7, 9, 12, 14, 16, 18, 19, 21, 23, 25, 27, 30, 32, 34, 36 };

                for (int i = 0; i < slotNumber18.Length; i++)
                {
                    if (slotNumber18[i] == winSlot)
                    {
                        return true;
                    }
                }
                return false;

            case TypeStawka.Black:

                slotNumber18 = new int[] { 2, 4, 6, 8, 10, 11, 13, 15, 17, 20, 22, 24, 26, 28, 29, 31, 33, 35 };

                for (int i = 0; i < slotNumber18.Length; i++)
                {
                    if (slotNumber18[i] == winSlot)
                    {
                        return true;
                    }
                }
                return false;

            default:

                Debug.Log(typeStawka + "typeStawka");

                return false;



        }
    }
    bool MoneyAdd = false;
    private void FinMetod(int id)
    {



        foreach (TypeStawka item in typeStavkasDictNoSlot.Keys)
        {
            if (StawkaInType(item, id))
            {

                Transform NoSlotWin = parrentRuletkaNumber.transform.Find(item.ToString());
                SlotType NoSlotTypes = NoSlotWin.GetComponent<SlotType>();
                if (NoSlotTypes.localChips != null)
                {
                   
                    MoneyAdd=true;
                    TotalMoney.TotalMoneys.Money += typeStavkasDictNoSlot[item].Stawka * typeStavkasDictNoSlot[item].Coeff;
                }
            }
        }

        Transform SlotWin = parrentRuletkaNumber.transform.Find(id.ToString());
        SlotType SlotTypes = SlotWin.GetComponent<SlotType>();


        if (typeStavkasDictInSlot.ContainsKey(id.ToString()) && SlotTypes.localChips != null)
        {
            
            int stawka = typeStavkasDictInSlot[SlotTypes.name];
            MoneyAdd = true;
            TotalMoney.TotalMoneys.Money += StawkaTypeCriation.SlotCoef /** stawkaSlot.countElement*/ * stawka;

        }

        if (MoneyAdd)
        {
            AudioEvent.AudioEvents.AudioPlay(AudioAction.Win);
        }
        else
        {
            AudioEvent.AudioEvents.AudioPlay(AudioAction.Lose);
        }
        restart.gameObject.SetActive(true);
        startRotationShar.gameObject.SetActive(false);
        TotalMoney.TotalMoneys.Info();
       
        //print(stawkaSlot.Coeff + "stawkaSlot.Coeff");
        //print(stawkaSlot.Stawka + "stawkaSlot.Stawka");
        //foreach (var item in typeStavkasDict.Values)
        //{
        //    print(item.countElement + "item.countElement");
        //    print(item.Coeff + "item.Coeff");
        //    print(item.Stawka + "item.Stawka");
        //}
    }
    // Start is called before the first frame update
    void Start()
    {
        InitDictTypeStawka();
    }
    void InitDictTypeStawka()
    {
        typeStavkasDictNoSlot = new Dictionary<TypeStawka, StawkaTypeCriation>();
        typeStavkasDictInSlot = new Dictionary<string, int>();
        //for (int i = 0; i < 12; i++)
        //{
        //    typeStavkasList.Add(new StawkaTypeCriation((TypeStawka)Enum.GetValues(typeof(TypeStawka)).GetValue(0), 0, 0, 0));
        //}


    }
    int WinMoney = 0;
    [Button]
    void Cliar()
    {
        WinMoney = 0;
    }
    public int testID;
    [Button]
    void testAction()
    {
        IDSlots er = new IDSlots();
        er.id = testID;
        FinMetod(testID);
    }
    [Button]
    void FinAction()
    {


        foreach (StawkaTypeCriation item in typeStavkasDictNoSlot.Values)
        {
            WinMoney += item.Coeff * item.countElement * item.Stawka;
        }
        print(WinMoney);
    }
}
