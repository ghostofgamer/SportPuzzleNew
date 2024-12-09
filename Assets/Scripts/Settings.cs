using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class Settings : MonoBehaviour
{
    
    [SerializeField] Color bgActiveColor;
    [SerializeField] Color handleActiveColor;
    [SerializeField] Color bgDefaultColor;
    [SerializeField] Color handleDefaultColor;
    [SerializeField] Toggle toggle;

    Image bgImage, handleImage;
    
    Vector2 handlePosition;
    RectTransform uiHandleRectTransform;

    private AudioSource bgAudioSource;

    private void Awake()
    {
        bgAudioSource = FindObjectOfType<AudioSource>();
        InitToggle();
    }

    void InitToggle()
    {
        uiHandleRectTransform = toggle.transform.GetChild(0).GetChild(0).GetComponent<RectTransform>();
        handlePosition = uiHandleRectTransform.anchoredPosition;
        bgImage = uiHandleRectTransform.parent.GetComponent<Image>();
        handleImage = uiHandleRectTransform.GetComponent<Image>();
        toggle.onValueChanged.AddListener(OnSwitch);

        toggle.isOn = PlayerPrefs.GetInt("soundEffects", 0)==0 ? false : true;
        if (toggle.isOn)
            SwitchToggleVisual(true);
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

    private void OnDestroy()
    {
        if (toggle != null)
        {
            toggle.onValueChanged.RemoveListener(OnSwitch);
        }
    }
}
