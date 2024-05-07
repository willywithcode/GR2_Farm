using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : Singleton<SoundManager>
{

    public AudioSource SoundAudio;
    public AudioSource StepGrassAudio;

    [SerializeField] private AudioClip Sword;
    [SerializeField] private AudioClip PickUp;
    [SerializeField] private AudioClip TakeDamage;
    [SerializeField] private AudioClip StepGrass;


    public void OnPlaySword()
    {
        SoundAudio.PlayOneShot(Sword);
    }

    public void OnPlayPickUp()
    {
        SoundAudio.PlayOneShot(PickUp);
    }

    public void OnTakeDamage()
    {
        SoundAudio.PlayOneShot(TakeDamage);
    }
}
