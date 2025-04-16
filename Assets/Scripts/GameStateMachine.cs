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

public interface IGameState
{
    void Enter();
    void Exit();
    void Update();
}

public class PlayerTurnState : IGameState
{
    private GameStateMachine _stateMachine;

    public PlayerTurnState(GameStateMachine stateMachine)
    {
        _stateMachine = stateMachine;
    }

    public void Enter()
    {
        Debug.Log("player turn");
        EventManager.Instance.Raise(new PlayerTurnStartedEvent());
    }

    public void Exit()
    {
        Debug.Log("Exit player turn");
    }

    public void Update()
    {
       
    }
}

public class EnemyTurnState : IGameState
{
    private readonly GameStateMachine _stateMachine;
    private float _turnTimer;
    private const float TURN_DURATION = 2f;

    public EnemyTurnState(GameStateMachine stateMachine)
    {
        _stateMachine = stateMachine;
    }

    public void Enter()
    {
        _turnTimer = TURN_DURATION;
        EventManager.Instance.Raise(new EnemyTurnStartedEvent());

  
        PlayEnemyCards();
    }

    private void PlayEnemyCards()
    {
        Card cardPlaying = null;
        foreach (var card in GameManager.Instance.Enemy.Hand)
        {

            GameManager.Instance.Enemy.PlayCard(card);
            cardPlaying = card;
        }
        GameManager.Instance.Enemy.Hand.Remove(cardPlaying);


    }

    public void Exit()
    {
        
    }

    public void Update()
    {
        _turnTimer -= Time.deltaTime;

        if (_turnTimer <= 0)
        {
            _stateMachine.ChangeState(_stateMachine.PlayerTurn);
            GameManager.Instance.Enemy.DrawCard();
        }
    }
}
public class GameOverState : IGameState
{
    private GameStateMachine gameStateMachine;

    public GameOverState(GameStateMachine gameStateMachine)
    {
        this.gameStateMachine = gameStateMachine;
    }

 
    public void Enter()
    {
      
    }

    public void Exit()
    {
        
    }

    public void Update()
    {
        
    }
}