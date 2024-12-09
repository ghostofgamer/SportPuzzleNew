using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundController : MonoBehaviour
{
    private AudioSource bgAudioSource;

    public void AwakeDelegated()
    {
        bgAudioSource = FindObjectOfType<AudioSource>();
        UpdateAudioSource();
    }

    void UpdateAudioSource()
    {
        if (bgAudioSource != null)
        {
            bool on = PlayerPrefs.GetInt("soundEffects", 0) == 0 ? false : true;
            if (on)
                bgAudioSource.Play();
            else
                bgAudioSource.Stop();
        }
    }
}
