using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingState : GroundedState
{
    public MovingState(PlayerLogic playerLogic, StateMachine stateMachine, PlayerData playerData, string animationBool) : base(playerLogic, stateMachine, playerData, animationBool)
    {
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        PlayerLogic.animator.SetFloat("x", movInput.x);
        PlayerLogic.animator.SetFloat("y", movInput.y);
        PlayerLogic.SetVel(movInput * playerData.speed * Time.deltaTime);
        
        if (movInput.magnitude == 0)
        {
            stateMachine.ChangeState(PlayerLogic.idleState);
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

