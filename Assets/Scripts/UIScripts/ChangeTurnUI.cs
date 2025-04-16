using UnityEngine;
using TMPro;
using UnityEngine.UI;
using static GameEvents;

public class ChangeTurnUI : MonoBehaviour
{

    [SerializeField] private GameObject _turnObject;
    [SerializeField] private GameObject _enemyTurnIcon;  
    [SerializeField] private GameObject _playerTurnIcon;
    [SerializeField] private float timer = 0f;
    [SerializeField] private float interval = 2f; 


    private void OnEnable()
    {
        EventManager.Instance.AddListener<PlayerTurnStartedEvent>(OnPlayerTurnStarted);
        EventManager.Instance.AddListener<EnemyTurnStartedEvent>(OnEnemyTurnStarted);
    }

    private void OnDisable()
    {
        EventManager.Instance.RemoveListener<PlayerTurnStartedEvent>(OnPlayerTurnStarted);
        EventManager.Instance.RemoveListener<EnemyTurnStartedEvent>(OnEnemyTurnStarted);
    }

    private void Update()
    {
        if (_turnObject.activeSelf)
        {
        timer += Time.deltaTime;

        if (timer >= interval)
        {
                _turnObject.SetActive(false);
                timer = 0f; 
        }
        }


    }

    private void OnPlayerTurnStarted(PlayerTurnStartedEvent e)
    {
        _turnObject.SetActive(true);
        _enemyTurnIcon.SetActive(false);
        _playerTurnIcon.SetActive(true);
    }

    private void OnEnemyTurnStarted(EnemyTurnStartedEvent e)
    {
        _turnObject.SetActive(true);
        _enemyTurnIcon.SetActive(true);
        _playerTurnIcon.SetActive(false);
    }

   
}