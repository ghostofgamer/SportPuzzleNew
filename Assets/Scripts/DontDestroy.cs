using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DontDestroy : MonoBehaviour
{
    private InLevelData levelData;
    private HealthRecovery healthRecovery;
    private SoundController soundController;    

    void Awake()
    {
        GameObject[] objs = GameObject.FindGameObjectsWithTag("data");

        if (objs.Length > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
            SceneManager.sceneLoaded += OnSceneLoaded;

            levelData = GetComponent<InLevelData>();
            healthRecovery = GetComponent<HealthRecovery>();
            soundController = GetComponent<SoundController>();

            healthRecovery.StartDelegate();
            soundController.AwakeDelegated();
        }

    }
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        levelData.StartDelegated();
        healthRecovery.TryInit();
        
    }
    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

}
