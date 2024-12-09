using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class valuteUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI text;

    public static valuteUI instance;

    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        updateUI();
    }

    public void updateUI()
    {
        text.text = PlayerPrefs.GetInt("valute", 0).ToString();
    }
}
