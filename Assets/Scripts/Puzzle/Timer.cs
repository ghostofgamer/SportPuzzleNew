using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    public TextMeshProUGUI textField;
    public int lifeTime = 300;

    private int timeLeft;
    private float mins;
    private float secs;
    public static Timer instance;

    private void Awake()
    {
        instance = this;   
        textField.gameObject.SetActive(false);
    }
    public void StartTimer(int time)
    {
        timeLeft = time;
        textField.gameObject.SetActive(true);
        StartCoroutine(SecundsUpdate());
    }


    public void stopTimer()
    {
        StopAllCoroutines();
    }

    IEnumerator SecundsUpdate()
    {
        while(timeLeft >= 0)
        {          
            mins = timeLeft / 60;
            secs = timeLeft % 60;
            textField.text = mins.ToString("f0") + ":" + (secs < 10 ? "0" + secs.ToString("f0") : secs.ToString("f0"));
            yield return new WaitForSeconds(1f);
            timeLeft--;
        }
        WinScript.instance.Loose();
    }
}
