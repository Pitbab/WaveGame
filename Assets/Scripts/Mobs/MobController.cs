using UnityEngine;
using UnityEngine.AI;

public class MobController : PoolItem
{
    [SerializeField] private MobData mobData;
    [SerializeField] private GameObject hpSlider;
    [SerializeField] private Animator animator;
    [SerializeField] private SkinnedMeshRenderer skRenderer;
    [SerializeField] private Transform attackZone;
    private Vector3 hpStartingScale;
    private NavMeshAgent navMesh;

    private float smallestDist;
    Transform bestTarget = null;

    private PlayerLogic target;
    private AnimationEvent attackEvent;

    private float timer;
    private bool isDead;

    private float localHealth;
    public int cashOnTouched => mobData.cashOnTouched;

    private void Awake()
    {
        hpStartingScale = new Vector3(1.01f, 1.01f, 1.01f);
    }

    private void Start()
    {
        isDead = false;
        timer = 0;
        navMesh = GetComponent<NavMeshAgent>();
        ResetMob();

        target = GameController.instance.player;
    }

    private void Update()
    {
        if (!gameObject.activeInHierarchy) return;
        
        if (timer < mobData.targetUpdateTime)
        {
            timer += Time.deltaTime;
        }
        else
        {
            if (Vector3.Distance(transform.position, target.transform.position) <= mobData.attackRange)
            {
                animator.SetBool("isAttacking", true);
                navMesh.avoidancePriority = 200;
            }
            else
            {
                navMesh.avoidancePriority = 50;
                animator.SetBool("isAttacking", false);
                navMesh.SetDestination(target.transform.position);
            }
            timer = 0;
        }

        animator.enabled = skRenderer.isVisible;
    }
    
    /// <summary>
    /// only for multiplayer
    /// </summary>
    /// <param name="players"></param>
    /// <returns></returns>

    private Transform FindClosest(Transform players)
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
        isDead = false;
        //animator.SetBool("isAttacking,", false);
    }

    public void Attack()
    {

        Collider[] attackHitbox = Physics.OverlapBox(attackZone.position, new Vector3(2, 2, 2), transform.rotation);

        foreach (var col in attackHitbox)
        {
            if (col.gameObject.layer == LayerMask.NameToLayer("Player"))
            {
                col.gameObject.GetComponent<PlayerLogic>().ChangeHp(-mobData.attackDamage);
                Debug.Log("col with box");
            }
        }
    }

    public void TakeDamage(float amount, PlayerLogic dealer)
    {
        float hpLeft = localHealth -= amount;

        if (hpLeft <= 0)
        {
            if (!isDead)
            {
                isDead = true;
                dealer.SetCash(mobData.cashOnKill * GameController.instance.scoreMultiplier);
                SpawnerManager.instance.RemoveMobFromCounter();
                SpawnerManager.instance.SpawnPowerUp(transform.position);
                navMesh.enabled = false;
                Remove(); 
            }
        }
        else
        {
            dealer.SetCash(mobData.cashOnTouched * GameController.instance.scoreMultiplier);
            localHealth = hpLeft;
            hpSlider.transform.localScale = new Vector3((localHealth / mobData.health) * hpStartingScale.x,
                hpStartingScale.y, hpStartingScale.z);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, mobData.attackRange);
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, transform.forward * mobData.attackRange);

        Gizmos.matrix = attackZone.localToWorldMatrix;
        Gizmos.DrawWireCube(Vector3.zero, new Vector3(2, 2, 2));
    }
}
