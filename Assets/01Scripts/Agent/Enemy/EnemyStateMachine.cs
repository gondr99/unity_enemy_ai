using System.Collections.Generic;
using UnityEngine;

public class EnemyStateMachine
{
    public EnemyState CurrentState { get; private set; }
    private Dictionary<int, EnemyState> _stateDict;

    public EnemyStateMachine()
    {
        _stateDict = new Dictionary<int, EnemyState>();
    }
    
    public void ChangeState(int newState)
    {
        CurrentState?.Exit();
        Debug.Assert(_stateDict.ContainsKey(newState), $"{newState} is not defined on statemachine");

        CurrentState = _stateDict[newState];
        CurrentState.Enter();
    }

    public void UpdateMachine()
    {
        CurrentState?.Update();
    }

    public void AddState(int stateNumber, EnemyState state)
    {
        _stateDict.Add(stateNumber, state);
    }
}
