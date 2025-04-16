using UnityEngine;
using System.Collections.Generic;
using static GameEvents;
using UnityEngine.UI;

public class HandUI : MonoBehaviour
{
    [SerializeField] private Transform _cardContainer;
    [SerializeField] private GameObject _cardPrefab;
    [SerializeField] private Image _playerFightArea;

    private List<CardUI> _cardUIs = new List<CardUI>();

    private void Awake()
    {
        EventManager.Instance.AddListener<CardDrawnEvent>(OnCardDrawn);
        EventManager.Instance.AddListener<CardPlayedEvent>(OnCardPlayed);
    }

    private void OnDisable()
    {
        EventManager.Instance.RemoveListener<CardDrawnEvent>(OnCardDrawn);
        EventManager.Instance.RemoveListener<CardPlayedEvent>(OnCardPlayed);
    }

    public void AddCardToHand(Card card)

    {
        var cardGO = Instantiate(_cardPrefab, _cardContainer);
        if (card.Data.cardType == CardData.CardType.Minion)
        {
            cardGO.AddComponent<Minion>();
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
            cardUI.transform.SetParent(_playerFightArea.transform, false);
            Minion minion = cardUI.gameObject.GetComponent<Minion>();
            if (minion != null)
            {
                minion.IsOnFight = true;
            }
        
            if (card.Data.cardType == CardData.CardType.Spell)
            {
                _cardUIs.Remove(cardUI);
                Destroy(cardUI.gameObject);

            }

        }
    }

    private void OnCardDrawn(CardDrawnEvent e)
    {
        AddCardToHand(e.Card);
    }

    private void OnCardPlayed(CardPlayedEvent e)
    {
        RemoveCardFromHand(e.Card);
    }
}