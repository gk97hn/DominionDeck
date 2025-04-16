using UnityEngine;
using System.Collections.Generic;
using static GameEvents;
using UnityEngine.UI;

public class EnemyHandUI : MonoBehaviour
{
    [SerializeField] private Transform _cardContainer;
    [SerializeField] private GameObject _cardPrefab;
    [SerializeField] private Image _enemyFightArea;

    private List<CardUI> _cardUIs = new List<CardUI>();

    private void Awake()
    {
        EventManager.Instance.AddListener<EnemyCardDrawnEvent>(OnCardDrawn);
        EventManager.Instance.AddListener<CardPlayedEvent>(OnCardPlayed);
    }

    private void OnDisable()
    {
        EventManager.Instance.RemoveListener<EnemyCardDrawnEvent>(OnCardDrawn);
        EventManager.Instance.RemoveListener<CardPlayedEvent>(OnCardPlayed);
    }

    public void AddCardToHand(Card card)

    {
        var cardGO = Instantiate(_cardPrefab, _cardContainer);
        if (card.Data.cardType == CardData.CardType.Minion)
        {
            cardGO.AddComponent<EnemyMinion>();
        }
        var cardUI = cardGO.GetComponent<CardUI>();
        cardUI.Initialize(card);
        _cardUIs.Add(cardUI);
    }

    public void RemoveCardFromHand(Card card)
    {
        var cardUI = _cardUIs.Find(c => c.Card == card);
        if (cardUI != null)
        {  
            cardUI.transform.SetParent(_enemyFightArea.transform, false);
            Transform backSide = cardUI.transform.GetChild(5);
            EnemyMinion enemyMinion = cardUI.gameObject.GetComponent<EnemyMinion>();
            if (enemyMinion != null)
            {
                enemyMinion.IsOnFight = true;
            }
            backSide.gameObject.SetActive(false);
            if (card.Data.cardType == CardData.CardType.Spell)
            {
                _cardUIs.Remove(cardUI);
                Destroy(cardUI.gameObject);

            }

        }
    }

    private void OnCardDrawn(EnemyCardDrawnEvent e)
    {
        AddCardToHand(e.Card);
    }

    private void OnCardPlayed(CardPlayedEvent e)
    {
        RemoveCardFromHand(e.Card);
    }
}