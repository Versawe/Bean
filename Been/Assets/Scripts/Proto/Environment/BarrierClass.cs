using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrierClass : MonoBehaviour
{
    //List of set states for Barriers
    private List<string> statusOptions = new List<string>() 
    {
        "Null",
        "Hover",
        "Filled"
    };

    //List of set Materials barriers could have
    public List<Material> mats = new List<Material>();

    // Status & Look-At Variables
    public string status = "Null";
    public bool LookedAt = false;
    private bool IsLookedAt = false;
    private float lookTimer = 0.05f;


    [Range(0f, 100f)]
    public float health;

    //Component vars
    MeshRenderer mr;
    BoxCollider bc;

    private void Awake()
    {
        mr = GetComponent<MeshRenderer>();
        bc = GetComponent<BoxCollider>();
        mr.material = mats[0]; //starts invisable
    }

    private void Update()
    {
        //checks if you are hovering over object or not
        DeactivateHover();
    }

    //is triggered from PCastRay.cs when the ray collides with the barrier
    public void BeingLookedAt()
    {
        if (status == statusOptions[2]) return;
        TriggerMatSwitch(1);
        IsLookedAt = true;
        lookTimer = 0.05f;
    }

    //deactivates hover state when player looks away from barrier
    private void DeactivateHover()
    {
        if (!IsLookedAt) return;
        if (status == statusOptions[2]) return;

        lookTimer -= 1 * Time.deltaTime;
        if(lookTimer <= 0) 
        {
            IsLookedAt = false;
            TriggerMatSwitch(0);
            lookTimer = 0.05f;
        }
    }

    //switches state and material for barrier
    public void TriggerMatSwitch(int ele)
    {
        for (int i = 0; i < statusOptions.Count; i++)
        {
            if(i == ele) 
            {
                status = statusOptions[ele];
                mr.material = mats[ele];

                if (i < 2) bc.isTrigger = true;
                else bc.isTrigger = false;
            }
        }
    }
}
