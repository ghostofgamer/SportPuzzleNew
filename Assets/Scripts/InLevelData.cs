using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InLevelData : MonoBehaviour
{
    public LevelScriptableObject[] data;
    public LevelScriptableObject[] dataTimers;

    static int levelToLoad = 0;

    public static InLevelData instance;

    private void Awake()
    {
        instance = this;
    }
    public void StartDelegated()
    {
        if (PuzzleController.instance != null)
        {
            Debug.Log("ТУТААА");
            int selectViewGame =  PlayerPrefs.GetInt("SelectViewGame", 0);
            int maxCompletedLevel;
            LevelScriptableObject lso;
            
            if (selectViewGame == 0)
            {
                maxCompletedLevel = PlayerPrefs.GetInt("CurrentStandardLevel", 0);
                Debug.Log("Загружаем Стандартный Левел " + maxCompletedLevel);
                lso = data[maxCompletedLevel];
            }
            else
            {
                maxCompletedLevel = PlayerPrefs.GetInt("CurrentTimingLevel", 0);
                lso = dataTimers[maxCompletedLevel];
            }
            
            /*LevelScriptableObject lso = data[levelToLoad];
            PuzzleController.instance.CreateDefaultPuzzle(levelToLoad + 1, lso.PuzzleCount, lso.Time, lso.Sprite);*/
            // LevelScriptableObject lso = data[maxCompletedLevel];
            PuzzleController.instance.CreateDefaultPuzzle(maxCompletedLevel + 1, lso.PuzzleCount, lso.Time, lso.Sprite);
        }
    }

    public void SetLevelToLoad(int i)
    {
        if (i < data.Length)
            levelToLoad = i;
        else
            Debug.Log("incorrect level to load index");
    }
    public void IncreaseLevelToLoad()
    {
        if (levelToLoad+1 < data.Length)
            levelToLoad++;
        else
            Debug.Log("incorrect level to load index");
    }
    public int GetLevelToLoad()
    {
        return levelToLoad;
    }
}

