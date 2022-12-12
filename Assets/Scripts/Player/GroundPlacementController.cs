using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;

public class GroundPlacementController : MonoBehaviour
{


    private PlaceableObject currentPlaceableObject;
    private GameObject currentPlaceableObjectPrefab;

    private float rotationy = 0;

    private void Update()
    {
        if (currentPlaceableObject != null)
        {
            RotateCurrentObject();
            MoveCurrentObjectToMouse();
            ReleaseIfClicked();
        }
    }
    public void SetCurrentPlaceableObject(PlaceableObject placeableObject)
    {
        if (placeableObject != null && placeableObject.cost <= CoinSystem.Instance.GetCoins())
        {
            currentPlaceableObject = placeableObject;
            currentPlaceableObjectPrefab = Instantiate(placeableObject.prefab);
        }
        else
        {
            if (currentPlaceableObjectPrefab != null)
            {
                Destroy(currentPlaceableObjectPrefab);
                currentPlaceableObjectPrefab = null;
            }

            currentPlaceableObject = null;
        }
    }
    

    private void MoveCurrentObjectToMouse()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitInfo;

        if (Physics.Raycast(ray, out hitInfo))
        {
            //currentPlaceableObjectPrefab.transform.position = hitInfo.point; (doesn't work correctly because of top down view)
            currentPlaceableObjectPrefab.transform.position = new Vector3(hitInfo.point.x, -3, hitInfo.point.z);
            //currentPlaceableObjectPrefab.transform.rotation = Quaternion.FromToRotation(Vector3.up, hitInfo.normal);
            currentPlaceableObjectPrefab.transform.rotation = Quaternion.Euler(0, rotationy, 0);
        }
    }

    private void ReleaseIfClicked()
    {
        if (Input.GetMouseButton(0))
        {
            //Update Coins of player
            CoinSystem.Instance.ReduceCoins(currentPlaceableObject.cost);
            
            currentPlaceableObject = null;
            currentPlaceableObjectPrefab = null;
            rotationy = 0;
            
           
        }
    }

    private void RotateCurrentObject()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (rotationy == 0)
            {
                rotationy = 90;
            }
            else
            {
                rotationy = 0;
            }
        }
    }
    
}
