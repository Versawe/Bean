using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingClass : MonoBehaviour
{
    public List<GameObject> barriers = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        foreach (GameObject obj in GameObject.FindGameObjectsWithTag("Barrier"))
        {
            barriers.Add(obj);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
