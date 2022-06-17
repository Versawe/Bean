using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AI : MonoBehaviour
{
    //Ray for Vision
    Ray aiRay;
    RaycastHit rayHit;
    float sightDistance = 2f;

    //important vars
    public string state = "";

    [SerializeField]
    private Transform playerLoc;

    //Component Vars
    NavMeshAgent nm;
    GameObject DestroyObj = null;
    // Start is called before the first frame update
    void Start()
    {
        playerLoc = GameObject.FindGameObjectWithTag("Player").transform;

        //float distance = Vector3.Distance(transform.position, playerLoc);

        nm = GetComponent<NavMeshAgent>();

        state = "Chase";
        //ChasePlayer();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateRay();
        Debug.DrawRay(aiRay.origin, aiRay.direction);

        ConfigureStates();
    }

    private void FixedUpdate()
    {
        CastRay();
    }

    private void ConfigureStates()
    {
        if (state == "Chase") 
        {
            ChasePlayer();
        }
        else if (state == "Destroy") 
        {
            nm.isStopped = true;
        }
    }

    private void ChasePlayer()
    {
        
        float distance = Vector3.Distance(playerLoc.position, transform.position);
        if (distance <= nm.stoppingDistance) 
        {
            nm.isStopped = true;
        }
        else
        {
            nm.isStopped = false;
            nm.SetDestination(playerLoc.position);
        }
    }

    private void UpdateRay()
    {
        aiRay.origin = transform.position;
        aiRay.direction = transform.forward;
    }

    private void CastRay() 
    {
        if(Physics.Raycast(aiRay.origin, aiRay.direction, out rayHit, sightDistance))
        {
            if (rayHit.collider.gameObject.GetComponent<BarrierClass>()) 
            {
                BarrierClass bar = rayHit.collider.gameObject.GetComponent<BarrierClass>();

                if (bar.status == "Null" || bar.status == "Hover") 
                {
                    DestroyObj = null;
                    state = "Chase";
                }
                else 
                {
                    state = "Destroy";
                    DestroyObj = bar.gameObject;
                }
            }
            else 
            {
                DestroyObj = null;
            }

            if(rayHit.collider.gameObject.tag == "Player") 
            {
                state = "Chase";
            }
        }
    }
}
