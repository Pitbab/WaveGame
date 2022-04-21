using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class MobController : PoolItem
{
    [SerializeField] private MobData mobData;
    private NavMeshAgent navMesh;
    private CharacterController characterController;

    private float smallestDist;
    Transform bestTarget = null;

    private float timer;
    

    private void Start()
    {
        timer = 0;
        characterController = GetComponent<CharacterController>();
        navMesh = GetComponent<NavMeshAgent>();
        //InvokeRepeating("SetDestination", 0f, mobData.targetUpdateTime);
    }

    private void Update()
    {
        if (gameObject.activeInHierarchy)
        {
            if (timer < mobData.targetUpdateTime)
            {
                timer += Time.deltaTime;
            }
            else
            {
                SetDestination();
                timer = 0;
            }
        }

    }


    private void SetDestination()
    {
        navMesh.SetDestination(FindClosest(GameController.instance.GetPlayersPos()).position);
    }

    private Transform FindClosest(Transform[] players)
    {

        float closestDistanceSqr = Mathf.Infinity;
        Vector3 currentPosition = transform.position;
        foreach(Transform potentialTarget in players)
        {
            Vector3 directionToTarget = potentialTarget.position - currentPosition;
            float dSqrToTarget = directionToTarget.sqrMagnitude;
            if(dSqrToTarget < closestDistanceSqr)
            {
                closestDistanceSqr = dSqrToTarget;
                bestTarget = potentialTarget;
            }
        }
     
        return bestTarget;
    }

}
