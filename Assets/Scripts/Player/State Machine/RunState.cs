using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunState : PlayerBaseState
{
    public RunState(PlayerStateMachine currentContext, StateFactory factory) : base(currentContext, factory) { }
    public override void EnterState()
    {
        _ctx.Animator.SetBool(_ctx.IsRunningHash, true);
    }
    public override void ExitState() { }
    public override void UpdateState() {

        _ctx.CurrentMovementX = _ctx.CurrentMovementInputX;
        _ctx.CurrentMovementZ = _ctx.CurrentMovementInputY;

        CheckSwitchStates();

    }
    public override void CheckSwitchStates() 
    {
        if (!_ctx.IsMovementPressed)
            SwitchState(_factory.Idle());
    }
    public override void InitializeSubState() { }
}
