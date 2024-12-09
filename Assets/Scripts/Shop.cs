using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void GetExtraLife()
    {
        int amount = PlayerPrefs.GetInt("valute", 0);
        int health = PlayerPrefs.GetInt("health", 3);
        int price = 2;
        if (amount > price && health < 3)
        {
            amount -= price;
            health++;
            HealthController.instance.IncreaseHealth();

            PlayerPrefs.SetInt("valute", amount);
            valuteUI.instance.updateUI();
        }
    }
}
