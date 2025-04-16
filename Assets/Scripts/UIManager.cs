using UnityEngine;
using UnityEngine.UI;
using static GameEvents;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }

   // [SerializeField] private HandUI _playerHandUI;
   // [SerializeField] private PlayerStatusUI _playerStatusUI;
   // [SerializeField] private EnemyStatusUI _enemyStatusUI;
    [SerializeField] private GameObject _gameOverPanel;
    [SerializeField] private Text _gameOverText;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    public void Initialize(PlayerController player)
    {
        //_playerHandUI.Initialize(player);
       // _playerStatusUI.Initialize(player);
       // _enemyStatusUI.Initialize(enemy);

        EventManager.Instance.AddListener<GameOverEvent>(OnGameOver);
    }

    private void OnGameOver(GameOverEvent e)
    {
        _gameOverPanel.SetActive(true);
        _gameOverText.text = e.PlayerWon ? "Victory!" : "Defeat!";
    }

    private void OnDestroy()
    {
        if (EventManager.Instance != null)
        {
            EventManager.Instance.RemoveListener<GameOverEvent>(OnGameOver);
        }
    }
}