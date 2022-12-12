using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemies : MonoBehaviour
{
   //If player buys lots of walls: Spawn enemies with high priority to destroy base / walls
   //If player buys lots of minions: Spawn enemies with high priority to destroy other minions

   //Every Time the player changes GameMode to Select Items the AI can also choose a certain amount of Minions to spawn

   [SerializeField] private int numberOfMinionsPerRound = 5;

   private List<GameObject> playerWalls;
   private List<GameObject> playerMinions;
   private GameObject[] playerTeam;

   private int numberOfMinionsToSpend;
   private bool minionsRecieved = false;

   [SerializeField] private GameObject BaseAttackerPrefab;
   [SerializeField] private GameObject MinionAttackerPrefab;

   private int numbersOfBaseAttackerstoSpawn;
   private int numbersOfMinionAttackerstoSpawn;
   private void Awake()
   {
      numberOfMinionsToSpend = 0;
   }

   private void Update()
   {
      if (!GameModeSwitch.Instance.TopDownGameModeEnabled())
      {
         if(!minionsRecieved) {
            numberOfMinionsToSpend += numberOfMinionsPerRound;
            minionsRecieved = true;
            UpdateDetectedPlayerTeam();
            SpendMinions();
         }
      }
      else
      {
         minionsRecieved = false;
      }
   }

   private void UpdateDetectedPlayerTeam()
   {
      playerTeam = GameObject.FindGameObjectsWithTag("PlayerTeam");
      
      playerWalls = new List<GameObject>();
      playerMinions = new List<GameObject>();
      
      foreach (var obj in playerTeam)
      {
         if (obj.layer == 6) //6 = Obstacle 
         {
            playerWalls.Add(obj);
         } else if (obj.layer == 7) //7 = AI
         {
            playerMinions.Add(obj);
         }
      }
   }

   private void SpendMinions()
   {
      if (playerTeam.Length == 0)
      {
         numbersOfBaseAttackerstoSpawn = Mathf.FloorToInt(numberOfMinionsToSpend * 0.5f);
         numbersOfMinionAttackerstoSpawn = Mathf.FloorToInt(numberOfMinionsToSpend * 0.5f);

      }
      else
      {
         float percentageWalls = (float) playerWalls.Count / playerTeam.Length;
         float percentageMinions = (float) playerMinions.Count / playerTeam.Length;

         numbersOfBaseAttackerstoSpawn = Mathf.FloorToInt(numberOfMinionsToSpend * percentageWalls);
         numbersOfMinionAttackerstoSpawn = Mathf.FloorToInt(numberOfMinionsToSpend * percentageMinions);
      }

      for (int i = 0; i < numbersOfBaseAttackerstoSpawn; i++)
      {
         numberOfMinionsToSpend--;
         Invoke("InstantiateBaseAttacker", i);
      }

      for (int i = 0; i < numbersOfMinionAttackerstoSpawn; i++)
      {
         numberOfMinionsToSpend--;
         Invoke("InstantiateMinionAttacker", i+0.5f);
      }
   }

   private void InstantiateBaseAttacker()
   {
      Instantiate(BaseAttackerPrefab, gameObject.transform.position, gameObject.transform.rotation);
   }

   private void InstantiateMinionAttacker()
   {
      Instantiate(MinionAttackerPrefab, gameObject.transform.position, gameObject.transform.rotation);
   }
}
