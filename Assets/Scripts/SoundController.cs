using System;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SoundController : MonoBehaviour
{
    [SerializeField] private AudioSource bgAudioSource;
    [SerializeField] private AudioMixerGroup _audioMixerGroup;

    private void Start()
    {
        bool onEffect = PlayerPrefs.GetInt("Effects", 0) == 0 ? false : true;
Debug.Log("ValueEffect " + onEffect);
        if (onEffect)
            _audioMixerGroup.audioMixer.SetFloat("Effect", 0);
        else
            _audioMixerGroup.audioMixer.SetFloat("Effect", -80);
        

        if (bgAudioSource != null)
        {
            bool on = PlayerPrefs.GetInt("soundEffects", 0) == 0 ? false : true;
            if (on)
                bgAudioSource.Play();
            else
                bgAudioSource.Stop();

            Debug.Log("ТУТ " + on);
        }
    }

    public void AwakeDelegated()
    {
        // bgAudioSource = FindObjectOfType<AudioSource>();
        UpdateAudioSource();
    }

    void UpdateAudioSource()
    {
        /*Debug.Log("аываыа");
        if (bgAudioSource != null)
        {
            bool on = PlayerPrefs.GetInt("soundEffects", 0) == 0 ? false : true;
            if (on)
                bgAudioSource.Play();
            else
                bgAudioSource.Stop();

            Debug.Log("ТУТ " + on);
        }*/
    }
}