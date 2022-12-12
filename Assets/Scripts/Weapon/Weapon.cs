using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    //Raycast
    [SerializeField] private LayerMask[] hittableLayers;
    [SerializeField] private float weaponRange;

    private Camera mainCamera;

    //Effects
    [SerializeField] private GameObject bulletHole;
    [SerializeField] private ParticleSystem muzzleFlash;
    [SerializeField]
    private float bulletHolePositionOffset;

    [SerializeField] private float fireRate; //Number between 0 and 1
    private bool canShoot = true;
    private float thresholdTime;

    private void Awake()
    {
        //Get main camera and store it in variable
        mainCamera = Camera.main;
    }

    private void Update()
    {
        mainCamera = Camera.main;
        if (thresholdTime < Time.time)
        {
            canShoot = true;
            thresholdTime = Time.time + fireRate;
        }
        else
        {
            canShoot = false; 
        }
        
        if (Input.GetMouseButton(0) && canShoot)
            Shoot();
    
    }

    private void Shoot()
    {
        HandleMuzzleFlash();
        HandleRaycast();
    }

    private void HandleRaycast()
    {
        foreach(var hittableLayer in hittableLayers) {
            if (Physics.Raycast(mainCamera.transform.position, mainCamera.transform.forward, out RaycastHit hit,
                weaponRange, hittableLayer))
            {
                

                //Bullet Hole
                GameObject currentBulletHole = Instantiate(bulletHole,
                    hit.point + (hit.normal * bulletHolePositionOffset),
                    Quaternion.LookRotation(-hit.normal));

                //Set BulletHole as child of current hittableObject
                currentBulletHole.transform.parent = hit.collider.gameObject.transform;

                //Damage to hittableObject
                GameObject hitObject = hit.collider.gameObject;
                hitObject.GetComponent<HitHealth>().decreaseHealth(10);

            }
            else
            {
                
            }
        }
    }

    private void HandleMuzzleFlash()
    {
        if (muzzleFlash.isPlaying)
        {
            muzzleFlash.Stop();
        }
        muzzleFlash.Play();
    }
}
