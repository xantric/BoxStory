public abstract class PlayerBaseState
{
    protected bool _isRootState = false;

    protected PlayerStateMachine _ctx;
    protected StateFactory _factory;

    protected PlayerBaseState _currentSubState;
    protected PlayerBaseState _currentSuperState;
    public PlayerBaseState(PlayerStateMachine currentContext, StateFactory factory)
    { 
        _ctx = currentContext;
        _factory = factory;
    }

    public abstract void EnterState();
    public abstract void ExitState();
    public abstract void UpdateState();
    public abstract void CheckSwitchStates();

    public abstract void InitializeSubState();

    public void UpdateStates() 
    {
        UpdateState();
        if (_currentSubState != null)
        {
            _currentSubState.UpdateStates();
        }
    }
    protected void SwitchState(PlayerBaseState newState)
    {
        ExitState();
        newState.EnterState();

        if (_isRootState)
        { _ctx.CurrentState = newState; }
        else if (_currentSuperState != null)
        { _currentSuperState.SetSubState(newState);}
    }
    protected void SetSuperState(PlayerBaseState newSuperState) 
    { 
        _currentSuperState = newSuperState;
    }
    protected void SetSubState(PlayerBaseState newSubState) 
    {
        _currentSubState = newSubState;
        newSubState.SetSuperState(this);
    }
 }
