using System.Collections.Generic;

enum PlayerStates
{ 
    RUN,
    IDLE,
    GROUNDED,
    JUMP
}

public class StateFactory
{
    PlayerStateMachine _context;
    Dictionary<PlayerStates, PlayerBaseState> _states = new Dictionary<PlayerStates, PlayerBaseState>();

    //constructor
    public StateFactory(PlayerStateMachine currentContext)
    { 
        _context = currentContext;

        _states[PlayerStates.RUN] = new RunState(_context, this);
        _states[PlayerStates.IDLE] = new IdleState(_context, this);
        _states[PlayerStates.JUMP] = new JumpState(_context, this);
        _states[PlayerStates.GROUNDED] = new GroundedState(_context, this);

    }

    public PlayerBaseState Idle() {
        return _states[PlayerStates.IDLE];
    }
    public PlayerBaseState Run() {
        return _states[PlayerStates.RUN];
    }
    public PlayerBaseState Jump() { 
        return _states[PlayerStates.JUMP];
    }
    public PlayerBaseState Grounded() { 
        return _states[PlayerStates.GROUNDED];
    }

}
