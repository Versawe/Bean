using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PCastRay : MonoBehaviour
{
    Ray pRay;
    RaycastHit rayHit;
    private float sightDistance = 2.25f;

    private GameObject tempObj;
    private bool IsActive = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //updates raycast's orgin and direction constantly
        UpdateRaycast();

        if (IsActive && tempObj) CheckInteract(tempObj);
        //CastRay();

        //debugs ray for in editor view
        Debug.DrawRay(pRay.origin, pRay.direction, Color.red);
    }

    private void FixedUpdate()
    {
        CastRay();
    }

    private void CastRay()
    {
        //Checks if the player is looking at anything, right now just prints the game object's name as an example
        if (Physics.Raycast(pRay.origin, pRay.direction, out rayHit, sightDistance))
        {
            if (rayHit.collider.gameObject.tag == "Barrier")
            {
                if(rayHit.collider.gameObject.GetComponent<BarrierClass>().status != "Filled") rayHit.collider.gameObject.GetComponent<BarrierClass>().BeingLookedAt();
                IsActive = true;
                tempObj = rayHit.collider.gameObject;
            }
            else 
            {
                IsActive = false;
                tempObj = null;
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

    private void CheckInteract(GameObject currObj)
    {
        if (Input.GetButtonDown("Interact") && currObj.GetComponent<BarrierClass>().status == "Hover")
        {
            currObj.GetComponent<BoxCollider>().isTrigger = false;
            currObj.GetComponent<MeshRenderer>().material = currObj.GetComponent<BarrierClass>().mats[2];
            currObj.GetComponent<BarrierClass>().status = "Filled";
            currObj.GetComponent<BarrierClass>().TriggerMatSwitch(2);
            print("work");
        }
    }
}