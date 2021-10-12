using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum AudioAction
{
    Win,
    Lose,
    Bet,
    NoRait,
    Purshes,
    ClickToUI,
    ClickToCard,
    Shop,
    Settings,
    MenuStart,
    LevelStart


}
public class AudioEvent : MonoBehaviour
{



    public static AudioEvent AudioEvents;

    public AudioSource AudioSource;

    public List<AudioClip> clipList;

    private void Awake()
    {
        if (AudioEvents == null)
        {
            AudioSource=gameObject.GetComponent<AudioSource>();
            AudioEvents = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public void AudioPlay(AudioAction audioAction)
    {
        AudioSource.PlayOneShot(clipList[(int)audioAction],1);
        
    }

}
