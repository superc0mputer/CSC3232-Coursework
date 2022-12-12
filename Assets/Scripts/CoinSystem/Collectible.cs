using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectible : MonoBehaviour
{
    [SerializeField] private int worth = 10;
   

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Debug.Log("Collected Collectible");
            CoinSystem.Instance.AddCoins(worth);
            Destroy(gameObject);
        }
    }
}
