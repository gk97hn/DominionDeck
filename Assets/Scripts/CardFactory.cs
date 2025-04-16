using UnityEngine;

public class CardFactory
{
    public enum DeckType
    {

        Ottoman,
        Byzantine

    }
    public Card CreateCard(CardData data)
    {
        Card card = new Card(data);

        return card;
    }

    public Card CreateRandomCard(DeckType type)
    {

        if (type == DeckType.Ottoman)
        {
            var allCards = Resources.LoadAll<CardData>("Cards/OttomanDeck");
            if (allCards.Length == 0) return null;

            var randomCardData = allCards[Random.Range(0, allCards.Length)];
            return CreateCard(randomCardData);
        }
        else if (type == DeckType.Byzantine)
        {
            var allCards = Resources.LoadAll<CardData>("Cards/ByzantineDeck");
            if (allCards.Length == 0) return null;

            var randomCardData = allCards[Random.Range(0, allCards.Length)];
            return CreateCard(randomCardData);
        }
        else 
        {
        return CreateCard(null);
        }
        
        
    }


}