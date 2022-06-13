using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PCastRay : MonoBehaviour
{
    //ray varibales
    Ray pRay;
    RaycastHit rayHit;
    private float sightDistance = 2.25f;

    //temp vars for Objs being looked at
    private GameObject tempObj;
    private bool IsActive = false;

    // Update is called once per frame
    void Update()
    {
        //updates raycast's orgin and direction constantly
        UpdateRaycast();

        //Calls CheckInteract only if conditions are met
        if (IsActive && tempObj) CheckInteract(tempObj);
        //CastRay();

        //debugs ray for in-editor view
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
            if (rayHit.collider.gameObject.tag == "Barrier") //if player is looking at a barrier
            {
                if(rayHit.collider.gameObject.GetComponent<BarrierClass>().status != "Filled") rayHit.collider.gameObject.GetComponent<BarrierClass>().BeingLookedAt();
                IsActive = true;
                tempObj = rayHit.collider.gameObject;
            }
            else //resets values
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

    //Checks interactions when player looks at an object and clicks a button
    private void CheckInteract(GameObject currObj)
    {
        //logic for if the object is a barrier
        if (Input.GetButtonDown("Interact") && currObj.GetComponent<BarrierClass>().status == "Hover") // builds barrier with interact
        {
            currObj.GetComponent<BarrierClass>().TriggerMatSwitch(2);
        }
        else if (Input.GetButtonDown("Interact2") && currObj.GetComponent<BarrierClass>().status == "Filled") // unbuilds barrier with interact2
        {
            currObj.GetComponent<BarrierClass>().TriggerMatSwitch(1);
        }
    }
}