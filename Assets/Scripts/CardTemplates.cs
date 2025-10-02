using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;

public class CardTemplates : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public static List<Card> SelectCards(Rarity rarity, int number)
    {
        List<Card> selectedCards = new List<Card>();
        List<CardTemplate> templates = RarityChances[rarity];
        List<int> usedIndices = new List<int>();
        for (int i = 0; i < number; i++)
        {
            int index = Random.Range(0, templates.Count);
            while (usedIndices.Contains(index) && usedIndices.Count < templates.Count)//ensure no duplicates if possible
            {
                index = Random.Range(0, templates.Count);
            }
            selectedCards.Add(templates[index].ToCard(PlayerID.Player1));
        }
        return selectedCards;
    }

    public static Card GetRandomCardOfRarity(Rarity rarity, PlayerID player)
    {
        List<CardTemplate> templates = RarityChances[rarity];
        int index = Random.Range(0, templates.Count);
        return templates[index].ToCard(player);
    }

    public static Dictionary<Rarity, List<CardTemplate>> RarityChances = new Dictionary<Rarity, List<CardTemplate>>()
    {
        {
            Rarity.Common, new List<CardTemplate>()
            {
                new CardTemplate(Rarity.Common, 2, 9, 0, 0),
                new CardTemplate(Rarity.Common, 1, 0, 8, 5),
                new CardTemplate(Rarity.Common, 1, 1, 2, 2),
                new CardTemplate(Rarity.Common, 4, 1, 4, 2),
                new CardTemplate(Rarity.Common, 3, 7, 0, 4),
                new CardTemplate(Rarity.Common, 9, 0, 3, 2),
                new CardTemplate(Rarity.Common, 2, 0, 3, 4),
                new CardTemplate(Rarity.Common, 1, 2, 5, 0),
                new CardTemplate(Rarity.Common, 0, 0, 0, 8),
                new CardTemplate(Rarity.Common, 0, 1, 2, 8),
                new CardTemplate(Rarity.Common, 1, 8, 3, 2),
                new CardTemplate(Rarity.Common, 0, 5, 3, 6),
                new CardTemplate(Rarity.Common, 1, 3, 0, 8),
                new CardTemplate(Rarity.Common, 2, 0, 6, 3),
                new CardTemplate(Rarity.Common, 4, 2, 0, 5),
                new CardTemplate(Rarity.Common, 4, 0, 3, 5),
                new CardTemplate(Rarity.Common, 1, 1, 9, 0),
                new CardTemplate(Rarity.Common, 5, 4, 6, 0),
                new CardTemplate(Rarity.Common, 3, 3, 0, 7),
                new CardTemplate(Rarity.Common, 1, 1, 4, 3),
                new CardTemplate(Rarity.Common, 4, 0, 1, 3),
                new CardTemplate(Rarity.Common, 1, 8, 2, 0),
                new CardTemplate(Rarity.Common, 3, 2, 4, 0),
                new CardTemplate(Rarity.Common, 0, 4, 3, 3),
                new CardTemplate(Rarity.Common, 1, 0, 5, 0),
                new CardTemplate(Rarity.Common, 3, 5, 6, 1),
                new CardTemplate(Rarity.Common, 1, 4, 8, 2),
                new CardTemplate(Rarity.Common, 1, 5, 7, 2),
                new CardTemplate(Rarity.Common, 3, 1, 1, 5),
                new CardTemplate(Rarity.Common, 4, 9, 2, 0),
                new CardTemplate(Rarity.Common, 6, 2, 3, 2),
                new CardTemplate(Rarity.Common, 2, 2, 3, 1),
                new CardTemplate(Rarity.Common, 4, 3, 3, 1),
                new CardTemplate(Rarity.Common, 3, 6, 3, 3),
                new CardTemplate(Rarity.Common, 1, 2, 6, 5),
                new CardTemplate(Rarity.Common, 1, 8, 4, 1),
            }
        },{
            Rarity.Uncommon, new List<CardTemplate>()
            {
                new CardTemplate(Rarity.Uncommon, 6, 9, 0, 3),
                new CardTemplate(Rarity.Uncommon, 5, 1, 8, 4),
                new CardTemplate(Rarity.Uncommon, 3, 6, 0, 9),
                new CardTemplate(Rarity.Uncommon, 6, 1, 8, 3),
                new CardTemplate(Rarity.Uncommon, 2, 3, 6, 8),
                new CardTemplate(Rarity.Uncommon, 4, 0, 5, 9),
                new CardTemplate(Rarity.Uncommon, 2, 5, 4, 7),
                new CardTemplate(Rarity.Uncommon, 2, 2, 5, 8),
                new CardTemplate(Rarity.Uncommon, 3, 7, 3, 3),
                new CardTemplate(Rarity.Uncommon, 1, 7, 0, 9),
                new CardTemplate(Rarity.Uncommon, 4, 8, 4, 3),
                new CardTemplate(Rarity.Uncommon, 7, 1, 2, 9),
                new CardTemplate(Rarity.Uncommon, 2, 7, 9, 0),
                new CardTemplate(Rarity.Uncommon, 7, 3, 9, 0),
                new CardTemplate(Rarity.Uncommon, 8, 5, 4, 1),
                new CardTemplate(Rarity.Uncommon, 3, 8, 3, 2), 
            }
        },
        {
            Rarity.Rare, new List<CardTemplate>()
            {
                new CardTemplate(Rarity.Rare, 7, 2, 4, 7),
                new CardTemplate(Rarity.Rare, 5, 1, 7, 9),
                new CardTemplate(Rarity.Rare, 8, 4, 9, 1),
                new CardTemplate(Rarity.Rare, 1, 4, 6, 9),
                new CardTemplate(Rarity.Rare, 6, 9, 7, 2),
                new CardTemplate(Rarity.Rare, 7, 8, 5, 4),
                new CardTemplate(Rarity.Rare, 8, 6, 3, 5),
                new CardTemplate(Rarity.Rare, 8, 5, 6, 1),
                new CardTemplate(Rarity.Rare, 5, 2, 5, 9),
                new CardTemplate(Rarity.Rare, 4, 2, 7, 8),
                new CardTemplate(Rarity.Rare, 9, 2, 7, 4),
                new CardTemplate(Rarity.Rare, 0, 8, 7, 8),
                new CardTemplate(Rarity.Rare, 0, 6, 9, 6),
                new CardTemplate(Rarity.Rare, 0, 8, 8, 5),
                new CardTemplate(Rarity.Rare, 5, 7, 1, 7),
                new CardTemplate(Rarity.Rare, 4, 9, 5, 2),
                new CardTemplate(Rarity.Rare, 3, 6, 7, 5),
                new CardTemplate(Rarity.Rare, 4, 9, 1, 8),
                new CardTemplate(Rarity.Rare, 7, 9, 6, 4),
                new CardTemplate(Rarity.Rare, 4, 5, 6, 6),
                new CardTemplate(Rarity.Rare, 8, 2, 2, 9),
                new CardTemplate(Rarity.Rare, 6, 5, 4, 5),
                new CardTemplate(Rarity.Rare, 7, 5, 2, 2),
            }
        },
        {
            Rarity.Epic, new List<CardTemplate>()
            {
                new CardTemplate(Rarity.Epic, 5, 8, 6, 8),
                new CardTemplate(Rarity.Epic, 2, 8, 8, 8),
                new CardTemplate(Rarity.Epic, 7, 7, 7, 5),
                new CardTemplate(Rarity.Epic, 8, 7, 4, 8),
                new CardTemplate(Rarity.Epic, 3, 6, 8, 9),
            }
        },
        {
            Rarity.Legendary, new List<CardTemplate>()
            {
                new CardTemplate(Rarity.Legendary, 3, 9, 8, 8),
                new CardTemplate(Rarity.Legendary, 4, 7, 9, 9),
            }
        }
    };
}
public class CardTemplate
{
    public Rarity Rarity;
    public int Top;
    public int Right;
    public int Bottom;
    public int Left;

    public CardTemplate(Rarity rarity, int top, int right, int bottom, int left)
    {
        Rarity = rarity;
        Top = top;
        Right = right;
        Bottom = bottom;
        Left = left;
    }

    public override string ToString()
    {
        return $"Rarity: {Rarity}, Top: {Top}, Right: {Right}, Bottom: {Bottom}, Left: {Left}";
    }

    public Card ToCard(PlayerID player)
    {
        return new Card(player, Top, Right, Bottom, Left);
    }
}

public enum Rarity
{
    Common,
    Uncommon,
    Rare,
    Epic,
    Legendary
}