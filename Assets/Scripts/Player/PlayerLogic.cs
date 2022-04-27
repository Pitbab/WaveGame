using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Animations.Rigging;
using UnityEngine.InputSystem;

public class PlayerLogic : MonoBehaviour
{
    #region Components

    [SerializeField] private Camera cam;
    [SerializeField] private Transform laser;
    [SerializeField] private Transform gunHolder;
    public Transform gunHolderRef { get; private set; }
    public Animator animator { get; private set; }
    public PlayerInputHandler playerInputHandler { get; private set; }
    public CharacterController characterController { get; private set; }

    private GunController currentGun;
    public BulletPool bulletPool { get; private set; }
    public PlayerUIController playerUi { get; private set; }

    #endregion

    #region State Variables

    public StateMachine stateMachine { get; private set; }
    
    public IdleState idleState { get; private set; }
    public MovingState movingState { get; private set; }
    
    [SerializeField] private PlayerData playerData;

    #endregion
    
    #region Other Variables

    private Vector3 workSpace;
    private Vector3 lookVec;
    private Vector3 mouseHit;
    private LayerMask playerLayer;

    #endregion

    #region Local Varaibles

    private float localHealth;
    public float maxHealth => playerData.health;
    public int localCash { get; private set; }

    #endregion

    #region Unity Callback Functions

    private void Awake()
    {
        
        //Creating the state Machine and all the needed states
        stateMachine = new StateMachine();

        idleState = new IdleState(this, stateMachine, playerData, "Idle");
        movingState = new MovingState(this, stateMachine, playerData, "Moving");
        playerUi = transform.parent.Find("PlayerUI").GetComponent<PlayerUIController>();
    }

    private void Start()
    {
        //getting components and init the state machine
        animator = GetComponent<Animator>();
        playerInputHandler = GetComponent<PlayerInputHandler>();
        characterController = GetComponent<CharacterController>();
        gunHolderRef = gunHolder;
        bulletPool = transform.parent.Find("BulletPool").GetComponent<BulletPool>();
        playerLayer = LayerMask.GetMask("Player");
        
        SetHp(playerData.health);

        stateMachine.Initialize(idleState);

    }

    private void Update()
    {
        stateMachine.currentState.LogicUpdate();
        ApplyGravity();
    }

    #endregion



    #region Set Functions

    /// <summary>
    /// Set the player movement velocity, ignoring the y axis (gravity)
    /// </summary>
    /// <param name="vel"></param>
    public void SetVel(Vector2 vel)
    {
        //using a workspace to not create a new vector every time this function is called
        workSpace.Set(vel.x, 0f, vel.y);
        characterController.Move(workSpace);
    }

    /// <summary>
    /// Rotate the player toward the mouse position
    /// </summary>
    /// <param name="mousePos"></param>
    public void SetRot(Vector2 mousePos)
    {
        Ray ray = cam.ScreenPointToRay(mousePos);

        if (Physics.Raycast(ray, out RaycastHit raycastHit, 100f, ~playerLayer))
        {
            mouseHit = raycastHit.point;
            lookVec.Set(mouseHit.x - transform.position.x, 0, mouseHit.z - transform.position.z);
            
            //TODO smoother rotation from character
            
            transform.rotation = Quaternion.LookRotation(lookVec);
            laser.position = mouseHit;
        }

    }

    /// <summary>
    /// Equip the player with a gun
    /// </summary>
    /// <param name="gun"></param>
    public void SetCurrentGun(GunController gun)
    {
        if (currentGun != null)
        {
            currentGun.Drop();
        }
        currentGun = gun;
    }

    private void SetHp(float hp)
    {
        localHealth = hp;
        playerUi.OnHpChange?.Invoke(localHealth);
    }

    public void ChangeHp(float amount)
    {

        float result;

        if (localHealth + amount < 0)
        {
            result = 0;
            Debug.Log("You are dead!");
            SetHp(0);
            
        }
        else if(localHealth + amount > playerData.health)
        {
            result = playerData.health;
        }
        else
        {
            result = localHealth + amount;
        }
        
        SetHp(result);

    }

    public void SetCash(int amount)
    {
        localCash += amount;
        playerUi.OnCashChange?.Invoke(EventID.Cash, localCash);
    }

    public void SwitchCamAngle()
    {
        cam.GetComponent<CameraFollow>().SwitchCamAngle();
        playerInputHandler.UseSwitchCam();
    }

    public void UseSpecialAttack()
    {
        StartCoroutine(SpecialAttack());
        playerInputHandler.UseSpecialAttack();

    }
    
    #endregion

    private IEnumerator SpecialAttack()
    {
        GameObject go = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        go.transform.position = transform.position;
        go.layer = LayerMask.NameToLayer("NoMobs");
        go.AddComponent<NavMeshObstacle>().carving = true;
        go.GetComponent<MeshRenderer>().enabled = false;

        go.transform.localScale = new Vector3(0, 0, 0);
        Vector3 targetScale = new Vector3(10, 10, 10);

        float timer = 0f;
        float timeToScale = 0.2f;

        
        while (timer < timeToScale)
        {
            timer += Time.deltaTime;

            float lerpValue = timer / timeToScale;
            
            go.transform.localScale = Vector3.Slerp(go.transform.localScale, targetScale, lerpValue);
            yield return null;
        }
        
        Destroy(go);
        
    }


    #region Other Functions

    /// <summary>
    /// Apply constant force down to create gravity
    /// </summary>
    private void ApplyGravity()
    {
        Vector3 motion = new Vector3(0f, playerData.gravity, 0f);
        characterController.Move(motion * Time.deltaTime);
    }
    
    public void Shoot()
    {
        if (currentGun != null && !currentGun.isReloading)
        {
            currentGun.Shoot();
        }
    }

    /// <summary>
    /// checking for an interactable object and calling it's 'Interact function'
    /// </summary>
    public void Interact()
    {
        playerInputHandler.UseInteract();

        Collider[] col = Physics.OverlapSphere(transform.position, playerData.detectionRange, playerData.WhatisInteractible);

        foreach (var hit in col)
        {
            hit.GetComponent<Interactible>().Interact(this);
            break;
        }

    }

    public void Reload()
    {
        if (currentGun != null && !currentGun.isReloading)
        {
            currentGun.Reload();
        }
    }
    #endregion


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(cam.transform.position, mouseHit);
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, playerData.detectionRange);

    }
}
