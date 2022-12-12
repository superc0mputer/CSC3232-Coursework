using System;
using System.Collections;
using System.Collections.Generic;
using Pathfinding;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class GameModeSwitch : MonoBehaviour
{
    private static GameModeSwitch instance;
    [SerializeField] private Camera TopViewCamera;
    [SerializeField] private Camera FirstPersonPlayerCamera;
    [SerializeField] private GameObject ItemPickerMenu;
    [SerializeField] private GameObject FirstPersonMenu;
    //[SerializeField] private GameObject player;
    private bool TopDownEnabled;
    private List<GameObject> AllAIGameObjects;
    [SerializeField] private GameObject Weapons;

    public static GameModeSwitch Instance
    {
        get
        {
            return instance;
        }
    }
    private void Awake()
    {
        instance = this;
        
        ItemPickerMenu = GameObject.Find("ItemPickerMenu");
        FirstPersonMenu = GameObject.Find("FirstPersonMenu");
        FindAllAI();
        TopDownGameMode();
    }
    
    public void TopDownGameMode()
    {
        FindAllAI();
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        
        TopDownEnabled = true;
        
        TopViewCamera.enabled = true;
        
        FirstPersonPlayerCamera.enabled = false;
        Weapons.SetActive(false);
        
        //player.SetActive(false);
        
        ItemPickerMenu.SetActive(true);
        FirstPersonMenu.SetActive(false);
        
        DeactivateAllAI();
    }
    
    public void FirstPersonGameMode()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        
        TopDownEnabled = false;
        
        TopViewCamera.enabled = false;
        
        //player.SetActive(true);
        
        FirstPersonPlayerCamera.enabled = true;
        Weapons.SetActive(true);
        
        ItemPickerMenu.SetActive(false);
        FirstPersonMenu.SetActive(true);
        
        ActivateAllAI();
    }

    private void Update()
    {

        if (Input.GetKeyDown(KeyCode.Return))
        {
            if (TopDownEnabled)
            {
                FirstPersonGameMode();
            }
            else
            {
                TopDownGameMode();
            }
        }
    }

    private void DeactivateAllAI()
    {
        foreach (GameObject go in AllAIGameObjects)
        {
            go.SetActive(false);
        }
    }

    private void ActivateAllAI()
    {
        foreach (GameObject go in AllAIGameObjects)
        {
            go.SetActive(true);
        }
    }

    public void FindAllAI()
    {
        AllAIGameObjects = new List<GameObject>();
        GameObject[] gameObjects = GameObject.FindObjectsOfType(typeof(GameObject)) as GameObject[];
        foreach (GameObject go in gameObjects)
        {
            if (go.layer==7)
            {
                AllAIGameObjects.Add(go);
            }
        }
    }

    public bool TopDownGameModeEnabled()
    {
        if (TopDownEnabled)
        {
            return true;
        }

        return false;
    }

    /*private void DeactivateAllAI()
    {
        foreach (var obj in AllAIGameObjects)
        {
            obj.GetComponent<GOAPPlanner>().enabled = false;
            obj.GetComponent<AIPath>().enabled = false;
            
            Goal_Base[] scriptsGoals = gameObject.GetComponents<Goal_Base>();
            foreach(var script in scriptsGoals)
            {
                script.enabled = false;
            }
            
            Action_Base[] scriptsActions = gameObject.GetComponents<Action_Base>();
            foreach(var script in scriptsActions)
            {
                script.enabled = false;
            }
        }
    }

    private void ActivateAllAI()
    {
        foreach (var obj in AllAIGameObjects)
        {
            Debug.Log(obj);
            
            obj.GetComponent<GOAPPlanner>().enabled = true;
            obj.GetComponent<AIPath>().enabled = true;
            
            Goal_Base[] scriptsGoals = obj.GetComponents<Goal_Base>();
            foreach(var script in scriptsGoals)
            {
                script.enabled = true;
            }
            
            Action_Base[] scriptsActions = obj.GetComponents<Action_Base>();
            foreach(var script in scriptsActions)
            {
                script.enabled = true;
            }
        }
    }*/
    
}
