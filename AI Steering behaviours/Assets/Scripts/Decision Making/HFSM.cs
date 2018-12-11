using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HFSM<T> where T : AbstractAgent
{
    // Maps a class name of that state to a specific instance of that state
    private Dictionary<Type, AbstractState<T>> _stateCache = new Dictionary<Type, AbstractState<T>>();

    // The current state we're in
    private AbstractState<T> _currentState;

    // Reference to the target so we can pass into our new states
    private T _target;

    public HFSM(T target)
    {
        _target = target;
        getAgentStates<AbstractState<T>>();
    }

    /// <summary>
    /// Changes the state if it is cached
    /// </summary>
    /// <typeparam name="U">The state to change to</typeparam>
    public void ChangeState<U>() where U : AbstractState<T>
    {
        // Check if a state like this is already in the cache
        if(!_stateCache.ContainsKey(typeof(U)))
        {
            Debug.LogError("Trying to access a state that is not a component of this object!");
            return;
        }

        AbstractState<T> newState = _stateCache[typeof(U)];

        if (_currentState != null)
        {
            // Handle pointing to base super states.
            if      (typeof(U).Equals(typeof(Super_Passive)))       newState = _stateCache[typeof(IdleState)];
            //else if (typeof(U).Equals(typeof(Super_Agressive)))     newState = _stateCache[typeof(SeekState)];
            else if (typeof(U).Equals(typeof(Super_Scared)))        newState = _stateCache[typeof(FleeState)];
        }

        changeState(newState);
    }

    /// <summary>
    /// Changes the state
    /// </summary>
    /// <param name="pNewState">The new state</param>
    private void changeState(AbstractState<T> pNewState)
    {
        if (_currentState == pNewState) return;
        AbstractState<T> prevState = _currentState;

        if (_currentState != null) _currentState.Exit(pNewState);
        _currentState = pNewState;
        if (_currentState != null) _currentState.Enter(prevState);
    }

    private void getAgentStates<U>() where U : AbstractState<T>
    {
        U[] targetStates = _target.GetComponents<U>();
        for (int i = 0; i < targetStates.Length; i++)
        {
            if (!_stateCache.ContainsKey(typeof(U)))
            {
                U state = targetStates[i];
                _stateCache.Add(state.GetType(), state);
                state.enabled = false;
            }
        }
    }

    // Updates the state
    public void Step()
    {
        if (_currentState != null) _currentState.Step();
    }

    public AbstractState<T> GetCurrentState()
    {
        return _currentState;
    }
}
