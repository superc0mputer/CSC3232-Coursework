using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class HitHealth : MonoBehaviour
{
    [SerializeField] private int maxHealth = 100;
    private int health;

    private void Awake()
    {
        health = maxHealth;
    }

    private void LateUpdate()
    {
        //Debug.Log("CurrentHealth" + health);
        if (health <= 0)
        {
            //Destroy all children (bulletholes) when object is destroyed
            for (int i = gameObject.transform.childCount - 1; i >= 0; i--)
            {
                Destroy(gameObject.transform.GetChild(i).gameObject);
            }
            
            Destroy(gameObject);
        }
    }

    public void decreaseHealth(int amount)
    {
        //Debug.Log("DecreasingHealth");
        health = health - amount;
    }

    public int GetHealth()
    {
        return health;
    }

    public int GetMaxHealth()
    {
        return maxHealth;
    }

    public int getHealthPercentage()
    {
        return health / maxHealth;
    }
}
