using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunState : MobState
{
    
    public RunState(MobController mobController, StateMachine stateMachine, MobData mobData, string animationBool) : base(mobController, stateMachine, mobData, animationBool) {}
    public override void Enter()
    {
        base.Enter();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void AnimationTrigger()
    {
        base.AnimationTrigger();
    }
}
