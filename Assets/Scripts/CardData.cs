using UnityEngine;

[CreateAssetMenu(fileName = "New Card", menuName = "Cards/Card Data")]
public class CardData : ScriptableObject
{
    public string cardName;
    public string description;
    public int manaCost;
    public int health;
    public Sprite artwork;
    public CardType cardType;

    public CardEffect[] effects;

    public enum CardType
    {
        Spell,
        Minion,
        Weapon
    }
}

[System.Serializable]
public class CardEffect
{
    public EffectType type;
    public int value;

    public enum EffectType
    {
        Damage,
        Heal,
        DrawCard,
        GainMana
        // TODO GKhn add other effects
    }
}