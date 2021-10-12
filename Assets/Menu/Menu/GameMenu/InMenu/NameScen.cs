using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum ScenGame
{
    Ruletka,
    BJ,
    Solitair,
    Bingo,
    Dou,
    Point
}
public class NameScen : MonoBehaviour
{
    public string scenName;
    public ScenGame ScenGame;
}
