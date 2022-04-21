using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : GroundedState
{
    public IdleState(PlayerController playerController, StateMachine stateMachine, PlayerData playerData, string animationBool) : base(playerController, stateMachine, playerData, animationBool)
    {
    }

    public override void Enter()
    {
        base.Enter();
        playerController.SetVel(Vector2.zero);
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (movInput.magnitude != 0)
        {
            stateMachine.ChangeState(playerController.movingState);
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
