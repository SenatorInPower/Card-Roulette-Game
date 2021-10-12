using System.Collections;
using UnityEngine;


    public class StawkaTypeCriation : TypeStavka
{
    public const int SlotCoef=36;
    TypeStawka typeStawka_;
    public override TypeStawka typeStawka { get => typeStawka_; set => typeStawka_=value; }

    int countElements_=0;

    public override int countElement { get => countElements_; set => countElements_ = value; }
    int Coeff_;

    public override int Coeff { get => Coeff_; set => Coeff_ = value; }
    int Stawka_;

    public override int Stawka { get => Stawka_; set => Stawka_=value; }

    public StawkaTypeCriation(TypeStawka typeStawka_, int countElements_, int Coeff_,int Stawka_)
    {
        this.typeStawka_ = typeStawka_;
        this.countElements_ = countElements_;
        this.Coeff_ = Coeff_;
        this.Stawka = Stawka_;
    }



   
    }
