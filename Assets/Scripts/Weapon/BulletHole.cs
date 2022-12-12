using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletHole : MonoBehaviour
{
    //For how many seconds should the bullet hole be visable?
    [SerializeField] private int upTimeSeconds = 5;

    private void Start()
    {
        StartCoroutine(SelfDestruct());
    }

    IEnumerator SelfDestruct()
    {
        yield return new WaitForSeconds(upTimeSeconds);
        Destroy(gameObject);
    }
}
