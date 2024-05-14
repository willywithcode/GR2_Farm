using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class OptionsMenu : MonoBehaviour
{
    [SerializeField] Slider SFXSlider;
    [SerializeField] AudioMixer MyMixer;

    public void SetVolume()
    {
        float volume = SFXSlider.value;
        MyMixer.SetFloat("SFX", volume);
    }

    public void Exit()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }
}
