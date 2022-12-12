using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    private Slider slider;
    private float currentHealth;
    private float maxHealth;

    [SerializeField] private GameObject currentObject;
    void Start()
    {
        slider = GetComponent<Slider>();
        maxHealth = (float) currentObject.GetComponent<HitHealth>().GetMaxHealth();
    }

   
    void LateUpdate()
    {
        if (currentObject == null) return;
        currentHealth = (float) currentObject.GetComponent<HitHealth>().GetHealth();
        slider.value = currentHealth / maxHealth;
    }
}
