using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : PlayerBaseState
{
    public IdleState(PlayerStateMachine currentContext, StateFactory factory) : base(currentContext, factory) 
    { _isRootState = false; }
    public override void EnterState()
    {
        _ctx.Animator.SetBool(_ctx.IsRunningHash, false);
        _ctx.CurrentMovementX = 0f;
        _ctx.CurrentMovementZ = 0f;
    }
    public override void ExitState() { }
    public override void UpdateState() {
        CheckSwitchStates();
   
    }
    public override void CheckSwitchStates() 
    {
        if (_ctx.IsMovementPressed)
            SwitchState(_factory.Run());
    }
    public override void InitializeSubState() { }
}
