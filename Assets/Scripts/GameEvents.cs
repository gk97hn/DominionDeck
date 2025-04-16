using UnityEngine;

public static class GameEvents 
{
    public interface IEvent { }

    public class CardPlayedEvent : IEvent
    {
        public Card Card { get; }
        public CardPlayedEvent(Card card) => Card = card;
    }

    public class PlayerTurnStartedEvent : IEvent { }

    public class CardDrawnEvent : IEvent
    {
        public Card Card { get; }
        public CardDrawnEvent(Card card) => Card = card;
    }
    public class EnemyCardDrawnEvent : IEvent
    {
        public Card Card { get; }
        public EnemyCardDrawnEvent(Card card) => Card = card;
    }

    public class CardSelectedEvent : IEvent
    {
        public Card Card { get; }
        public CardSelectedEvent(Card card) => Card = card;
    }

    public class ManaChangedEvent : IEvent
    {
        public int CurrentMana { get; }
        public int MaxMana { get; }
        public ManaChangedEvent(int current, int max)
        {
            CurrentMana = current;
            MaxMana = max;
        }
    }
    public class EnemyManaChangedEvent : IEvent
    {
        public int CurrentMana { get; }
        public int MaxMana { get; }
        public EnemyManaChangedEvent(int current, int max)
        {
            CurrentMana = current;
            MaxMana = max;
        }
    }
    public class GameOverEvent : IEvent
    {
        public bool PlayerWon { get; }
        public GameOverEvent(bool playerWon) => PlayerWon = playerWon;
    }
    public class EnemyTurnStartedEvent : IEvent { }

    public class EnemyHealthChangedEvent : IEvent
    {
        public int CurrentHealth { get; }
        public EnemyHealthChangedEvent(int health) => CurrentHealth = health;
    }

    public class PlayerHealthChangedEvent : IEvent
    {
        public int CurrentHealth { get; }
        public PlayerHealthChangedEvent(int health) => CurrentHealth = health;
    }
    public class MinionSpawnedEvent : IEvent
    {
        public Minion Minion { get; }
        public MinionSpawnedEvent(Minion minion) => Minion = minion;
    }

    public class MinionDamagedEvent : IEvent
    {
        public Minion Minion { get; }
        public int DamageAmount { get; }
        public MinionDamagedEvent(Minion minion, int damage)
        {
            Minion = minion;
            DamageAmount = damage;
        }
    } 
    public class EnemyMinionDamagedEvent : IEvent
    {
        public EnemyMinion Minion { get; }
        public int DamageAmount { get; }
        public EnemyMinionDamagedEvent(EnemyMinion minion, int damage)
        {
            Minion = minion;
            DamageAmount = damage;
        }
    }

    public class MinionHealedEvent : IEvent
    {
        public Minion Minion { get; }
        public int HealAmount { get; }
        public MinionHealedEvent(Minion minion, int heal)
        {
            Minion = minion;
            HealAmount = heal;
        }
    }

    public class MinionDiedEvent : IEvent
    {
        public Minion Minion { get; }
        public MinionDiedEvent(Minion minion) => Minion = minion;
    }
    public class EnemyMinionDiedEvent : IEvent
    {
        public EnemyMinion Minion { get; }
        public EnemyMinionDiedEvent(EnemyMinion minion) => Minion = minion;
    }
}
