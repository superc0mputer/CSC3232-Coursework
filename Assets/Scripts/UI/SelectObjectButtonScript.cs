using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectObjectButtonScript : MonoBehaviour
{
    [SerializeField] private PlaceableObjectHandler placeableObjectHandler;

    [SerializeField] private int objectIndex;
    
    public void selectPlaceableObject()
    {
        placeableObjectHandler.setcurrentPlaceableObjectWithIndex(objectIndex);
    }
}
