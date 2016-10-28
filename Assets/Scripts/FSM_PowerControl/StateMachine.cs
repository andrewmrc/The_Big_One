using UnityEngine;
using System.Collections;
using System.Collections.Generic;



public class Transition
{
    public InputTransition inputT;
    public State fromState;
    public State toState;

    public Transition(State startState, InputTransition transition, State exitState)
    {
        fromState = startState;
        inputT = transition;
        toState = exitState;
    }
}

public class StateMachine
{
    public State stateControlBody;
    public State statePowerControl;
    public State stateShowMemory;

    List<Transition> transitionList;

    public State initialState;
    private State currentState;

    public void StateUpdate()
    {
        currentState.StateUpdate();
    }

    public void CreateTransition()
    {
        transitionList = new List<Transition>();
        transitionList.Add(new Transition(stateControlBody, InputTransition.MouseButtonOneDown, statePowerControl));
        transitionList.Add(new Transition(statePowerControl, InputTransition.MouseButtonOneUp, stateControlBody));
        //transitionList.Add(new Transition(statePowerControl, InputTransition.ShowMemory, stateShowMemory));
        transitionList.Add(new Transition(stateShowMemory, InputTransition.UnshowMemory, statePowerControl));
        transitionList.Add(new Transition(stateShowMemory, InputTransition.UnshowMemory, stateControlBody));
		transitionList.Add(new Transition(stateControlBody, InputTransition.ShowMemory, stateShowMemory));

    }

    public void StartMachine()
    {
        this.currentState = initialState;
    }

    public void HandleInput(InputTransition i)
    {
        foreach (var transition in transitionList)
        {
            if (transition.inputT == i && transition.fromState == this.currentState)
            {
                this.currentState = transition.toState;
            }
        }
    }
}

