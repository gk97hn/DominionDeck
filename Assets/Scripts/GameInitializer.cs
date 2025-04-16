using UnityEngine;

public class GameInitializer : MonoBehaviour
{
    private void Start()
    {
        var player = new PlayerController(30, 2, 5);
       // var enemy = new EnemyController();

        GameManager.Instance.Player = player;
        //GameManager.Instance.Enemy = enemy;

        var stateMachine = new GameStateMachine();

       
        player.StartTurn();
    }
}