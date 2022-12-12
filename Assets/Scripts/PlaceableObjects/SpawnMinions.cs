using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnMinions : MonoBehaviour
{
    [SerializeField] private GameObject MinionPrefab;
    private int numberOfMinions = 0;

    private void Update()
    {
        SpawnMinion();
    }

    public void AddToNumberOfMinions()
    {
        if(CoinSystem.Instance.GetCoins() >= 30) {
            numberOfMinions++;
            CoinSystem.Instance.ReduceCoins(30);
        }
    }

    public void SpawnMinion()
    {
        if (!GameModeSwitch.Instance.TopDownGameModeEnabled())
        {
            for (int i = 0; i < numberOfMinions; i++)
            {
                numberOfMinions--;
                Invoke("InstantiateMinion", i);
            }
        }
    }
    private void InstantiateMinion()
    {
        Instantiate(MinionPrefab, gameObject.transform.position, gameObject.transform.rotation);
    }
    
}
