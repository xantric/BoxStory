using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkState : PlayerBaseState
{
    public WalkState(PlayerStateMachine currentContext, StateFactory factory) : base (currentContext, factory) { }

    public override void EnterState() { }
    public override void ExitState() { }
    public override void UpdateState() { }
    public override void CheckSwitchStates() {
        CheckSwitchStates();
    }
    public override void InitializeSubState() { }
}
