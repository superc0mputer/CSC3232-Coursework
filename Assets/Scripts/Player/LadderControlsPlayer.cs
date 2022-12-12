using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LadderControlsPlayer : MonoBehaviour
{
    public Transform characterController;
    private bool inside = false;
    [SerializeField] private float speedUpDown = 3.3f;
    private FirstPersonController FPSInput;

    private void Start()
    {
        characterController = gameObject.transform;
        FPSInput = GetComponent<FirstPersonController>();
        inside = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Ladder")
        {
            FPSInput.enabled = false;
            inside = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        FPSInput.enabled = true;
        inside = false;
    }

    private void Update()
    {
        if (inside == true && Input.GetKey("w"))
        {
            characterController.transform.position += Vector3.up * speedUpDown * Time.deltaTime;
        }

        if (inside == true && Input.GetKey("s"))
        {
            characterController.transform.position += Vector3.down * speedUpDown * Time.deltaTime;
        }
    }
}
