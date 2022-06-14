using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AI : MonoBehaviour
{
    Ray aiRay;
    RaycastHit rayHit;
    float sightDistance = 2f;

    [SerializeField]
    private Transform playerLoc;

    NavMeshAgent nm;
    // Start is called before the first frame update
    void Start()
    {
        playerLoc = GameObject.FindGameObjectWithTag("Player").transform;

        nm = GetComponent<NavMeshAgent>();

        //ChasePlayer();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateRay();
        ChasePlayer();

        Debug.DrawRay(aiRay.origin, aiRay.direction);
    }

    private void FixedUpdate()
    {
        CastRay();
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
                print("A Barrier");
            }
        }
    }
}
