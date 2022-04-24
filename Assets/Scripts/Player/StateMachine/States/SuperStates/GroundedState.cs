using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundedState : PlayerState
{
    protected Vector2 movInput;
    protected Vector2 mouseInput;
    protected bool canShoot;
    protected bool interaction;
    protected bool reloading;

    public GroundedState(PlayerController playerController, StateMachine stateMachine, PlayerData playerData, string animationBool) : base(playerController, stateMachine, playerData, animationBool) {}

    public override void Enter()
    {
        base.Enter();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        movInput = playerController.playerInputHandler.movementInput;
        mouseInput = playerController.playerInputHandler.mousePos;
        canShoot = playerController.playerInputHandler.isShooting;
        interaction = playerController.playerInputHandler.isInteracting;
        reloading = playerController.playerInputHandler.isReloading;

        if (canShoot)
        {
            playerController.Shoot();
        }

        if (interaction)
        {
            playerController.Interact();
        }

        if (reloading)
        {
            playerController.Reload();
        }
        
        playerController.SetRot(mouseInput);
        
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void DoCheck()
    {
        base.DoCheck();
    }

    public override void AnimationTrigger()
    {
        base.AnimationTrigger();
    }

    public override void AnimationFinishedTrigger()
    {
        base.AnimationFinishedTrigger();
    }
}
