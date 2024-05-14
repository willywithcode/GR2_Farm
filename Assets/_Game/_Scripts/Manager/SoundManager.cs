using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : Singleton<SoundManager>
{

    public AudioSource SoundAudioAll;
    //public AudioSource SoundAudioPlayer;
    public AudioSource StepGrassAudio;

    [SerializeField] private AudioClip Sword;
    [SerializeField] private AudioClip PickUp;
    [SerializeField] private AudioClip TakeDamage;
    [SerializeField] private AudioClip EdiableItem;
    [SerializeField] private AudioClip StepGrass;


    public void OnPlaySword()
    {
        SoundAudioAll.PlayOneShot(Sword);
    }

    public void OnPlayPickUp()
    {
        SoundAudioAll.PlayOneShot(PickUp);
    }

    public void OnTakeDamage()
    {
        SoundAudioAll.PlayOneShot(TakeDamage);
    }

    public void OnEdiableItem()
    {
        SoundAudioAll.PlayOneShot(EdiableItem);
    }

}
