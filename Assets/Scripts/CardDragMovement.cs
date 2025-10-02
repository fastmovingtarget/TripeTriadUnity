using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;
using System.Collections.Generic;
using UnityEngine.Rendering;
using UnityEditor;

public class CardDragMovement : MonoBehaviour
{
    Mouse inputDevice;
    Vector2 originalPosition;
    Vector2 downClick;
    GameObject hoveredBoardSpace;
    bool isTurn;


    void Start()
    {
        inputDevice = Mouse.current;
        originalPosition = transform.position;
        downClick = Vector2.zero;
        isTurn = true;
    }
    void Update()
    {
        if(downClick != Vector2.zero)
        { 
            if(inputDevice.press.wasReleasedThisFrame)
            {
                downClick = Vector2.zero;
                if (hoveredBoardSpace != null)
                {
                    transform.position = hoveredBoardSpace.transform.position;
                    hoveredBoardSpace.SendMessage("OnCardPlaced", gameObject.GetComponent<CardData>().Card);
                    transform.parent.gameObject.SendMessage("CardPlayed", this.gameObject);
                }
                else
                {
                    transform.position = originalPosition;
                    gameObject.GetComponent<SortingGroup>().sortingOrder = 3;
                    gameObject.GetComponent<SpriteRenderer>().color = VisualsBehaviour.PlayerColours[gameObject.GetComponent<CardData>().Owner];
                }
            }
            else
            {
                Vector2 mousePos = Camera.main.ScreenToWorldPoint(inputDevice.position.ReadValue());
                transform.position = new Vector3(originalPosition.x + (mousePos.x - downClick.x), originalPosition.y + (mousePos.y - downClick.y), transform.position.z);
            }
        }
        else if(isTurn)
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(inputDevice.position.ReadValue());
            RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero);
            if (hit.collider != null && hit.collider.gameObject == gameObject && inputDevice.press.wasPressedThisFrame)
            {
                if (downClick == Vector2.zero)
                {
                    downClick = mousePos;
                    gameObject.GetComponent<SortingGroup>().sortingOrder = 10;
                    gameObject.gameObject.GetComponent<SpriteRenderer>().color = VisualsBehaviour.SelectedColours[gameObject.GetComponent<CardData>().Owner];
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("BoardSpace"))
        {
            if(hoveredBoardSpace != null)
            {
                hoveredBoardSpace.GetComponent<SpriteRenderer>().color = Color.white;
            }
            hoveredBoardSpace = collision.gameObject;
            hoveredBoardSpace.GetComponent<SpriteRenderer>().color = Color.green;
        }
    }

    public void EnableTurn()
    {
        isTurn = true;
    }
    public void DisableTurn()
    {
        isTurn = false;
    }
}
