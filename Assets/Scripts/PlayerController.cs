using UnityEngine;
using System.Collections.Generic;
using static GameEvents;
using System;

public class PlayerController : IController
{
    public int Health { get; private set; } = 30;
    public int CurrentMana { get; private set; }
    public int MaxMana { get; private set; } = 1;

    private List<Card> _deck = new List<Card>();
    private List<Card> _hand = new List<Card>();

    public PlayerController(int startingHealth, int startingMana, int deckSize)
    {
        Health = startingHealth;
        MaxMana = startingMana;
        CurrentMana = startingMana;
        EventManager.Instance.AddListener<ManaChangedEvent>(OnManaChanged);
        InitializeDeck(deckSize);
    }
    ~PlayerController()
    {
        EventManager.Instance.RemoveListener<ManaChangedEvent>(OnManaChanged);
    }

    private void OnManaChanged(ManaChangedEvent e)
    {
        CurrentMana = e.CurrentMana;
        MaxMana = e.MaxMana;

        foreach (var card in _hand)
        {
            card.UpdatePlayability(CurrentMana);
        }
    }
    private void InitializeDeck(int deckSize)
    {
        for (int i = 0; i < deckSize; i++)
        {
            _deck.Add(GameManager.Instance.CardFactory.CreateRandomCard(CardFactory.DeckType.Ottoman));
        }
        for (int i = 0; i < 3; i++)
        {
            DrawCard();
        }
    }
    public void DrawCard()
    {
        if (_deck.Count == 0) return;

        var card = _deck[0];
        _deck.RemoveAt(0);
        _hand.Add(card);

        EventManager.Instance.Raise(new CardDrawnEvent(card));
        UpdateCardPlayability();
    }

    public void StartTurn()
    {
        MaxMana = Mathf.Min(10, MaxMana + 1);
        CurrentMana = MaxMana;
        DrawCard();

        UpdateCardPlayability();
        EventManager.Instance.Raise(new ManaChangedEvent(CurrentMana, MaxMana));
        EventManager.Instance.Raise(new PlayerHealthChangedEvent(Health));
    }

    public void SpendMana(int amount)
    {
        CurrentMana -= amount;
        UpdateCardPlayability();
        EventManager.Instance.Raise(new ManaChangedEvent(CurrentMana, MaxMana));
      
    }

    private void UpdateCardPlayability()
    {
        foreach (var card in _hand)
        {
            card.UpdatePlayability(CurrentMana);
        }
    }

    public bool PlayCard(Card card)
    {
        if (_hand.Contains(card) && CurrentMana >= card.Data.manaCost)
        {
            return new CardCommand(card, this).Execute();
        }
        return false;
    }

    public void TakeDamage(int amount)
    {
        Health = Mathf.Max(0, Health - amount);
        EventManager.Instance.Raise(new PlayerHealthChangedEvent(Health));

        if (Health <= 0)
        {
            EventManager.Instance.Raise(new GameOverEvent(false));
        }
    }
    public void Heal(int amount)
    {
        Health = Mathf.Max(0, Health + amount);
        EventManager.Instance.Raise(new PlayerHealthChangedEvent(Health));

    }
}