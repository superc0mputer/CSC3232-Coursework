using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinSystem : MonoBehaviour
{
    private static CoinSystem instance;

    public static CoinSystem Instance
    {
        get
        {
            return instance;
        }
    }
    
    [SerializeField] private int coins = 100;

    private void Awake()
    {
        instance = this;
    }

    public void AddCoins(int amount)
    {
        coins += amount;
    }

    public void ReduceCoins(int amount)
    {
        coins -= amount;
    }

    public int GetCoins()
    {
        return coins;
    }

    private void Update()
    {
        UIManager.Instance.UpdateCoinText(coins);
    }
}
