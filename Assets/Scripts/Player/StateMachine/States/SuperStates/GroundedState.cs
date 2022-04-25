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
    protected bool camChange;

    public GroundedState(PlayerLogic playerLogic, StateMachine stateMachine, PlayerData playerData, string animationBool) : base(playerLogic, stateMachine, playerData, animationBool) {}

    public override void Enter()
    {
        base.Enter();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        movInput = PlayerLogic.playerInputHandler.movementInput;
        mouseInput = PlayerLogic.playerInputHandler.mousePos;
        canShoot = PlayerLogic.playerInputHandler.isShooting;
        interaction = PlayerLogic.playerInputHandler.isInteracting;
        reloading = PlayerLogic.playerInputHandler.isReloading;
        camChange = PlayerLogic.playerInputHandler.isCamChanging;

        if (canShoot)
        {
            PlayerLogic.Shoot();
        }

        if (interaction)
        {
            PlayerLogic.Interact();
        }

        if (reloading)
        {
            PlayerLogic.Reload();
        }

        if (camChange)
        {
            PlayerLogic.SwitchCamAngle();
        }
        
        PlayerLogic.SetRot(mouseInput);
        
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
