using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateAStarGraph : MonoBehaviour
{
    private AstarPath path;
    // Start is called before the first frame update
    void Start()
    {
        path = GetComponent<AstarPath>();
    }

    // Update is called once per frame
    void Update()
    {
        path.Scan();
    }
}
