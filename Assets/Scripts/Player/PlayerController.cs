using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Animations.Rigging;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    #region Components

    [SerializeField] private Camera cam;
    public Camera playerCam;
    [SerializeField] private Transform laser;
    [SerializeField] private Transform gunHolder;
    public Transform gunHolderRef { get; private set; }
    public Animator animator { get; private set; }
    public PlayerInputHandler playerInputHandler { get; private set; }
    public CharacterController characterController { get; private set; }

    private GunController currentGun;
    
    public BulletPool bulletPool { get; private set; }

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

    #region Local Varaibles from data

    private float localHealth;

    #endregion

    #region Unity Callback Functions

    private void Awake()
    {
        
        //Creating the state Machine and all the needed states
        stateMachine = new StateMachine();

        idleState = new IdleState(this, stateMachine, playerData, "Idle");
        movingState = new MovingState(this, stateMachine, playerData, "Moving");
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

        localHealth = playerData.health;
        playerCam = cam;
        
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

    public void TakeDamage(float amount)
    {
        float healthLeft = localHealth - amount;

        if (healthLeft < 0)
        {
            //death
            Debug.Log("You are dead!");
        }
        else
        {
            Debug.Log("you took : " + amount + " damage");
            localHealth = healthLeft;
        }
    }
    
    #endregion

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
        if (currentGun != null)
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
    #endregion


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(cam.transform.position, mouseHit);
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, playerData.detectionRange);

    }
}
