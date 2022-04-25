using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerState
{
    protected PlayerLogic PlayerLogic;
    protected StateMachine stateMachine;
    protected PlayerData playerData;

    protected bool isAnimationFinished;

    protected float startTime;
    private string animationBool;
    
    
    
    protected PlayerState(PlayerLogic playerLogic, StateMachine stateMachine, PlayerData playerData, string animationBool)
    {
        this.PlayerLogic = playerLogic;
        this.stateMachine = stateMachine;
        this.playerData = playerData;
        this.animationBool = animationBool;
    }
    
    public virtual void Enter()
    {
        DoCheck();
        PlayerLogic.animator.SetBool(animationBool, true);
        startTime = Time.time;
        isAnimationFinished = false;
    }

    public virtual void LogicUpdate()
    {
        
    }

    public virtual void PhysicsUpdate()
    {
        DoCheck();
    }

    public virtual void Exit()
    {
        PlayerLogic.animator.SetBool(animationBool, false);
    }

    public virtual void DoCheck()
    {
        
    }

    public virtual void AnimationTrigger()
    {

    }

    public virtual void AnimationFinishedTrigger() => isAnimationFinished = true;
}
