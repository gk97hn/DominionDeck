using static GameEvents;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : IController
{
    public int Health { get; private set; }
    public int CurrentMana { get; private set; }
    public int MaxMana { get; private set; }

    private List<Card> _deck = new List<Card>();
    public List<Card> Hand = new List<Card>();

    public EnemyController(int startingHealth, int startingMana, int deckSize)
    {
        Health = startingHealth;
        MaxMana = startingMana;
        CurrentMana = startingMana;

        InitializeDeck(deckSize);
    }

    private void InitializeDeck(int deckSize)
    {
        for (int i = 0; i < deckSize; i++)
        {
            _deck.Add(GameManager.Instance.CardFactory.CreateRandomCard(CardFactory.DeckType.Byzantine));
        }
        for (int i = 0; i < 3; i++)
        {
            DrawCard();
        }
    }

    public void StartTurn()
    {
        MaxMana = Mathf.Min(10, MaxMana + 1);
        CurrentMana = MaxMana;
        DrawCard();
        EventManager.Instance.Raise(new EnemyManaChangedEvent(CurrentMana, MaxMana));
        EventManager.Instance.Raise(new EnemyHealthChangedEvent(Health));
        //EventManager.Instance.Raise(new EnemyTurnStartedEvent());

      
    }

    public void DrawCard()
    {
        if (_deck.Count == 0) return;

        var card = _deck[0];
        _deck.RemoveAt(0);
        Hand.Add(card);
        EventManager.Instance.Raise(new EnemyCardDrawnEvent(card));
    }

    public void PlayCard(Card card)
    {
        if (Hand.Contains(card) && CurrentMana >= card.Data.manaCost)
        {
           
            CurrentMana -= card.Data.manaCost;

           
            foreach (var effect in card.Data.effects)
            {
                ApplyEffect(effect);
            }

            EventManager.Instance.Raise(new CardPlayedEvent(card));
        }
    }

    private void ApplyEffect(CardEffect effect)
    {

        switch (effect.type)
        {

            case CardEffect.EffectType.Damage:
                ApplyDamageEffect(effect.value);
                break;
            case CardEffect.EffectType.Heal:

                var player1 = GameManager.Instance.Enemy;
                EventManager.Instance.Raise(new EnemyHealthChangedEvent(effect.value));
               
                break;

            case CardEffect.EffectType.GainMana:

                CurrentMana += effect.value;
                EventManager.Instance.Raise(new EnemyManaChangedEvent(CurrentMana, int.MaxValue));
                break;

            case CardEffect.EffectType.DrawCard:

                break;
        }
    }
    private void ApplyDamageEffect(int damage)
    {

        bool damagedMinion = MinionManager.Instance.DamageFirstPlayerMinion(damage);


        if (!damagedMinion)
        {
            var player = GameManager.Instance.Player;
            player.TakeDamage(damage);
        }
    }
    public void TakeDamage(int amount)
    {
        Health = Mathf.Max(0, Health - amount);
        EventManager.Instance.Raise(new EnemyHealthChangedEvent(Health));

        if (Health <= 0)
        {
            EventManager.Instance.Raise(new GameOverEvent(true));
        }
    }
}