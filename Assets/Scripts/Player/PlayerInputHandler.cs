using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputHandler : MonoBehaviour
{
    public Vector2 movementInput { get; private set; }
    public Vector2 mousePos { get; private set; }
    
    public bool isShooting { get; private set; }
    
    public bool isInteracting { get; private set; }
    public bool isReloading { get; private set; }
    public bool isCamChanging { get; private set; }
    
    public bool isSpecialAttack { get; private set; }

    public PlayerInput playerInput;

    private void Start()
    {
        playerInput = GetComponent<PlayerInput>();
    }

    private void Update()
    {

    }

    public void OnMoveInput(InputAction.CallbackContext context)
    {
        movementInput = context.ReadValue<Vector2>();
    }

    public void OnMouseMove(InputAction.CallbackContext context)
    {
        mousePos = context.ReadValue<Vector2>();
    }

    public void OnShoot(InputAction.CallbackContext context)
    {
        isShooting = context.ReadValueAsButton();
    }

    public void OnInteract(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            isInteracting = true;
        }
    }

    public void OnReload(InputAction.CallbackContext context)
    {
        isReloading = context.ReadValueAsButton();
    }

    public void OnCamChange(InputAction.CallbackContext context)
    {
        isCamChanging = context.ReadValueAsButton();
    }

    public void OnSpecialAttack(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            isSpecialAttack = true;
        }
    }

    public void UseSwitchCam() => isCamChanging = false;
    public void UseInteract() => isInteracting = false;
    public void UseSpecialAttack() => isSpecialAttack = false;


}
