using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class HealthController : MonoBehaviour
{
    [SerializeField] Heart[] hearts;

    public int health;
    int maxHealth = 3;
    
    public static HealthController instance;

    void Awake()
    {
        instance = this;
        health = PlayerPrefs.GetInt("health", 3);
    }
    private void Start()
    {
        UpdateUI();
    }

    public void DecreaseHealth()
    {
        health = Math.Max(health-1, 0);
        UpdateHP();
    }
    public void IncreaseHealth()
    {
        health = Math.Min(health + 1, maxHealth);
        UpdateHP();
    }
    public void UpdateHP()
    {
        PlayerPrefs.SetInt("health", health);

        UpdateUI();    
    }

    public void UpdateUI()
    {
        for (int i = 0; i < hearts.Length; i++)
        {
            hearts[i].Set(i < health);
        }
    }
}
