using System;
using System.Collections;
using UnityEngine;

public class HealthRecovery : MonoBehaviour
{
    [SerializeField] int recoveryTime = 180;
    public static HealthRecovery instance;
    HealthController healthController;

    internal bool recovering;
    private void Awake()
    {
        instance = this;
    }
    public void StartDelegate()
    {
        CalculateOfflineIncome();
    }
    public void TryInit()
    {
        healthController = FindObjectOfType<HealthController>();
    }
    private void CalculateOfflineIncome()
    {
        string lastPlayedTimeString = PlayerPrefs.GetString("LastPlayedTime", null);

        var lastPlayedTime = DateTime.Parse(lastPlayedTimeString);
        float secondsSpan = (float)(DateTime.UtcNow - lastPlayedTime).TotalSeconds;

        float totalRecovery = secondsSpan / recoveryTime;
        int savedHealth = PlayerPrefs.GetInt("health", 3);
        PlayerPrefs.SetInt("health", Math.Min(savedHealth + (int)totalRecovery,3));
    }

    private void OnApplicationQuit()
    {
        PlayerPrefs.SetString("LastPlayedTime", DateTime.UtcNow.ToString());
    }

    private void Update()
    {
        if (!recovering && PlayerPrefs.GetInt("health", 3) < 3)
        {
            Debug.Log("recover");
            StartCoroutine(Recover());
        }
    }
    public IEnumerator Recover()
    {
        recovering = true;
        yield return new WaitForSeconds(recoveryTime);
        

        Debug.Log("recovered");
        if (FindObjectOfType<HealthController>() != null)
        {
            HealthController.instance.IncreaseHealth();
        }
        else
        {
            int savedHealth = PlayerPrefs.GetInt("health", 3);
            PlayerPrefs.SetInt("health", savedHealth > 2 ? 3 : savedHealth + 1);
        }
        recovering = false;
    }
}
