using static GameEvents;
using UnityEngine;

public class EnemyTurnState : IGameState
{
    private readonly GameStateMachine _stateMachine;
    private float _turnTimer;
    private const float TURN_DURATION = 3f;

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