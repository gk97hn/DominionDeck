using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class DrawCards : MonoBehaviour
{
    [SerializeField] public List<GameObject> cardsList;
    [SerializeField] public GameObject PlayerDeckArea;
    [SerializeField] public GameObject EnemyDeckArea;
    [SerializeField] public int CardLimit;

    void Start()
    {
        
    }
    public void InstantiateRandomCard(bool isEnemy)
    {
        if (cardsList == null || cardsList.Count == 0)
        {
            return;
        }
        int randomIndex = Random.Range(0, cardsList.Count);

        GameObject cardToInstantiate = cardsList[randomIndex];

      
        GameObject newCard = Instantiate(cardToInstantiate, new Vector3(0, 0, 0), Quaternion.identity);

        if (isEnemy)
        {
            newCard.transform.SetParent(EnemyDeckArea.transform, false);
        }
        else 
        {
            newCard.transform.SetParent(PlayerDeckArea.transform, false);
        }
    }

    public void InstantiateMultipleRandomCards()
    {
        for (int i = 0; i < CardLimit; i++)
        {
            InstantiateRandomCard(true);
            InstantiateRandomCard(false);
        }
    }

    public void OnClickEndTurnButton() 
    {
        InstantiateMultipleRandomCards();

    }
}
