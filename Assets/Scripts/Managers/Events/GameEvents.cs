using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEvents
{
    public event Action<GameState> onStateChange;

    public void StateChange(GameState newState)
    {
        if (onStateChange != null)
            onStateChange(newState);
    }

    public event Action<GameState> onUpdateGameState;

    public void UpdateGameState(GameState newState)
    {
        onUpdateGameState?.Invoke(newState);
    }
}
