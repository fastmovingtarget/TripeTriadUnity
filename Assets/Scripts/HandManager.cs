using NUnit.Framework;
using System.Security.Cryptography;
using UnityEngine;
using System.Collections.Generic;

public class HandManager : MonoBehaviour
{
    public PlayerID Owner;
    public List<Card> Hand = new List<Card>();
    public int MaxHandSize;
    public bool AIControlled;

    public GameObject CardPrefab;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void InitialisePlayerHand(PlayerID playerID, bool isAIControlled, List<Card> playerHand)
    {
        foreach(Transform child in transform)
        {
            Destroy(child.gameObject);
        }
        Owner = playerID;
        AIControlled = isAIControlled;
        MaxHandSize = 5;
        Hand = playerHand;
        CardPrefab = Resources.Load<GameObject>("Prefabs/CardPrefab");

        for (int i = 0; i < MaxHandSize; i++)
        {
            GameObject cardObject = Instantiate(CardPrefab, transform);
            cardObject.GetComponent<CardData>().Initialise(Hand[i]);
            cardObject.name = "Card" + (i + 1);
            cardObject.transform.localPosition = new Vector3(0, i * -5.2f, 0);
            if (!AIControlled)
                cardObject.AddComponent<CardDragMovement>();
            else
                cardObject.AddComponent<AICardMovement>();
        }
        if (AIControlled)
        {
            ComputerOpponentAI existingAI = gameObject.GetComponent<ComputerOpponentAI>();
            if (existingAI == null)
                gameObject.AddComponent<ComputerOpponentAI>();

            gameObject.GetComponent<ComputerOpponentAI>().Initialise(playerID);
        }

    }

    public void ResetPlayerHand()
    {
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }
        Hand.Clear();
    }

    void CardPlayed(GameObject card)
    {
        card.transform.SetParent(null);
        if (transform.childCount == 0)
        {
            transform.parent.SendMessage("EndGame", Owner);
        }
        else
        {
            if (!AIControlled)
            {
                foreach (Transform child in transform)
                {
                    child.GetComponent<CardDragMovement>().DisableTurn();
                }
            }
            transform.parent.SendMessage("PlayerTurnEnded", Owner);
        }
        Hand.Remove(card.GetComponent<CardData>().Card);
        Destroy(card);
    }
}
