using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundedState : PlayerBaseState
{
    public GroundedState(PlayerStateMachine currentContext, StateFactory factory) : base(currentContext, factory) 
    {
        
        _isRootState = true;
    }
    public override void EnterState() 
    {
        InitializeSubState();
        _ctx.CurrentMovementY = _ctx.GroundedGravity;
    }
    public override void ExitState() 
    {

    }
    public override void UpdateState() { 
        CheckSwitchStates();
    }
    public override void CheckSwitchStates() { 

        if (_ctx.IsJumpPressed && !_ctx.RequireNewJumpPress)
        {
            SwitchState(_factory.Jump());
        }
    }
    public override void InitializeSubState() 
    {
        if (!_ctx.IsMovementPressed)
            SetSubState(_factory.Idle());

        if(_ctx.IsMovementPressed)
            SetSubState(_factory.Run());
    }
}
