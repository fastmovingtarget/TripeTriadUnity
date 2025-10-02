using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;

public class SelectionPanelManager : MonoBehaviour
{
    List<Card> selectedCards = new List<Card>();
    public GameObject CardPrefab;
    public PlayerID player;
    public int rowMax;
    public int columnMax;
    public float verticalSize = 5f;
    public float horizontalSize = 5f;
    public float spacing = 0.5f;
    public float maxSelectableCards = 5;
    public float commonProportion = 0.5f;
    public float uncommonProportion = 0.3f;
    public float rareProportion = 0.14f;
    public float epicProportion = 0.05f;
    public float legendaryProportion = 0.01f;

    private List<Card> SelectionList = new List<Card>();

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void PopulateSelectionList()
    {
        float proportionsSum = commonProportion + uncommonProportion + rareProportion + epicProportion + legendaryProportion;//should be 1, but just in case
        float selectionListSize = rowMax *  columnMax;
        while(SelectionList.Count < selectionListSize)
        {
            float rand = Random.Range(0f, 1f);
            Card cardToAdd;
            if(rand < legendaryProportion)
            {
                Debug.Log("Legendary card selected");
                cardToAdd = CardTemplates.GetRandomCardOfRarity(Rarity.Legendary, player);
            }
            else if (rand < (legendaryProportion + epicProportion)/proportionsSum)
            {
                Debug.Log("Epic card selected");
                cardToAdd = CardTemplates.GetRandomCardOfRarity(Rarity.Epic, player);
            }
            else if (rand < (legendaryProportion + epicProportion + rareProportion)/proportionsSum)
            {
                Debug.Log("Rare card selected");
                cardToAdd = CardTemplates.GetRandomCardOfRarity(Rarity.Rare, player);
            }
            else if (rand < (legendaryProportion + epicProportion + rareProportion + uncommonProportion) / proportionsSum)
            {
                Debug.Log("Uncommon card selected");
                cardToAdd = CardTemplates.GetRandomCardOfRarity(Rarity.Uncommon, player);
            }
            else
            {
                Debug.Log("Common card selected");
                cardToAdd = CardTemplates.GetRandomCardOfRarity(Rarity.Common, player);
            }
            if(!SelectionList.Contains(cardToAdd))//no duplicates
                SelectionList.Add(cardToAdd);
            else
                Debug.Log("Duplicate card found, reselecting");
        }
    }

    public void InitialiseSelectionPanel()
    {
        if(transform.childCount > 0)
        {
            foreach (Transform child in transform)
            { 
                Destroy(child.gameObject);
            }
            selectedCards.Clear();
        }
        PopulateSelectionList();
        int k = 0;
        for (int i = 0; i < rowMax; i++)
        {
            for (int j = 0; j < columnMax; j++, k++)
            {
                float horizontalLocation = ((i - (rowMax - 1)/2f) * (horizontalSize + spacing));//midpoint is 0, so to take an example, if rowMax is 1 then offset should be 0 when I is 0. If rowMax is 2 then offsets should be -0.5 and 0.5 when I is 0 and 1 respectively. and so on
                float verticalLocation = ((j - (columnMax - 1)/2f) * (verticalSize + spacing));
                GameObject cardObject = Instantiate(CardPrefab, transform);

                cardObject.GetComponent<CardData>().Initialise(SelectionList[k]);

                cardObject.transform.localPosition = new Vector3(horizontalLocation, verticalLocation, 0);
                cardObject.AddComponent<SelectionCardManager>();
            }
        }
    }

    public void SelectCard(GameObject cardObject)
    {
        if(selectedCards.Count < maxSelectableCards)
        {
            Card card = cardObject.GetComponent<CardData>().Card;
            cardObject.GetComponent<SpriteRenderer>().color = VisualsBehaviour.SelectedColours[card.CurrentOwner];
            cardObject.GetComponent<SelectionCardManager>().isSelected = true;
            selectedCards.Add(card);

            gameObject.transform.parent.GetComponent<GameStateManager>().SendMessage("CardSelected", card);
        }
    }
    public void DeselectCard(Card card)
    {
        selectedCards.Remove(card);
        gameObject.transform.parent.GetComponent<GameStateManager>().SendMessage("CardDeselected", card);
    } 
}
