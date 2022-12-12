using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameState : MonoBehaviour
{
    private GameObject player;
    private GameObject PlayerBase;
    private GameObject EnemyBase;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        PlayerBase = GameObject.Find("PlayerTeamBase");
        EnemyBase = GameObject.Find("EnemyTeamBase");
    }

    // Update is called once per frame
    void Update()
    {
        if (player == null || PlayerBase == null)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            SceneManager.LoadScene("Loss");
        }

        if (EnemyBase == null)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            SceneManager.LoadScene("Win");
        }
        
        
    }
    
    
}
