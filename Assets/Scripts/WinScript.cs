using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class WinScript : MonoBehaviour
{
    public static int levelNumber = 1;
    public static WinScript instance;

    [SerializeField] GameObject gameScreen;
    [SerializeField] GameObject winScreen;
    [SerializeField] GameObject looseScreen;
    [SerializeField] TextMeshProUGUI levelName;
    [SerializeField] TextMeshProUGUI countedText;
    [SerializeField] ParticleSystem winEffect;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        winScreen.SetActive(false);
        if (looseScreen != null) looseScreen.SetActive(false);
        gameScreen.SetActive(true);
    }

    public void Win(int countedPuzzles)
    {
        int selectViewGame = PlayerPrefs.GetInt("SelectViewGame", 0);

        if (selectViewGame == 0)
        {
            int maxCompletedLevel = PlayerPrefs.GetInt("StandardLevel", 0);
            int currentLevel =  PlayerPrefs.GetInt("CurrentStandardLevel", 0);
            
            if (maxCompletedLevel <= currentLevel)
            {
                maxCompletedLevel++;
                            
                            PlayerPrefs.SetInt("StandardLevel", maxCompletedLevel);
                            Debug.Log("Сохр Standart " + maxCompletedLevel);
            }
            
        }
        else
        {
            int maxCompletedLevel = PlayerPrefs.GetInt("TimingLevel", 0);
            maxCompletedLevel++;
            PlayerPrefs.SetInt("TimingLevel", maxCompletedLevel);
        }

        gameScreen.SetActive(false);
        winScreen.SetActive(true);
        countedText.text = "You get " + countedPuzzles;
        PlayerPrefs.SetInt("valute", PlayerPrefs.GetInt("valute", 0) + countedPuzzles);
        valuteUI.instance.updateUI();
        if (!PuzzleController.instance.setUserPuzzle)
        {
            Timer.instance.stopTimer();
            winEffect.Play();
        }
    }

    public void Loose()
    {
        gameScreen.SetActive(false);
        looseScreen.SetActive(true);
        HealthController.instance.DecreaseHealth();
    }

    public void NextLevel()
    {
        // InLevelData.instance.IncreaseLevelToLoad();
        int selectViewGame =  PlayerPrefs.GetInt("SelectViewGame", 0);
            
        if (selectViewGame == 0)
        {
           int currentStandard = PlayerPrefs.GetInt("CurrentStandardLevel", 0);
           currentStandard++;
           PlayerPrefs.SetInt("CurrentStandardLevel", currentStandard);
        }
        else
        {
           int currentTiming = PlayerPrefs.GetInt("CurrentTimingLevel", 0);
           currentTiming++;
           PlayerPrefs.SetInt("CurrentTimingLevel", currentTiming);
        }
        
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}