using UnityEngine;

namespace DefaultNamespace
{
    [System.Serializable]
    public class PlaceableObject
    {
        [SerializeField] public GameObject prefab;
        [SerializeField] public int cost;
        
        
    }
}