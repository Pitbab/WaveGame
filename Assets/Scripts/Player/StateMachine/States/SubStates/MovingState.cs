using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingState : GroundedState
{
    public MovingState(PlayerController playerController, StateMachine stateMachine, PlayerData playerData, string animationBool) : base(playerController, stateMachine, playerData, animationBool)
    {
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        playerController.animator.SetFloat("x", movInput.x);
        playerController.animator.SetFloat("y", movInput.y);
        playerController.SetVel(movInput * playerData.speed * Time.deltaTime);
        
        if (movInput.magnitude == 0)
        {
            stateMachine.ChangeState(playerController.idleState);
        }
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
}

