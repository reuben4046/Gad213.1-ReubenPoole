using UnityEngine;

public class MousePointer : MonoBehaviour
{
    Vector2 mousePos;

    void MousePosition()
    {
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = mousePos;
    }

    void Update()
    {
        MousePosition();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Metal")) return;

        GameEvents.OnMouseHoveringOverMetal?.Invoke(other.GetComponent<Metal>(), isHovering: true);
    }
}
