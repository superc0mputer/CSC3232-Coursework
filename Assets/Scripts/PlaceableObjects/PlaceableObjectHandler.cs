    using System.Collections;
using System.Collections.Generic;
    using DefaultNamespace;
    using UnityEngine;

public class PlaceableObjectHandler : MonoBehaviour
{
    [SerializeField] private GroundPlacementController groundPlacementController;

    [SerializeField] private PlaceableObject[] placeableObjects;

    public void setcurrentPlaceableObjectWithIndex(int i)
    {
        groundPlacementController.SetCurrentPlaceableObject(placeableObjects[i]);
    }

    public void setCurrentPlaceableObjectToNull()
    {
        groundPlacementController.SetCurrentPlaceableObject(null);
    }
}
