using System;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class MobController : PoolItem
{
    [SerializeField] private MobData mobData;
    [SerializeField] private GameObject hpSlider;
    private Vector3 hpStartingScale;
    private NavMeshAgent navMesh;
    private CharacterController characterController;

    private float smallestDist;
    Transform bestTarget = null;

    private float timer;

    private float localHealth;

    private void Awake()
    {
        hpStartingScale = new Vector3(1.01f, 1.01f, 1.01f);
    }

    private void Start()
    {
        localHealth = mobData.health;
        
        timer = 0;
        characterController = GetComponent<CharacterController>();
        navMesh = GetComponent<NavMeshAgent>();
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
                Attack();
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

    public void ResetMob()
    {
        localHealth = mobData.health;
        hpSlider.transform.localScale = hpStartingScale;
    }

    private void Attack()
    {
        Ray ray = new Ray(transform.position, Vector3.forward);
        
        if(Physics.Raycast(ray, out RaycastHit hit, mobData.attackRange, mobData.whatIsPlayer))
        {
            hit.collider.gameObject.GetComponent<PlayerController>().TakeDamage(mobData.attackDamage);
        }
    }

    public void TakeDamage(float amount)
    {
        float hpLeft = localHealth -= amount;

        if (hpLeft < 0)
        {
            SpawnerManager.instance.numberAlive--;
            Remove();
        }
        else
        {
            localHealth = hpLeft;
            hpSlider.transform.localScale = new Vector3((localHealth / mobData.health) * hpStartingScale.x,
                hpStartingScale.y, hpStartingScale.z);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, mobData.attackRange);
    }
}
