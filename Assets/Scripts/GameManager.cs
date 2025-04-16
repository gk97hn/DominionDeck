using UnityEngine;
using UnityEngine.UI;
using static GameEvents;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public PlayerController Player { get;  set; }
    public EnemyController Enemy { get; private set; }
    public CardFactory CardFactory { get; private set; }
    public GameStateMachine StateMachine { get; private set; }

    [SerializeField] private int _startingHealth = 30;
   // [SerializeField] private int _startingMana = 1;
    [SerializeField] private int _deckSize = 20;
    [SerializeField] private PlayerAvatarUI _playerAvatarUI;
    [SerializeField] private Button _endTurnButton;

    private void Start()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        InitializeGame();
    }

    private void InitializeGame()
    {
      
        CardFactory = new CardFactory();

     
        Player = new PlayerController(_startingHealth, 10, _deckSize);
        Enemy = new EnemyController(_startingHealth, 10, _deckSize);
        Player.StartTurn();
        Enemy.StartTurn();
        StateMachine = new GameStateMachine();
        SetupEventListeners();
        StateMachine.ChangeState(StateMachine.PlayerTurn);
    }
    public void OnEndTurn() 
    { 
        if(StateMachine.GetCurrentState() is PlayerTurnState) 
        {
            StateMachine.ChangeState(StateMachine.EnemyTurn);
            Player.DrawCard();
        }
       
    }
    private void SetupEventListeners()
    {
        EventManager.Instance.AddListener<GameOverEvent>(OnGameOver);
    }

    private void OnGameOver(GameOverEvent e)
    {
        Debug.Log(e.PlayerWon ? "Player Won!" : "Enemy Won!");
        StateMachine.ChangeState(StateMachine.GameOver);
    }

    private void Update()
    {
        StateMachine?.Update();
    }

    private void OnDestroy()
    {
        if (EventManager.Instance != null)
        {
            EventManager.Instance.RemoveListener<GameOverEvent>(OnGameOver);
        }
    }
}