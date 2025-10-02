using UnityEngine;

public class AICardMovement : MonoBehaviour
{
    public Transform targetPosition; // The position the AI card should move to
    private GameObject currentBoardSpace;
    public int speed = 10;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (targetPosition == null)
            return;

        if (targetPosition.position != transform.position)
        {
            float step = speed * Time.deltaTime; // Adjust the speed as necessary
            transform.position = Vector3.MoveTowards(transform.position, targetPosition.position, step);
        }
        else
        {
            targetPosition.gameObject.SendMessage("OnCardPlaced", gameObject.GetComponent<CardData>().Card);
            transform.parent.gameObject.SendMessage("OnCardPlaced", this.gameObject);
            targetPosition = null; // Clear the target position after reaching it
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("BoardSpace"))
        {
            currentBoardSpace = collision.gameObject;
        }
    }
}
