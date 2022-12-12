using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    private static UIManager _instance;

    public static UIManager Instance
    {
        get
        {
            return _instance;
        }
    }
    
    public TextMeshProUGUI coinText;

    private void Awake()
    {
        _instance = this;
    }
    

    public void UpdateCoinText(int coins)
    {
        coinText.text = "Current Coins: " + coins;
    }
    
}
