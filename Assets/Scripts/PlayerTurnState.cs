using UnityEngine;
using static GameEvents;

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