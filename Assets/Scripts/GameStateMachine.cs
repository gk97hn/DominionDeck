using System;
using UnityEngine;
using static GameEvents;

public class GameStateMachine
{
    private IGameState _currentState;

    public PlayerTurnState PlayerTurn { get; private set; }
    public EnemyTurnState EnemyTurn { get; private set; }
    public GameOverState GameOver { get; private set; }

    public GameStateMachine()
    {
        PlayerTurn = new PlayerTurnState(this);
        EnemyTurn = new EnemyTurnState(this);
        GameOver = new GameOverState(this);

        _currentState = PlayerTurn;
        _currentState.Enter();
    }
    public IGameState GetCurrentState()
    {
        return _currentState;

    }
    public void ChangeState(IGameState newState)
    {
        _currentState.Exit();
        _currentState = newState;
        _currentState.Enter();
    }

    public void Update()
    {
        _currentState.Update();
    }
}

