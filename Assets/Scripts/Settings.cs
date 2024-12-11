using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class Settings : MonoBehaviour
{
    [SerializeField] private AudioMixerGroup _audioMixer;
    [SerializeField] Color bgActiveColor;
    [SerializeField] Color handleActiveColor;
    [SerializeField] Color bgDefaultColor;
    [SerializeField] Color handleDefaultColor;
    [SerializeField] Toggle toggle;
    [SerializeField] Toggle toggleEffect;

    Image bgImage, handleImage;
    [SerializeField] Image bgImageEffect;
    [SerializeField] Image handleImageEffect;

    Vector2 handlePosition;
    Vector2 handlePositionEffect;
    RectTransform uiHandleRectTransform;
    RectTransform uiHandleRectTransformEffect;

    private AudioSource bgAudioSource;

    private void Awake()
    {
        bgAudioSource = FindObjectOfType<AudioSource>();
        InitToggle();
    }

    void InitToggle()
    {
        uiHandleRectTransform = toggle.transform.GetChild(0).GetChild(0).GetComponent<RectTransform>();
        uiHandleRectTransformEffect = toggleEffect.transform.GetChild(0).GetChild(0).GetComponent<RectTransform>();
        handlePosition = uiHandleRectTransform.anchoredPosition;
        handlePositionEffect = uiHandleRectTransformEffect.anchoredPosition;
        bgImage = uiHandleRectTransform.parent.GetComponent<Image>();
        handleImage = uiHandleRectTransform.GetComponent<Image>();
        toggle.onValueChanged.AddListener(OnSwitch);
        toggleEffect.onValueChanged.AddListener(ToggleEffect);

        bool isOn = PlayerPrefs.GetInt("soundEffects", 0) == 1;

        bool isOnEffect = PlayerPrefs.GetInt("Effects", 0) == 1;
        toggleEffect.isOn = isOnEffect;
        SwitchToggleVisualEffect(isOnEffect);
        UpdateAudioSourceEffect();

        toggle.isOn = isOn;
        SwitchToggleVisual(isOn);
        UpdateAudioSource();
        Debug.Log(isOn);


        /*toggle.isOn = PlayerPrefs.GetInt("soundEffects", 0)==0 ? false : true;
        if (toggle.isOn)
            SwitchToggleVisual(true);*/
    }

    void OnSwitch(bool on)
    {
        SwitchToggleVisual(on);

        if ((PlayerPrefs.GetInt("soundEffects", 0) == 0 ? false : true) != on)
        {
            PlayerPrefs.SetInt("soundEffects", on ? 1 : 0);
            UpdateAudioSource();
        }
    }

    void SwitchToggleVisual(bool on)
    {
        uiHandleRectTransform.DOAnchorPos(on ? handlePosition * -1 : handlePosition, .4f).SetEase(Ease.InOutBack);
        bgImage.DOColor(on ? bgActiveColor : bgDefaultColor, .4f);
        handleImage.DOColor(on ? handleActiveColor : handleDefaultColor, .4f);
    }

    void SwitchToggleVisualEffect(bool on)
    {
        uiHandleRectTransformEffect.DOAnchorPos(on ? handlePositionEffect * -1 : handlePositionEffect, .4f)
            .SetEase(Ease.InOutBack);
        // uiHandleRectTransform.DOAnchorPos(on ? handlePosition * -1 : handlePosition, .4f).SetEase(Ease.InOutBack);
        bgImageEffect.DOColor(on ? bgActiveColor : bgDefaultColor, .4f);
        // bgImage.DOColor(on ? bgActiveColor : bgDefaultColor, .4f);
        handleImageEffect.DOColor(on ? handleActiveColor : handleDefaultColor, .4f);
        // handleImage.DOColor(on ? handleActiveColor : handleDefaultColor, .4f);
    }

    void UpdateAudioSource()
    {
        if (bgAudioSource != null)
        {
            bool on = PlayerPrefs.GetInt("soundEffects", 0) == 0 ? false : true;
            if (on)
            {
                if (!bgAudioSource.isPlaying)
                {
                    bgAudioSource.Play();
                }
                // bgAudioSource.Play();
            }

            else
            {
                if (bgAudioSource.isPlaying)
                {
                    bgAudioSource.Stop();
                }
                 // bgAudioSource.Stop();
            }
               
        }
    }

    void UpdateAudioSourceEffect()
    {
        if (bgAudioSource != null)
        {
            bool on = PlayerPrefs.GetInt("Effects", 0) == 0 ? false : true;

            if (on)
                _audioMixer.audioMixer.SetFloat("Effect", 0);
            else
                _audioMixer.audioMixer.SetFloat("Efect", -80);
        }
    }

    private void OnDestroy()
    {
        if (toggle != null)
        {
            toggle.onValueChanged.RemoveListener(OnSwitch);
        }
    }

    public void ToggleEffect(bool value)
    {
        Debug.Log("VALUE " + value);

        SwitchToggleVisualEffect(value);

        if (value)
        {
            _audioMixer.audioMixer.SetFloat("Effect", 0);
        }

        else
        {
            _audioMixer.audioMixer.SetFloat("Effect", -80);
        }


        PlayerPrefs.SetInt("Effects", value ? 1 : 0);
        UpdateAudioSourceEffect();
    }
}