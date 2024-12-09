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
        if(looseScreen!=null)looseScreen.SetActive(false);
        gameScreen.SetActive(true);
    }

    public void Win(int countedPuzzles)
    {
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
        InLevelData.instance.IncreaseLevelToLoad();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
