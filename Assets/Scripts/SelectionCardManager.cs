using UnityEngine;
using UnityEngine.InputSystem;

public class SelectionCardManager : MonoBehaviour
{
    public bool isSelected = false;
    private Mouse inputDevice;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        inputDevice = Mouse.current;
    }

    // Update is called once per frame
    void Update()
    {
        if (inputDevice.press.wasPressedThisFrame)
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(inputDevice.position.ReadValue());
            RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero);
            if (hit.collider != null && hit.collider.gameObject == gameObject)
            {
                if (!isSelected)
                {
                    transform.parent.gameObject.SendMessage("SelectCard", gameObject);
                }
                else
                {
                    transform.parent.gameObject.SendMessage("DeselectCard", gameObject.GetComponent<CardData>().Card);
                    gameObject.GetComponent<SpriteRenderer>().color = VisualsBehaviour.PlayerColours[gameObject.GetComponent<CardData>().Owner];
                    isSelected = false;
                }
            }
        }
    }
}
