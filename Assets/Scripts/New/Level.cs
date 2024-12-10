using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Level : MonoBehaviour
{
    [SerializeField] private GameObject _lock;
    [SerializeField] private int _index;
    [SerializeField] private bool _standartLevel;
    [SerializeField] private SceneLoader _sceneLoader;
    [SerializeField] private TMP_Text _text;


    private void Start()
    {
        _text.text = $"Level {_index + 1}";

        if (_standartLevel)
        {
            int saveData = PlayerPrefs.GetInt("StandardLevel", 0);
            _lock.SetActive(_index > saveData);
            GetComponent<Button>().interactable = _index <= saveData;
        }
        else
        {
            int saveData = PlayerPrefs.GetInt("TimingLevel", 0);
            _lock.SetActive(_index > saveData);
            GetComponent<Button>().interactable = _index <= saveData;
        }
    }

    public void Select()
    {
        if (_standartLevel)
        {
            PlayerPrefs.SetInt("SelectViewGame", 0);
            PlayerPrefs.SetInt("CurrentStandardLevel", _index);
            _sceneLoader.LoadScene("StandardMode");
        }
        else
        {
            PlayerPrefs.SetInt("CurrentTimingLevel", _index);

            PlayerPrefs.SetInt("SelectViewGame", 1);
        }
    }
}