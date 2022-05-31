using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrierClass : MonoBehaviour
{
    private List<string> statusOptions = new List<string>() 
    {
        "Null",
        "Hover",
        "Filled"
    };

    public List<Material> mats = new List<Material>();

    public string status = "Null";
    public bool LookedAt = false;

    private bool IsLookedAt = false;
    private float lookTimer = 0.05f;

    MeshRenderer mr;

    private void Awake()
    {
        mr = GetComponent<MeshRenderer>();
        mr.material = mats[0];
    }

    private void Update()
    {
        DeactivateHover();
    }

    public void BeingLookedAt()
    {
        if (status == statusOptions[2]) return;
        TriggerMatSwitch(1);
        IsLookedAt = true;
        lookTimer = 0.05f;
    }

    private void DeactivateHover()
    {
        if (!IsLookedAt) return;
        lookTimer -= 1 * Time.deltaTime;
        if(lookTimer <= 0) 
        {
            IsLookedAt = false;
            TriggerMatSwitch(0);
            lookTimer = 0.05f;
        }

    }

    private void TriggerMatSwitch(int ele) 
    {
        for (int i = 0; i < statusOptions.Count - 1; i++) 
        {
            if(i == ele) 
            {
                status = statusOptions[ele];
                mr.material = mats[ele];
                print(i);
                print(ele);
            }
        }
    }
}
