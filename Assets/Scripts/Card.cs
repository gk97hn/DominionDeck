using UnityEngine;
using static GameEvents;

public class Card
{
    public CardData Data { get; }
    public bool CanBePlayed { get; private set; }


    public Card(CardData data)
    {
        Data = data;
      
    }

    public void UpdatePlayability(int currentMana)
    {
        CanBePlayed = currentMana >= Data.manaCost;
    }

    public void Play()
    {
   
        EventManager.Instance.Raise(new CardPlayedEvent(this));

        foreach (var effect in Data.effects)
        {
            ApplyEffect(effect);
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

                var player1 = GameManager.Instance.Player;
                player1.Heal(effect.value);  
                break;

            case CardEffect.EffectType.GainMana:
       
                var player2 = GameManager.Instance.Player;
                int newMana = player2.CurrentMana + effect.value;

               
                EventManager.Instance.Raise(new ManaChangedEvent(newMana, player2.MaxMana));
                break;

            case CardEffect.EffectType.DrawCard:
                
                break;
        }
    }
    private void ApplyDamageEffect(int damage)
    {
      
        bool damagedMinion = MinionManager.Instance.DamageFirstMinion(damage);

     
        if (!damagedMinion)
        {
            var player = GameManager.Instance.Enemy;
            player.TakeDamage(damage);
        }
    }

}