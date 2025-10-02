using UnityEngine;
using UnityEngine.InputSystem;

public class TestScript : MonoBehaviour
{
    Mouse inputDevice;

    void Start()
    {
        inputDevice = Mouse.current;
    }
    void Update()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(inputDevice.position.ReadValue());
        RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero);
        if(hit.collider != null && hit.collider.gameObject == gameObject && inputDevice.press.isPressed)
        {
            transform.Translate(inputDevice.delta.ReadValue()/52);
        }
    }
}
