using System.Collections.Generic;
using System;
using UnityEngine;

public abstract class StateManager<EState>: MonoBehaviour where EState : Enum
{

    protected Dictionary<EState, BaseState<EState>> States = new Dictionary<EState, BaseState<EState>>();
    //protected because we want to give access to this dictionary only to the scripts that inherit the State Manager

    protected BaseState<EState> CurrentState;

    protected  bool IsTransitioning = false;

    private void Start(){
        CurrentState.EnterState();
    }
    private void Update(){
        EState nextStateKey = CurrentState.GetNextState();

        if( !IsTransitioning && nextStateKey.Equals(CurrentState.StateKey))
            CurrentState.UpdateState(); //call current state logic every frame
        else
            TransitionToState(nextStateKey);
    }

    void TransitionToState(EState statekey)
    {
        IsTransitioning = true;
        CurrentState.ExitState();
        CurrentState = States[statekey];
        CurrentState.EnterState();
        IsTransitioning = false;
    }

    void OnTriggerEnter(Collider other){
        CurrentState.OnTriggerEnter(other);
    }
    void OnTriggerExit(Collider other){
        CurrentState.OnTriggerExit(other);
    }
    void OnTriggerStay(Collider other){
        CurrentState.OnTriggerStay(other);
    }

}
