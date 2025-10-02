using System;
using UnityEngine;

public class BoardSpaceManager : MonoBehaviour
{
    public int X;
    public int Y;
    public Card OccupyingCard { get; set; }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = null;
        transform.GetChild(1).GetComponent<SpriteRenderer>().sprite = null;
        transform.GetChild(2).GetComponent<SpriteRenderer>().sprite = null;
        transform.GetChild(3).GetComponent<SpriteRenderer>().sprite = null;
    }

    // Update is called once per frame
    void Update()
    {
                
    }

    public void OnCardPlaced(Card card)
    {
        OccupyingCard = card;
        gameObject.transform.parent.gameObject.SendMessage("OnCardPlaced", this);
        gameObject.GetComponent<BoxCollider2D>().enabled = false;
        transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = VisualsBehaviour.numberSprites[card.Top];
        transform.GetChild(1).GetComponent<SpriteRenderer>().sprite = VisualsBehaviour.numberSprites[card.Right];
        transform.GetChild(2).GetComponent<SpriteRenderer>().sprite = VisualsBehaviour.numberSprites[card.Bottom];
        transform.GetChild(3).GetComponent<SpriteRenderer>().sprite = VisualsBehaviour.numberSprites[card.Left];
        GetComponent<SpriteRenderer>().color = VisualsBehaviour.PlayerColours[card.CurrentOwner];
    }

    public void ResetSpace()
    {
        OccupyingCard = null;
        gameObject.GetComponent<BoxCollider2D>().enabled = true;
        transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = null;
        transform.GetChild(1).GetComponent<SpriteRenderer>().sprite = null;
        transform.GetChild(2).GetComponent<SpriteRenderer>().sprite = null;
        transform.GetChild(3).GetComponent<SpriteRenderer>().sprite = null;
        GetComponent<SpriteRenderer>().color = Color.white;
    }

    public void ChangeOwnership(PlayerID newOwner)
    {
        if (OccupyingCard == null) throw new Exception("No card to change ownership of");
        OccupyingCard.CurrentOwner = newOwner;
        GetComponent<SpriteRenderer>().color = VisualsBehaviour.PlayerColours[newOwner];
    }
}
