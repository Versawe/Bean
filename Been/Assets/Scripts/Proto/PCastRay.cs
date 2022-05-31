using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PCastRay : MonoBehaviour
{
    Ray pRay;
    RaycastHit rayHit;
    private float sightDistance = 2.25f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //updates raycast's orgin and direction constantly
        UpdateRaycast();

        //debugs ray for in editor view
        Debug.DrawRay(pRay.origin, pRay.direction, Color.red);
    }

    private void FixedUpdate()
    {
        //Checks if the player is looking at anything, right now just prints the game object's name as an example
        if (Physics.Raycast(pRay.origin, pRay.direction, out rayHit, sightDistance))
        {
            if(rayHit.collider.gameObject.tag == "Barrier") 
            {
                rayHit.collider.gameObject.GetComponent<BarrierClass>().BeingLookedAt();
            }
        }
    }

    private void UpdateRaycast()
    {
        //set orgin and direction of raycast
        //origin is camera location, direction is forward vector (Z)
        pRay.origin = transform.position;
        pRay.direction = transform.forward;
    }
}