using UnityEngine;

public class CardData : MonoBehaviour
{
    public Card Card { get; set; }
    public PlayerID Owner;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Initialise(Card card)
    {
        Card = card;
        Owner = card.CurrentOwner;
        transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = VisualsBehaviour.numberSprites[Card.Top];
        transform.GetChild(1).GetComponent<SpriteRenderer>().sprite = VisualsBehaviour.numberSprites[Card.Right];
        transform.GetChild(2).GetComponent<SpriteRenderer>().sprite = VisualsBehaviour.numberSprites[Card.Bottom];
        transform.GetChild(3).GetComponent<SpriteRenderer>().sprite = VisualsBehaviour.numberSprites[Card.Left];
        GetComponent<SpriteRenderer>().color = VisualsBehaviour.PlayerColours[Owner];
    }
}
