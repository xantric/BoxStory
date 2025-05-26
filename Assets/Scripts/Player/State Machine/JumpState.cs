using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpState : PlayerBaseState
{
    public JumpState(PlayerStateMachine currentContext, StateFactory factory) : base(currentContext, factory) 
    { 
        _isRootState = true;
    }
    public override void EnterState()
    {
        InitializeSubState();
        handleJump();
    }
    public override void ExitState() 
    {
        _ctx.Animator.SetBool(_ctx.IsJumpingHash, false);
        if (_ctx.IsJumpPressed)
        _ctx.RequireNewJumpPress = true;
    }
    public override void UpdateState()
    {
        handleGravity();
        CheckSwitchStates();
    }
    public override void CheckSwitchStates() 
    {
        if (_ctx.IsGrounded)
        {
            SwitchState(_factory.Grounded());
        }
    }
    public override void InitializeSubState() 
    {
        if (!_ctx.IsMovementPressed)
            SetSubState(_factory.Idle());

        if (_ctx.IsMovementPressed)
            SetSubState(_factory.Run());
    }

///----------------------------------------------------------------///

    void handleJump()
    {
        _ctx.IsJumping = true;
        _ctx.Animator.SetBool(_ctx.IsJumpingHash, true);

        _ctx.CurrentMovementY = _ctx.InitialJumpVelocity;
    }

    void handleGravity()
    {
        bool isFalling = _ctx.CurrentMovementY <= 0.0f || !_ctx.IsJumpPressed;

        if (isFalling)
        {
            float previousYVelocity = _ctx.CurrentMovementY;
            float newYVelocity = _ctx.CurrentMovementY + (_ctx.Gravity * Time.deltaTime * 3.0f);
            float nextYVelocity = (previousYVelocity + newYVelocity) * .5f;
            _ctx.CurrentMovementY = nextYVelocity;
        }
        else
        {
            float previousYVelocity = _ctx.CurrentMovementY;
            float newYVelocity = _ctx.CurrentMovementY + (_ctx.Gravity * Time.deltaTime);
            float nextYVelocity = (previousYVelocity + newYVelocity) * .5f;
            _ctx.CurrentMovementY = nextYVelocity;
        }
    }
}