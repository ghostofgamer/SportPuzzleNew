using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AchievementItem : MonoBehaviour
{
    [SerializeField] private AchievementsEnum _achievementsEnum;
    [SerializeField] private float _maxCount;
    [SerializeField] private Image _imageFill;
    [SerializeField] private TMP_Text _textValue;

    private float _currentCount;

    private void Start()
    {
        switch (_achievementsEnum)
        {
            case AchievementsEnum.StandardLevels:
                ShowStandartModeValue();
                break;

            case AchievementsEnum.TimeLevels:
                ShowTimeModeValue();
                break;

            case AchievementsEnum.CustomLevel:
                ShowCustomModeValue();
                break;

            case AchievementsEnum.AllLevels:
                ShowAllLevelsValue();
                break;
        }
    }

    private void ShowStandartModeValue()
    {
        _currentCount = PlayerPrefs.GetInt("StandardLevel", 0);
        _textValue.text = $"{_currentCount}/{_maxCount}";
        _imageFill.fillAmount = _currentCount / _maxCount;
    }

    private void ShowTimeModeValue()
    {
        _currentCount = PlayerPrefs.GetInt("TimingLevel", 0);
        _textValue.text = $"{_currentCount}/{_maxCount}";
        _imageFill.fillAmount = _currentCount / _maxCount;
    }

    private void ShowCustomModeValue()
    {
        _currentCount = PlayerPrefs.GetInt("CreatePuzzle", 0);
        _textValue.text = $"{_currentCount}/{_maxCount}";
        _imageFill.fillAmount = _currentCount / _maxCount;
    }

    private void ShowAllLevelsValue()
    {
        int standartMode = PlayerPrefs.GetInt("StandardLevel", 0);
        int timeMode = PlayerPrefs.GetInt("TimingLevel", 0);
        _currentCount = standartMode + timeMode;
        _textValue.text = $"{_currentCount}/{_maxCount}";
        _imageFill.fillAmount = _currentCount / _maxCount;
    }
}