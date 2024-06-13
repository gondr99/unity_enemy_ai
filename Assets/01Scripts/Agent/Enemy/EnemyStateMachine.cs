using System;
using System.Collections.Generic;
using UnityEngine;

public enum FSMState 
{
    Idle,
    Move,
    Attack,
    Dead
}

public class EnemyStateMachine
{
    public Dictionary<string, EnemyState> stateDictionary;
    public EnemyState currentState;

    public EnemyStateMachine()
    {
        stateDictionary = new Dictionary<string, EnemyState>();
    }

    public void Initialize(string initState)
    {
        try {
            currentState = stateDictionary[initState];
            currentState.Enter();
        }catch(NullReferenceException nullEx)
        {
            Debug.LogError($"{initState} is not defined on statemachine");
            Debug.Log(nullEx);
        }
    }

    public void ChangeState(string newState)
    {
        try {
            currentState.Exit();
            currentState = stateDictionary[newState];
            currentState.Enter();
        }catch(NullReferenceException nullEx)
        {
            Debug.LogError($"{newState} is not defined on statemachine");
            Debug.Log(nullEx);
        }
    }

    public void UpdateMachine()
    {
        if(currentState == null) return;
        
        currentState.Update();
    }

    public void AddState(string stateName, EnemyState state)
    {
        stateDictionary.Add(stateName, state);
    }
}
