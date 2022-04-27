using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobState
{

    protected MobController mobController;
    protected StateMachine stateMachine;
    protected MobData mobData;
    protected string animationBool;
    protected float startTime;


    protected MobState(MobController mobController, StateMachine stateMachine, MobData mobData, string animationBool)
    {
        this.mobController = mobController;
        this.stateMachine = stateMachine;
        this.mobData = mobData;
        this.animationBool = animationBool;
    }
    
    public virtual void Enter()
    {
        //PlayerLogic.animator.SetBool(animationBool, true);
        startTime = Time.time;
    }

    public virtual void LogicUpdate()
    {
        
    }
    
    public virtual void Exit()
    {
        //mobController.animator.SetBool(animationBool, false);
    }
    

    public virtual void AnimationTrigger()
    {

    }

    //public virtual void AnimationFinishedTrigger() => isAnimationFinished = true;
    
}
